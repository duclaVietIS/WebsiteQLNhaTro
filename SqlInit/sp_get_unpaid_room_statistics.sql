DROP PROCEDURE IF EXISTS sp_get_unpaid_room_statistics;
DELIMITER ;
CREATE PROCEDURE sp_get_unpaid_room_statistics(IN p_apartment_id BIGINT)
BEGIN
    WITH last_month AS (
        SELECT
            CAST(DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 1 MONTH), '%Y-%m-01') AS DATE) AS first_day_last_month,
            LAST_DAY(DATE_SUB(CURRENT_DATE, INTERVAL 1 MONTH)) AS last_day_last_month
    )
    SELECT
        a.id AS ApartmentId,
        a.name AS ApartmentName,
        ar.id AS RoomId,
        ar.room_number AS RoomNumber,
        t.id AS TenantId,
        t.name AS TenantName,
        t.tel AS TenantPhone,
        t.email AS TenantEmail,
        rfc.charge_date AS ChargeDate,
        rfc.total_price AS TotalPrice,
        rfc.total_paid AS TotalPaid,
        (rfc.total_price - IFNULL(rfc.total_paid, 0)) AS RemainingDebt,
        (rfc.electricity_number_after - rfc.electricity_number_before) AS ElectricityUsed,
        (rfc.water_number_after - rfc.water_number_before) AS WaterUsed,
        rfc.electricity_number_before AS ElectricityNumberBefore,
        rfc.electricity_number_after AS ElectricityNumberAfter,
        rfc.water_number_before AS WaterNumberBefore,
        rfc.water_number_after AS WaterNumberAfter
    FROM
        room_fee_collections rfc
            INNER JOIN apartment_rooms ar ON rfc.apartment_room_id = ar.id
            INNER JOIN apartments a ON ar.apartment_id = a.id
            INNER JOIN tenants t ON rfc.tenant_id = t.id
            CROSS JOIN last_month lm
    WHERE
        rfc.charge_date >= lm.first_day_last_month
      AND rfc.charge_date <= lm.last_day_last_month
      AND IFNULL(rfc.total_paid, 0) < IFNULL(rfc.total_price, 0)
      AND (p_apartment_id IS NULL OR a.id = p_apartment_id)
    ORDER BY
        (rfc.total_price - IFNULL(rfc.total_paid, 0)) DESC;
END;

-- call sp_get_unpaid_room_statistics(NULL);
call sp_get_unpaid_room_statistics(2);