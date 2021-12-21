//
//  FieldsEncoder.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

public struct FieldsEncoder {
    public static func encode(urlRequest: inout URLRequest, with parameters: Fields?) {
        guard let fields = parameters else {return}
        for field in fields {
            if urlRequest.value(forHTTPHeaderField: field.key) == nil {
                urlRequest.setValue(field.value, forHTTPHeaderField: field.key)
            }
        }
    }
}
