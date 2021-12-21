//
//  NetworkAPI.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

public protocol NetworkAPI {
    var path : String {get}
    var httpMethod: HTTPMethod {get}
    var bodyParametrs: BodyParameters? {get}
    var urlParameters: URLParameters? {get}
    var fields: Fields? {get}
    var httpHeaders: HTTPHeader? {get}
}
