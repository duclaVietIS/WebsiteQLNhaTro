CREATE VIEW vw_unpaid_room_statistics AS
WITH last_month AS (
    SELECT 
        DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 1 MONTH), '%Y-%m-01') as first_day_last_month,
        LAST_DAY(DATE_SUB(CURRENT_DATE, INTERVAL 1 MONTH)) as last_day_last_month
)
SELECT 
    a.id as ApartmentId,
    a.name as ApartmentName,
    ar.id as RoomId,
    ar.room_number as RoomNumber,
    t.id as TenantId,
    t.full_name as TenantName,
    t.tel as TenantPhone,
    t.email as TenantEmail,
    rfc.charge_date as ChargeDate,
    rfc.total_price as TotalPrice,
    rfc.total_paid as TotalPaid,
    (rfc.total_price - IFNULL(rfc.total_paid, 0)) as RemainingDebt,
    (rfc.electricity_number_after - rfc.electricity_number_before) as ElectricityUsed,
    (rfc.water_number_after - rfc.water_number_before) as WaterUsed
FROM 
    room_fee_collections rfc
    INNER JOIN apartment_rooms ar ON rfc.apartment_room_id = ar.id
    INNER JOIN apartments a ON ar.apartment_id = a.id
    INNER JOIN tenants t ON rfc.tenant_id = t.id
    CROSS JOIN last_month lm
WHERE 
    rfc.charge_date >= lm.first_day_last_month
    AND rfc.charge_date <= lm.last_day_last_month
    AND IFNULL(rfc.total_paid, 0) < IFNULL(rfc.total_price, 0);