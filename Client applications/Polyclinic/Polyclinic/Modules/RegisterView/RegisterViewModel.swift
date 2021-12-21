//
//  RegisterViewModel.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

class RegisterViewModel: ObservableObject  {
    
    let registerManager = RegistrationManager()
    
    func register(name: String, cost: Int, completion: @escaping (Result<ServiceModel, Error>) -> Void) {
        registerManager.register(name: name, cost: cost, completion: completion)
    }
}
