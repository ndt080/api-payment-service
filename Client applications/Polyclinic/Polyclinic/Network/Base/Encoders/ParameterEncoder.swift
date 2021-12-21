//
//  ParameterEncoder.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

// Протокол кодирует параметры
public protocol ParameterUrlEncoder {
    /// Кодиреует параметры для запроса
    static func encode(urlRequest: inout URLRequest, with parameters: URLParameters) throws
}

// Протокол кодирует параметры так как вам нужно
public protocol ParameterBodyEncoder {
    /// Кодирует параметры для запроса
    static func encode(urlRequest: inout URLRequest, with parameters: BodyParameters) throws
}
