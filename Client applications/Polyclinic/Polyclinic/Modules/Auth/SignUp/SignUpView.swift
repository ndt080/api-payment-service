//
//  SignUpView.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import SwiftUI

struct SignUpView: View {
    
    @Environment(\.managedObjectContext) private var viewContext
    
    @StateObject private var viewModel = SignUpViewModel()
    
    @State var email: String = ""
    @State var password: String = ""
    
    @State var isLoading: Bool = false
    
    var body: some View {
        NavigationView {
            VStack(alignment: .center) {
                Text("Polyclinic")
                    .bold()
                    .font(.italic(.title)())
                
                Spacer(minLength: 100)
                
                VStack(alignment: .center, spacing: 16.0) {
                    RoundedRectangle(cornerRadius: 100)
                        .stroke(.teal.opacity(0.7), lineWidth: 1)
                        .frame(height: 48)
                        .overlay(
                            TextField(text: $email, prompt: Text("Email")) {
                                Text("Email")
                            }
                                .fixedSize(horizontal: false,
                                           vertical: false)
                                .padding(.horizontal, 24)
                        )
                        .padding(EdgeInsets(top: 0,
                                            leading: 10,
                                            bottom: 5,
                                            trailing: 10))
                    
                    RoundedRectangle(cornerRadius: 100)
                        .stroke(.teal.opacity(0.7), lineWidth: 1)
                        .frame(height: 48)
                        .overlay(
                            SecureField(text: $password, prompt: Text("Password")) {
                                Text("Password")
                            }
                                .fixedSize(horizontal: false,
                                           vertical: false)
                                .padding(.horizontal, 24)
                        )
                        .padding(EdgeInsets(top: 0,
                                            leading: 10,
                                            bottom: 5,
                                            trailing: 10))
                }
                
                Button {
                    self.isLoading.toggle()
                    viewModel.register(email: email, password: password) { result in
                        switch result {
                        case .success:
                            self.isLoading.toggle()
                            break
                        case .failure:
                            self.isLoading.toggle()
                            break
                        }
                    }
                } label: {
                    Text("SignUp")
                        .foregroundColor(.white)
                }
                .buttonStyle(.bordered)
                .background(.teal)
                .opacity(0.9)
                .cornerRadius(20)
                .padding()
                
                if self.isLoading {
                    ProgressView()
                }
                
                Spacer(minLength: 250)
                
                NavigationLink(
                    destination: RegisterView()
                        .navigationBarTitle("")
                        .navigationBarHidden(false)
                        .navigationBarBackButtonHidden(true),
                    isActive: $viewModel.isRegistered
                ) {
                    EmptyView()
                }
                
            }
        }
    }
}

struct SignUpView_Previews: PreviewProvider {
    static var previews: some View {
        SignUpView()
    }
}
