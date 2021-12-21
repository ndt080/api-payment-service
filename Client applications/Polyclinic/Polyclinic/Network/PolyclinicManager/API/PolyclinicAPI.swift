//
//  PolyclinicAPI.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

enum PolyclinicAPI {
    
    case getAllApointments
    case makeApointment(data: Apointment)
}

extension PolyclinicAPI: NetworkAPI {
    var path: String {
        switch self {
        case .getAllApointments:
            return "/api/polyclinic/polyclinic"
        case .makeApointment:
            return "/api/polyclinic/polyclinic/AddVisit"
        }
    }
    
    var httpMethod: HTTPMethod {
        switch self {
        case .getAllApointments:
            return .get
        case .makeApointment:
            return .post
        }
    }
    
    var bodyParametrs: BodyParameters? {
        switch self {
        case .getAllApointments:
            return nil
        case .makeApointment(data: let data):
            do {
                let dictionary = try DictionaryEncoder().encode(data)
                return dictionary
            } catch let error {
                print(error)
                return nil
            }
        }
    }
    
    var urlParameters: URLParameters? {
        switch self {
        case .getAllApointments:
            let token = UserDefaultsManager().getToken()
            return ["accessKey":token]
        case .makeApointment:
            return nil
        }
    }
    
    var fields: Fields? {
        switch self {
        case .getAllApointments:
            return ["accept" : "*/*",
                    "Content-Type" : "application/json"]
        case .makeApointment:
            return ["accept" : "*/*",
                    "Content-Type" : "application/json"]
        }
    }
    
    var httpHeaders: HTTPHeader? {
        return nil
    }
    
    
}
