//
//  AuthAPI.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

enum AuthAPI {
    /// register user
    case register(email: String, password: String)
    /// login
    case login(email: String, password: String)
}

extension AuthAPI: NetworkAPI {
    var path: String {
        switch self {
        case .register:
            return "/api/Auth/register"
        case .login:
            return "/api/Auth/login"
        }
    }
    
    var httpMethod: HTTPMethod {
        switch self {
        case .register:
            return .post
        case .login:
            return .post
        }
    }
    
    var bodyParametrs: BodyParameters? {
        switch self {
        case .register(let email, let password):
            return ["email" : email,
                    "password" : password]
        case .login(email: let email, password: let password):
            return ["email" : email,
                    "password" : password]
        }
    }
    
    var urlParameters: URLParameters? {
        switch self {
        case .register:
            return nil
        case .login:
            return nil
        }
    }
    
    var fields: Fields? {
        switch self {
        case .register:
            return ["accept" : "*/*",
                    "Content-Type" : "application/json"]
        case .login:
            return ["accept" : "*/*",
                    "Content-Type" : "application/json"]
        }
    }
    
    var httpHeaders: HTTPHeader? {
        //guard let token = AsapService.token else { return nil }
        return ["Authorization" : "token"]
    }
}
