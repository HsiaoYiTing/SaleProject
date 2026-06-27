CREATE TABLE sale (
    id VARCHAR(50) PRIMARY KEY,
    store_id VARCHAR(50) NOT NULL,
    product_id VARCHAR(50) NOT NULL,
    price NUMERIC(10, 2),
    qty INTEGER,
    sale_time TIMESTAMP,
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_by VARCHAR(50) NOT NULL
);

CREATE TABLE sale_temp (
    id VARCHAR(50) PRIMARY KEY,
    store_id VARCHAR(50) NOT NULL,
    product_name VARCHAR(50) NOT NULL,
    price NUMERIC(10, 2),
    qty INTEGER,
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

CREATE TABLE sale_sum (
    id VARCHAR(50) PRIMARY KEY,
    store_id VARCHAR(50),
    sale_time DATE ,
    price NUMERIC(10, 2),
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE member (
    account VARCHAR(50) PRIMARY KEY,
    password VARCHAR(100) NOT NULL,
    name VARCHAR(50) NOT NULL
);

INSERT INTO sale (id, store_id, product_id, qty, price, sale_time, update_by) VALUES('1','123331', '111', 5,50, '2026-06-25 13:14:58.007729', 'OL');
INSERT INTO sale (id, store_id, product_id, qty, price, sale_time, update_by) VALUES('2','123331', '222', 10, 350, '2026-06-25 14:14:58.007729', 'OL');
INSERT INTO sale (id, store_id, product_id, qty, price, sale_time, update_by) VALUES('3','123331', '333', 1, 59, '2026-06-25 15:14:58.007729', 'OL');
INSERT INTO sale (id, store_id, product_id, qty, price, sale_time, update_by) VALUES('4','123331', '4444', 3, 120, '2026-06-24 15:14:58.007729', 'OL');

INSERT INTO product (id, name, price) VALUES('000010', '麥香奶茶', 10);
INSERT INTO product (id, name, price) VALUES('000100', '乖乖椰子大', 35);
INSERT INTO product (id, name, price) VALUES('001000', '來一客海鮮泡麵', 59);
INSERT INTO product (id, name, price) VALUES('010000', 'MM巧克力', 40);

INSERT INTO store (id, name, address) VALUES('123331', '門市A', '台北');
INSERT INTO store (id, name, address) VALUES('123332', '門市B', '新北');
INSERT INTO store (id, name, address) VALUES('123333', '門市C', '桃園');

INSERT INTO sale_sum (id, store_id, sale_time, price) VALUES('12333120260621','123331', '2026-06-21', 500);
INSERT INTO sale_sum (id, store_id, sale_time, price) VALUES('12333120260620','123331', '2026-06-20', 2000);
INSERT INTO sale_sum (id, store_id, sale_time, price) VALUES('12333220260620','123332', '2026-06-20', 500);
INSERT INTO sale_sum (id, store_id, sale_time, price) VALUES('12333320260621','123333', '2026-06-21', 1500);

SELECT * FROM sale;
SELECT * FROM sale_temp;
SELECT * FROM product;
SELECT * FROM store;
SELECT * FROM sale_sum;
SELECT * FROM member;

SELECT * FROM sale_sum LEFT JOIN store s on sale_sum.store_id = s.id  WHERE store_id = '123331' ;

SELECT * FROM sale
    LEFT JOIN product p ON sale.product_id = p.id
    LEFT JOIN store s ON sale.store_id = s.id
    WHERE  sale_time >= '2026-06-25 00:00:00' AND sale_time <= '2026-06-25 23:59:59' AND store_Id = '123331';


INSERT INTO sale_sum (id, store_id, sale_time, price )
VALUES ('12333120260621', '123331', '2026-06-21', 400)
ON CONFLICT (id)
DO UPDATE
SET price = 12000, create_time = CURRENT_TIMESTAMP
RETURNING id;


SELECT * FROM sale
LEFT JOIN product p ON sale.product_id = p.id
LEFT JOIN store s ON sale.store_id = s.id
WHERE sale_time >= '2026-05-01 00:00:00' AND sale_time <= '2026-05-02 00:00:00' AND store_Id = '123331'