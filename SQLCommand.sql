CREATE TABLE sale (
    id VARCHAR(50) PRIMARY KEY,
    store_id VARCHAR(50) NOT NULL,
    product_id VARCHAR(50) NOT NULL,
    amount INTEGER,
    sale_time TIMESTAMP,
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_by VARCHAR(50) NOT NULL
);

CREATE TABLE product (
    id VARCHAR(50) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price NUMERIC(10, 2)
);

CREATE TABLE store (
    id VARCHAR(50) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    address VARCHAR(100)
);

CREATE TABLE sale_summary (
    store_id VARCHAR(50) PRIMARY KEY,
    sale_time DATE,
    price NUMERIC(10, 2),
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE member (
    account VARCHAR(50) PRIMARY KEY,
    password VARCHAR(100) NOT NULL,
    name VARCHAR(50) NOT NULL
);


SELECT * FROM sale;
SELECT * FROM product;
SELECT * FROM store;
SELECT * FROM sale_summary;
SELECT * FROM member;