-- Create database motel_db
CREATE DATABASE motel_db;
USE motel_db;

-- Script tạo các bảng cho hệ thống quản lý nhà trọ
CREATE TABLE users (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100),
    email VARCHAR(256),
    password_hash VARCHAR(256) NOT NULL,
    role VARCHAR(32) NOT NULL,
    is_active BOOLEAN DEFAULT TRUE
);

CREATE TABLE apartments (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    user_id BIGINT,
    name VARCHAR(45),
    province_id VARCHAR(256),
    district_id VARCHAR(256),
    ward_id VARCHAR(256),
    address VARCHAR(256),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE apartment_rooms (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    apartment_id BIGINT,
    room_number VARCHAR(45),
    default_price BIGINT,
    max_tenant BIGINT,
    FOREIGN KEY (apartment_id) REFERENCES apartments(id)
);

CREATE TABLE tenants (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45),
    tel VARCHAR(45),
    identity_card_number VARCHAR(45),
    email VARCHAR(256)
);

CREATE TABLE tenant_contracts (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    apartment_room_id BIGINT,
    tenant_id BIGINT,
    pay_period BIGINT,
    price DECIMAL(18,2),
    electricity_pay_type BIGINT,
    electricity_price DECIMAL(18,2),
    electricity_num_start BIGINT,
    water_pay_type BIGINT,
    water_price DECIMAL(18,2),
    water_number_start BIGINT,
    number_of_tenant_current BIGINT,
    start_date DATETIME,
    end_date DATETIME,
    FOREIGN KEY (apartment_room_id) REFERENCES apartment_rooms(id),
    FOREIGN KEY (tenant_id) REFERENCES tenants(id)
);

CREATE TABLE room_fee_collections
(
    id                        BIGINT PRIMARY KEY AUTO_INCREMENT,
    tenant_contract_id        BIGINT,
    apartment_room_id         BIGINT,
    tenant_id                 BIGINT,
    electricity_number_before BIGINT,
    electricity_number_after  BIGINT,
    image_electric_path       VARCHAR(1024),
    water_number_before       BIGINT,
    water_number_after        BIGINT,
    image_water_path          VARCHAR(1024),
    charge_date               DATETIME,
    total_debt                DECIMAL(18, 2),
    total_price               DECIMAL(18, 2),
    total_paid                DECIMAL(18, 2),
    fee_collection_uuid       VARCHAR(64),
    FOREIGN KEY (tenant_contract_id) REFERENCES tenant_contracts (id),
    FOREIGN KEY (apartment_room_id) REFERENCES apartment_rooms (id),
    FOREIGN KEY (tenant_id) REFERENCES tenants (id)
);

CREATE TABLE room_fee_collection_histories (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    room_fee_collection_id BIGINT,
    paid_date DATETIME,
    price DECIMAL(18,2),
    FOREIGN KEY (room_fee_collection_id) REFERENCES room_fee_collections(id)
);

CREATE TABLE water_usages (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    apartment_room_id BIGINT,
    usage_number BIGINT,
    input_date DATETIME,
    FOREIGN KEY (apartment_room_id) REFERENCES apartment_rooms(id)
);

CREATE TABLE electricity_usages (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    apartment_room_id BIGINT,
    usage_number BIGINT,
    input_date DATETIME,
    FOREIGN KEY (apartment_room_id) REFERENCES apartment_rooms(id)
);

CREATE TABLE contract_monthly_costs (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    tenant_contract_id BIGINT,
    monthly_cost_id BIGINT,
    pay_type BIGINT,
    price DECIMAL(18,2),
    FOREIGN KEY (tenant_contract_id) REFERENCES tenant_contracts(id),
    FOREIGN KEY (monthly_cost_id) REFERENCES monthly_costs(id)
);

CREATE TABLE monthly_costs (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45)
);

CREATE TABLE prefectures (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    ward_id VARCHAR(256),
    ward_name VARCHAR(256),
    ward_name_en VARCHAR(256),
    ward_level VARCHAR(256),
    district_id VARCHAR(256),
    district_name VARCHAR(256),
    province_id VARCHAR(256),
    province_name VARCHAR(256)
);

CREATE TABLE admins (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    admin_uuid VARCHAR(64),
    admin_login_id VARCHAR(64),
    name VARCHAR(45),
    email VARCHAR(256)
);
