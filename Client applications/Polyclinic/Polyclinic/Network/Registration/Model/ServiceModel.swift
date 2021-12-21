//
//  ServiceModel.swift
//  Polyclinic
//
//  Created by Dzmitry Semenovich on 22.12.21.
//

import Foundation

//"id": 2,
//"serviceName": "MailingService3",
//"apiKey": "f0e4d07c07dba9999e3084c7694d447f",
//"start": "2021-12-21T22:48:56.7015803+00:00",
//"end": "2022-01-20T22:48:56.7015822+00:00"

struct ServiceModel: Codable {
    var id: Int
    var serviceName: String
    var apiKey: String
    var start: String
    var end: String
}
