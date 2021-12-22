//
//  View+Extension.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation
import SwiftUI

extension View {
    func toastApoinyment(isShowing: Binding<Bool>, text: Text, apointments: Binding<[Apointment]>) -> some View {
        ToastApointment(apointments: apointments,
                        isShowing: isShowing,
                        presenting: { self },
                        text: text)
    }

}
