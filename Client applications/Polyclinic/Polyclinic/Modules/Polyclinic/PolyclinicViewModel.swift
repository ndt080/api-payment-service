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
                completion(.success(model))
                self.isLoading.toggle()
            case .failure(let error):
                completion(.failure(error))
            }
        }
    }
}
