-- Dữ liệu mẫu cho hệ thống quản lý nhà trọ

INSERT INTO users (name, email) VALUES
('Nguyen Van A', 'a@example.com'),
('Tran Thi B', 'b@example.com');

INSERT INTO apartments (user_id, name, province_id, district_id, ward_id, address) VALUES
(1, 'Apartment A', '01', '001', '0001', '123 Main St'),
(2, 'Apartment B', '02', '002', '0002', '456 Second St');

INSERT INTO apartment_rooms (apartment_id, room_number, default_price, max_tenant) VALUES
(1, '101', 2500000, 3),
(1, '102', 2700000, 2),
(2, '201', 3000000, 4);

INSERT INTO tenants (name, tel, identity_card_number, email) VALUES
('Le Van C', '0901234567', '123456789', 'c@example.com'),
('Pham Thi D', '0907654321', '987654321', 'd@example.com');

INSERT INTO tenant_contracts (apartment_room_id, tenant_id, pay_period, price, electricity_pay_type, electricity_price, electricity_num_start, water_pay_type, water_price, water_number_start, number_of_tenant_current, start_date, end_date) VALUES
(1, 1, 1, 2500000, 1, 3500, 100, 1, 15000, 50, 1, '2025-01-01', '2025-12-31'),
(2, 2, 1, 2700000, 1, 3500, 120, 1, 15000, 60, 1, '2025-02-01', '2025-12-31');

INSERT INTO room_fee_collections (tenant_contract_id, apartment_room_id, tenant_id, electricity_number_before, electricity_number_after, water_number_before, water_number_after, charge_date, total_debt, total_price, total_paid, fee_collection_uuid) VALUES
(1, 1, 1, 100, 120, 50, 60, '2025-03-01', 0, 2550000, 2550000, 'fee-uuid-1'),
(2, 2, 2, 120, 140, 60, 70, '2025-04-01', 0, 2720000, 2720000, 'fee-uuid-2');

INSERT INTO room_fee_collection_histories (room_fee_collection_id, paid_date, price) VALUES
(1, '2025-03-02', 2550000),
(2, '2025-04-02', 2720000);

INSERT INTO water_usages (apartment_room_id, usage_number, input_date) VALUES
(1, 10, '2025-03-01'),
(2, 12, '2025-04-01');

INSERT INTO electricity_usages (apartment_room_id, usage_number, input_date) VALUES
(1, 20, '2025-03-01'),
(2, 22, '2025-04-01');

INSERT INTO contract_monthly_costs (tenant_contract_id, monthly_cost_id, pay_type, price) VALUES
(1, 1, 1, 50000),
(2, 2, 1, 60000);

INSERT INTO monthly_costs (name) VALUES
('Internet'),
('Rác'),
('Gửi xe');

INSERT INTO prefectures (ward_id, ward_name, ward_name_en, ward_level, district_id, district_name, province_id, province_name) VALUES
('0001', 'Phường 1', 'Ward 1', 'Phường', '001', 'Quận 1', '01', 'TP A'),
('0002', 'Phường 2', 'Ward 2', 'Phường', '002', 'Quận 2', '02', 'TP B');

INSERT INTO admins (admin_uuid, admin_login_id, name, email) VALUES
('admin-uuid-1', 'admin1', 'Admin One', 'admin1@example.com'),
('admin-uuid-2', 'admin2', 'Admin Two', 'admin2@example.com');
