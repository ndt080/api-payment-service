//
//  RegisterViewModel.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

class RegisterViewModel: ObservableObject  {
    
    let registerManager = RegistrationManager()
    let defaults = UserDefaultsManager()
    
    func register(name: String, cost: Int, completion: @escaping (Result<ServiceModel, Error>) -> Void) {
        registerManager.register(name: name, cost: cost) { result in
            switch result {
            case .success(let model):
                self.defaults.saveAPIKey(key: model.apiKey)
                completion(.success(model))
            case .failure(let error):
                completion(.failure(error))
            }
        }
    }
}
