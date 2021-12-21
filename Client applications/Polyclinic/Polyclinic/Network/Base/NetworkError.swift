//
//  NetworkError.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

enum NetworkError<Error> {
    case success
    case failure(Error)
}
