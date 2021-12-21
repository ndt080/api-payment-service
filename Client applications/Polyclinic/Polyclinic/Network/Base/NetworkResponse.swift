//
//  NetworkResponse.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 21.12.21.
//

import Foundation

public enum NetworkResponse: String, Error {
    case success
    case authentificationError = "Ошибка авторизации"
    case badRequest = "Плохой запрос"
    case failed = "Ошибка"
    case noData = "Нет данных"
    case unableToDecode = "Невозможно декодировать"
}
