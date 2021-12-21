//
//  AuthManager.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

class AuthManager: ObservableObject {
    let router = Router<AuthAPI>(baseURL: URL(string: "https://api-payment-service.herokuapp.com")!)
    
    
    public func handleNetworkResponse(_ response: HTTPURLResponse) -> NetworkError<Error> {
        switch response.statusCode {
        case 200...299 : return .success
        case 401...500 : return .failure(NetworkResponse.authentificationError)
        case 501...599 : return .failure(NetworkResponse.badRequest)
        default: return .failure(NetworkResponse.failed)
        }
    }
}

extension AuthManager: AuthManagerProtocol {
    private func routerRequest<T: Codable>(_ route: AuthAPI,
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
    
    func register(email: String, password: String, completion: @escaping (Result<RegisterModel, Error>) -> Void) {
        routerRequest(.register(email: email, password: password),
                      decodeType: RegisterModel.self,
                      completion: completion,
                      progress: nil)
    }
    
    func login(email: String, password: String, completion: @escaping (Result<String, Error>) -> Void) {
        routerRequest(.login(email: email, password: password),
                      decodeType: String.self,
                      completion: completion,
                      progress: nil)
    }
}
