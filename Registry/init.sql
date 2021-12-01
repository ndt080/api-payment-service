CREATE TABLE services (
    id serial PRIMARY KEY,
    name varchar(100) NOT NULL
);

CREATE TABLE service_methods (
    id serial PRIMARY KEY,
    method_name varchar(100) NOT NULL,
    service_id int REFERENCES services(id) NOT NULL
);

CREATE TABLE method_arguments (
    id serial PRIMARY KEY,
    arg_name varchar(50),
    method_id int REFERENCES service_methods(id)
);

INSERT INTO
    services (name)
VALUES
    ('Service1'),
    ('Service2'),
    ('Service3'),
    ('Service4'),
    ('Service5');