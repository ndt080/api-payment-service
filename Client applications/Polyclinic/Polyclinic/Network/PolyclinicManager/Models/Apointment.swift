//
//  Apointment.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

struct Apointment: Codable {
    var id: Int
    var doctorFio: String
    var patientFio: String
    var date: String
    var speciality: String
}
