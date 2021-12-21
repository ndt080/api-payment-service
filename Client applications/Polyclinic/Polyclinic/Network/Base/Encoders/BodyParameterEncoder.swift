//
//  BodyParameterEncode.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

public struct BodyParameterEncoder: ParameterBodyEncoder {
    public static func encode(urlRequest: inout URLRequest, with parameters: BodyParameters) {
        do {
            let jsonAsData = try JSONSerialization.data(withJSONObject: parameters, options: [ .fragmentsAllowed])
            urlRequest.httpBody = jsonAsData
        } catch let error {
            print(error.localizedDescription)
        }
    }
}
