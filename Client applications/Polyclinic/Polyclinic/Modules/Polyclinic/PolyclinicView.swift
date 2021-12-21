//
//  LoginView.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import SwiftUI

struct PolyclinicView: View {
    
    @StateObject private var viewModel = PolyclinicViewModel()
    
    @State var apointments = [Apointment]()
    
    let defaults = UserDefaultsManager()
    
    var body: some View {
        VStack(alignment: .center, spacing: 10.0) {
            ZStack {
                if !viewModel.isLoading || !apointments.isEmpty {
                    List(apointments, id: \.id) { item in
                        Text("\(item.patientFio) - \(item.doctorFio)\n \(item.date)")
                    }.listStyle(.grouped)
                } else {
                    ProgressView()
                }
            }
        }
        .navigationTitle("jwt: \(defaults.getToken())")
        .onAppear {
            loadApointments()
        }
    }
    
    private func loadApointments() {
        viewModel.loadApointments { result in
            switch result {
            case .success(let model):
                self.apointments += model
            case .failure(let error):
                break
            }
        }
    }
}

struct PolyclinicView_Previews: PreviewProvider {
    static var previews: some View {
        PolyclinicView()
    }
}
