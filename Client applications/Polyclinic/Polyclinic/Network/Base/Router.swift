//
//  Router.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

public typealias NetworkRouterCompletion = (_ data: Data?,_ response: URLResponse?,_ error: Error?) -> ()
public typealias RouterProgressHandler = (_ currentSize: Int64, _ totalSize: Int64) -> ()

public class Router<EndPoint: NetworkAPI> : NSObject, URLSessionDataDelegate {
    public var task : URLSessionTask?
    public var baseURL: URL
    
    public let operationQueue: OperationQueue = OperationQueue()
    public var progressHandlerByTaskId: [Int : RouterProgressHandler]
    
    
    public init(baseURL: URL) {
        self.baseURL = baseURL
        self.progressHandlerByTaskId = [:]
        super.init()
    }
    
    public func request(_ route: EndPoint, completion: @escaping NetworkRouterCompletion, progress:  RouterProgressHandler? = nil) {
        let session = URLSession(configuration: .default, delegate: self, delegateQueue: operationQueue)
        do {
            let request = try buildRequest(from: route)
            switch request.httpMethod {
            case HTTPMethod.post.rawValue, HTTPMethod.put.rawValue:
                task = session.uploadTask(with: request, from: request.httpBody, completionHandler: { data, response, error in
                    completion(data, response, error)
                })
                if progress != nil {
                    progressHandlerByTaskId[task?.taskIdentifier ?? 0] = progress
                }
            default:
                task = session.dataTask(with: request, completionHandler: { data, response, error in
                    completion(data, response, error)
                })
            }
        } catch {
            completion(nil, nil, error)
        }
        task?.resume()
    }
    
    // Отмена задачи
    public func cancel() {
        task?.cancel()
    }
    // Построние запроса
    public func buildRequest(from route: EndPoint) throws -> URLRequest {
        
        var request = URLRequest(url: self.baseURL.appendingPathComponent(route.path),
                                 cachePolicy: .reloadIgnoringLocalAndRemoteCacheData,
                                 timeoutInterval: 30.0)
        
        request.httpMethod = route.httpMethod.rawValue
        
        self.configureParameters(bodyParameters: route.bodyParametrs, urlParameters: route.urlParameters, request: &request)
        self.configureFields(fields: route.fields, request: &request)
        self.addAditionalHeaders(route.httpHeaders, request: &request)
        
        #if DEBUG
        print("CURL \n\(request.curlString)")
        #endif
        return request
    }
    
    public func configureParameters(bodyParameters: BodyParameters?,
                                    urlParameters: URLParameters?,
                                    request: inout URLRequest) {
        if let bodyParameters = bodyParameters {
            BodyParameterEncoder.encode(urlRequest: &request, with: bodyParameters)
        }
        if let urlParameters = urlParameters {
            URLParameterEncoder.encode(urlRequest: &request, with: urlParameters)
        }
    }
    
    public func configureFields(fields: Fields?, request: inout URLRequest) {
        FieldsEncoder.encode(urlRequest: &request, with: fields)
    }
    
    public func addAditionalHeaders(_ additionalHeaders: HTTPHeader?, request: inout URLRequest) {
        guard let headers = additionalHeaders else {return}
        for (key, value) in headers {
            request.setValue(value, forHTTPHeaderField: key)
        }
    }
    
    public func urlSession(_ session: URLSession, task: URLSessionTask, didSendBodyData bytesSent: Int64, totalBytesSent: Int64, totalBytesExpectedToSend: Int64) {
        if let handlerProgress = progressHandlerByTaskId[task.taskIdentifier] {
            handlerProgress(totalBytesSent, totalBytesExpectedToSend)
            if totalBytesSent == totalBytesExpectedToSend {
                progressHandlerByTaskId.removeValue(forKey: task.taskIdentifier)
            }
        }
    }
}
