//
//  PolyclinicApp.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import SwiftUI

@main
struct PolyclinicApp: App {
    let persistenceController = PersistenceController.shared

    var body: some Scene {
        WindowGroup {
            SignUpView()
                .environment(\.managedObjectContext, persistenceController.container.viewContext)
        }
    }
}
