//
//  AuthManagerProtocol.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

protocol AuthManagerProtocol: AnyObject {
    func register(email: String, password: String, completion: @escaping (Result<RegisterModel, Error>) -> Void)
    func login(email: String, password: String, completion: @escaping (Result<String, Error>) -> Void)
}
