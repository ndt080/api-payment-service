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
    case deleteApointment(data: Apointment)
}

extension PolyclinicAPI: NetworkAPI {
    var path: String {
        switch self {
        case .getAllApointments:
            return "/api/Polyclinic/GetAllVisits"
        case .makeApointment:
            return "/api/Polyclinic/AddVisit"
        case .deleteApointment:
            return "/api/Polyclinic/DeleteVisit"
        }
    }
    
    var httpMethod: HTTPMethod {
        switch self {
        case .getAllApointments:
            return .get
        case .makeApointment:
            return .post
        case .deleteApointment:
            return .delete
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
        case .deleteApointment(data: let data):
            return nil
        }
    }
    
    var urlParameters: URLParameters? {
        switch self {
        case .getAllApointments:
            let token = UserDefaultsManager().getAPIKey()
            return ["accessKey":token]
        case .makeApointment:
            let token = UserDefaultsManager().getAPIKey()
            return ["accessKey":token]
        case .deleteApointment(data: let data):
            let token = UserDefaultsManager().getAPIKey()
            return ["patient" : data.patientFio,
                    "speciality" : data.speciality,
                    "accessKey": token]
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
        case .deleteApointment:
            return ["accept" : "*/*",
                    "Content-Type" : "application/json"]
        }
    }
    
    var httpHeaders: HTTPHeader? {
        let token = UserDefaultsManager().getToken()
        return ["accessKey":token]
    }
}
