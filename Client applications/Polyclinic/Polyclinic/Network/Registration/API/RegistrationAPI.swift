//
//  RegistrationAPI.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

enum RegistrationAPI {
    case subscribe(name: String, cost: Int)
}

extension RegistrationAPI: NetworkAPI {
    var path: String {
        switch self {
        case .subscribe:
            return "/api/Subscribe/subscribe"
        }
    }
    
    var httpMethod: HTTPMethod {
        switch self {
        case .subscribe:
            return .post
        }
    }
    
    var bodyParametrs: BodyParameters? {
        switch self {
        case .subscribe(let name, let cost):
            return ["serviceName": name,
                    "paymentAmount": cost]
        }
    }
    
    var urlParameters: URLParameters? {
        switch self {
        case .subscribe:
            return nil
        }
    }
    
    var fields: Fields? {
        switch self {
        case .subscribe:
            return ["accept" : "*/*",
                    "Content-Type" : "application/json"]
        }
    }
    
    var httpHeaders: HTTPHeader? {
        let token = UserDefaultsManager().getToken()
        return ["Authorization" : token]
    }
    
    
}
