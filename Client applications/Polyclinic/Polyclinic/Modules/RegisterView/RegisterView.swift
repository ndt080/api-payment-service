//
//  RegisterView.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import SwiftUI

struct RegisterView: View {
    
    @StateObject var viewModel = RegisterViewModel()
    
    @State var serviceName: String = ""
    
    @State var paymentAmount: Int = 300
    
    @State var isSubscribed: Bool = false
    
    var body: some View {
        NavigationView {
            VStack(alignment: .center) {
                Text("Service Registration")
                    .font(.title)
                    .bold()
                    .italic()
                
                VStack(alignment: .center) {
                    TextField(text: $serviceName, prompt: Text("Service")) {
                        Text("Service")
                    }.textFieldStyle(.roundedBorder)
                    
                    Stepper {
                        Text("Payment amount: \(paymentAmount)")
                    } onIncrement: {
                        paymentAmount += 1
                    } onDecrement: {
                        if paymentAmount > 0 {
                            paymentAmount -= 1
                        }
                    }
                    
                    Button {
                        viewModel.register(name: serviceName, cost: paymentAmount) { result in
                            switch result {
                            case .success:
                                self.isSubscribed.toggle()
                            case .failure(let error):
                                print(error)
                            }
                        }
                    } label: {
                        Text("Submit")
                    }.foregroundColor(.teal)
                }
                .padding()
                .background(.gray.opacity(0.2))
                .cornerRadius(23.0)
                .padding()
                
                NavigationLink(isActive: $isSubscribed) {
                    PolyclinicView()
                } label: {
                    EmptyView()
                }

            }
        }
    }
}

struct RegisterView_Previews: PreviewProvider {
    static var previews: some View {
        RegisterView()
            .previewInterfaceOrientation(.portrait)
    }
}
