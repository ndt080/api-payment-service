//
//  PlyclinicViewModel.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation
import SwiftUI

class PolyclinicViewModel: ObservableObject {
    @State var isLoading: Bool = true
    
    let polyclinicManager = PolyclinicManager()
    
    func loadApointments(completion: @escaping (Result<[Apointment], Error>) -> Void) {
        polyclinicManager.getAllApointments { result in
            switch result {
            case .success(let model):
                self.isLoading.toggle()
                completion(.success(model))
            case .failure(let error):
                self.isLoading.toggle()
                completion(.failure(error))
            }
        }
    }
    
    func makeApointment(data: Apointment, completion: @escaping (Result<Apointment, Error>) -> Void) {
        polyclinicManager.makeApointment(data: data, completion: completion)
    }
    
    func deleteApointment(data: Apointment, completion: @escaping (Result<String, Error>) -> Void) {
        polyclinicManager.deleteApointment(data: data, completion: completion)
    }
}
