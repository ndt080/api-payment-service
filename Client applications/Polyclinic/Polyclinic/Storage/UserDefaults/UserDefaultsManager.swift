//
//  UserDefaultsManager.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

class UserDefaultsManager {
    private let defaults = UserDefaults.standard
    
    func saveToken(token: String) {
        defaults.set(token, forKey: "access.token")
    }
    
    func getToken() -> String {
        return defaults.string(forKey: "access.token") ?? ""
    }
}
