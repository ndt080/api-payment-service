//
//  PolyclinicManagerProtocol.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

protocol PolyclinicManagerProtocol: AnyObject {
    func getAllApointments(completion: @escaping (Result<[Apointment], Error>) -> Void)
    func makeApointment(data: Apointment, completion: @escaping (Result<String, Error>) -> Void)
}
