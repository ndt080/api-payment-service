//
//  SignUpViewModel.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation
import Combine
import SwiftUI

class SignUpViewModel: ObservableObject {
  
    @Published var isRegistered = false
    
    private let authManager = AuthManager()
    private let userDefaultsManager = UserDefaultsManager()
    
    func register(email: String, password: String, completion: @escaping (Result<RegisterModel, Error>) -> Void) {
        authManager.register(email: email, password: password) { result in
            switch result {
            case .success(let model):
                print(model)
                self.userDefaultsManager.saveToken(token: model.jwtToken)
                DispatchQueue.main.async {
                    self.isRegistered.toggle()
                }
                completion(.success(model))
            case .failure(let error):
                print(error)
                completion(.failure(error))
            }
        }
    }
}
