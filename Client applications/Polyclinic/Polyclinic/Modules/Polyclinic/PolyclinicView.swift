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
    
    @State var makeApointment: Bool = false
    
    let defaults = UserDefaultsManager()
    
    var body: some View {
        NavigationView {
            HStack(alignment: .center) {
                ZStack(alignment: .center) {
                    if viewModel.isLoading {
                        if apointments.isEmpty {
                            HStack(alignment: .center) {
                                Text("Apointments list is empty!")
                            }
                        } else {
                            List{
                                ForEach(apointments) { item in
                                    Text("\(item.patientFio) - \(item.doctorFio)\n \(item.date)")
                                }.onDelete(perform: deleteItems)
                            }.listStyle(.grouped)
                                
                        }
                    } else {
                        ProgressView()
                    }
                }
                
            }
            .toolbar {
                ToolbarItem(placement: .navigationBarTrailing) {
                    EditButton()
                }
                ToolbarItem {
                    Button {
                        makeApointment.toggle()
                    } label: {
                        Label("Add Item", systemImage: "plus")
                    }
                }
            }
        }
        .toastApoinyment(isShowing: $makeApointment, text: Text("Add new apointment"), apointments: $apointments)
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
    
    private func deleteItems(offsets: IndexSet) {
        withAnimation {
            offsets.map { apointments[$0] }.forEach { apointment in
                if let idx = apointments.firstIndex(where: { $0.id == apointment.id}) {
                    apointments.remove(at: idx)
                    viewModel.deleteApointment(data: apointment) { _ in}
                }
            }
        }
    }
}

struct ToastApointment<Presenting>: View where Presenting: View {

    @ObservedObject var viewModel = PolyclinicViewModel()

    @Binding var apointments: [Apointment]
    
    @State var specialist: String = ""
    
    @State var pacient: String = ""
    
    @State var specialty: String = ""
    
    @State var apointment: Date = Date()
    
    /// The binding that decides the appropriate drawing in the body.
    @Binding var isShowing: Bool
    /// The view that will be "presenting" this toast
    let presenting: () -> Presenting
    /// The text to show
    let text: Text

    var body: some View {

        GeometryReader { geometry in

            ZStack(alignment: .center) {

                self.presenting()
                    .blur(radius: self.isShowing ? 1 : 0)

                VStack(alignment: .center, spacing: 0.0) {
                    self.text
                        .bold()
                        .font(.title)
                    TextField(text: $specialist, prompt: Text("Specialist's name")) {
                        EmptyView()
                    }
                    .textFieldStyle(.roundedBorder)
                    .padding()
                    
                    TextField(text: $pacient, prompt: Text("Your name")) {
                        EmptyView()
                    }
                    .textFieldStyle(.roundedBorder)
                    .padding()
                    
                    TextField(text: $specialty, prompt: Text("Speciality")) {
                        EmptyView()
                    }
                    .textFieldStyle(.roundedBorder)
                    .padding()
                    
                    DatePicker("Select date of apointment", selection: $apointment,
                               displayedComponents: [.date, .hourAndMinute])
                        .datePickerStyle(.graphical)
                        .tint(.teal)
                    
                    Button {
                        self.viewModel.makeApointment(data: Apointment(id: 0,
                                                                       doctorFio: specialist,
                                                                       patientFio: pacient,
                                                                       date: itemFormatter.string(from: apointment), speciality: specialty)) { result in
                            switch result {
                            case .success(let model):
                                apointments.append(model)
                                self.isShowing.toggle()
                            case .failure(let error):
                                self.isShowing.toggle()
                            }
                        }
                    } label: {
                        Text("Make apointment")
                    }
                    .buttonStyle(.bordered)
                    .background(.teal)
                    .cornerRadius(13.0)
                }
                .frame(width: geometry.size.width,
                       height: geometry.size.height)
                .background(Color.secondary.colorInvert())
                .foregroundColor(Color.primary.opacity(0.4))
                .cornerRadius(20)
                .transition(.slide)
                .opacity(self.isShowing ? 1 : 0)
            }
        }
    }
}

private let itemFormatter: DateFormatter = {
    let formatter = DateFormatter()
    formatter.calendar = Calendar(identifier: .iso8601)
    formatter.locale = Locale(identifier: "en_US_POSIX")
    formatter.timeZone = TimeZone(secondsFromGMT: 0)
    formatter.dateFormat = "yyyy-MM-dd'T'HH:mm:ss.SSSXXXXX"
    return formatter
}()

struct PolyclinicView_Previews: PreviewProvider {
    static var previews: some View {
        PolyclinicView()
    }
}
