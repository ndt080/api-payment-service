//
//  PolyclinicManager.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

class PolyclinicManager {
    let router = Router<PolyclinicAPI>(baseURL: URL(string: "https://api-polyclinic-service.herokuapp.com")!)
    
    
    public func handleNetworkResponse(_ response: HTTPURLResponse) -> NetworkError<Error> {
        switch response.statusCode {
        case 200...299 : return .success
        case 401...500 : return .failure(NetworkResponse.authentificationError)
        case 501...599 : return .failure(NetworkResponse.badRequest)
        default: return .failure(NetworkResponse.failed)
        }
    }
}

extension PolyclinicManager: PolyclinicManagerProtocol {
    
    private func routerRequest<T: Codable>(_ route: PolyclinicAPI,
                                           decodeType: T.Type,
                                           completion: @escaping (Result<T, Error>) -> Void,
                                           progress: RouterProgressHandler? = nil) {
            router.request(route, completion: { data, response, error in
                
                guard error == nil else {
                    completion(.failure(error!))
                    return
                }
                
                guard let response = response as? HTTPURLResponse else {
                    completion(.failure(NetworkResponse.unableToDecode))
                    return
                }
                
                let result = self.handleNetworkResponse(response)
                switch result {
                    case .success:
                        guard let responseData = data else {
                            completion(.failure(NetworkResponse.noData))
                            return
                        }
                        do {
                            let apiResponse: T = try JSONDecoder().decode(decodeType, from: responseData)
                            completion(.success(apiResponse))
                        } catch let error {
                            print(error.localizedDescription)
                            completion(.failure(NetworkResponse.unableToDecode))
                        }
                        
                    case .failure(let failureError):
                        completion(.failure(failureError))
                }
            }, progress: progress)
        }
    
    func getAllApointments(completion: @escaping (Result<[Apointment], Error>) -> Void) {
        routerRequest(.getAllApointments,
                      decodeType: [Apointment].self,
                      completion: completion,
                      progress: nil)
    }
    
    func makeApointment(data: Apointment, completion: @escaping (Result<Apointment, Error>) -> Void) {
        routerRequest(.makeApointment(data: data),
                      decodeType: Apointment.self,
                      completion: completion,
                      progress: nil)
    }
    
    func deleteApointment(data: Apointment, completion: @escaping (Result<String, Error>) -> Void) {
        routerRequest(.deleteApointment(data: data),
                      decodeType: String.self,
                      completion: completion,
                      progress: nil)
    }
}


