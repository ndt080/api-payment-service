//
//  RegisterModel.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

struct RegisterModel: Codable {
    var id: Int
    var email: String
    var jwtToken: String
    var refreshToken: String
    var subscriptions: [String]
}
