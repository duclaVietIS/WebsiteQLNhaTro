create table admins
(
    id             bigint auto_increment
        primary key,
    admin_uuid     varchar(64)  null,
    admin_login_id varchar(64)  null,
    name           varchar(45)  null,
    email          varchar(256) null
);

create table monthly_costs
(
    id   bigint auto_increment
        primary key,
    name varchar(45) null
);

create table prefectures
(
    id            bigint auto_increment
        primary key,
    ward_id       varchar(256) null,
    ward_name     varchar(256) null,
    ward_name_en  varchar(256) null,
    ward_level    varchar(256) null,
    district_id   varchar(256) null,
    district_name varchar(256) null,
    province_id   varchar(256) null,
    province_name varchar(256) null
);

create table tenants
(
    id                   bigint auto_increment
        primary key,
    name                 varchar(45)  null,
    tel                  varchar(45)  null,
    identity_card_number varchar(45)  null,
    email                varchar(256) null
);

create table users
(
    id            bigint auto_increment
        primary key,
    name          varchar(100)         null,
    email         varchar(256)         null,
    password_hash varchar(256)         not null,
    role          varchar(32)          not null,
    is_active     tinyint(1) default 1 null
);

create table apartments
(
    id          bigint auto_increment
        primary key,
    user_id     bigint        null,
    name        varchar(45)   null,
    province_id varchar(256)  null,
    district_id varchar(256)  null,
    ward_id     varchar(256)  null,
    address     varchar(256)  null,
    image_path  varchar(1024) null,
    constraint apartments_ibfk_1
        foreign key (user_id) references users (id)
);

create table apartment_rooms
(
    id            bigint auto_increment
        primary key,
    apartment_id  bigint                      null,
    room_number   varchar(45)                 null,
    default_price decimal(18, 2) default 0.00 null,
    max_tenant    bigint                      null,
    constraint apartment_rooms_ibfk_1
        foreign key (apartment_id) references apartments (id)
);

create index apartment_id
    on apartment_rooms (apartment_id);

create index user_id
    on apartments (user_id);

create table electricity_usages
(
    id                bigint auto_increment
        primary key,
    apartment_room_id bigint   null,
    usage_number      bigint   null,
    input_date        datetime null,
    constraint electricity_usages_ibfk_1
        foreign key (apartment_room_id) references apartment_rooms (id)
);

create index apartment_room_id
    on electricity_usages (apartment_room_id);

create table tenant_contracts
(
    id                       bigint auto_increment
        primary key,
    apartment_room_id        bigint         null,
    tenant_id                bigint         null,
    pay_period               bigint         null,
    price                    decimal(18, 2) null,
    electricity_pay_type     bigint         null,
    electricity_price        decimal(18, 2) null,
    electricity_num_start    bigint         null,
    water_pay_type           bigint         null,
    water_price              decimal(18, 2) null,
    water_number_start       bigint         null,
    number_of_tenant_current bigint         null,
    start_date               datetime       null,
    end_date                 datetime       null,
    constraint tenant_contracts_ibfk_1
        foreign key (apartment_room_id) references apartment_rooms (id),
    constraint tenant_contracts_ibfk_2
        foreign key (tenant_id) references tenants (id)
);

create table contract_monthly_costs
(
    id                 bigint auto_increment
        primary key,
    tenant_contract_id bigint         null,
    monthly_cost_id    bigint         null,
    pay_type           bigint         null,
    price              decimal(18, 2) null,
    constraint contract_monthly_costs_ibfk_1
        foreign key (tenant_contract_id) references tenant_contracts (id),
    constraint contract_monthly_costs_ibfk_2
        foreign key (monthly_cost_id) references monthly_costs (id)
);

create index monthly_cost_id
    on contract_monthly_costs (monthly_cost_id);

create index tenant_contract_id
    on contract_monthly_costs (tenant_contract_id);

create table room_fee_collections
(
    id                        bigint auto_increment
        primary key,
    tenant_contract_id        bigint         null,
    apartment_room_id         bigint         null,
    tenant_id                 bigint         null,
    electricity_number_before bigint         null,
    electricity_number_after  bigint         null,
    image_electric_path       varchar(1024)  null,
    water_number_before       bigint         null,
    water_number_after        bigint         null,
    image_water_path          varchar(1024)  null,
    charge_date               datetime       null,
    total_debt                decimal(18, 2) null,
    total_price               decimal(18, 2) null,
    total_paid                decimal(18, 2) null,
    fee_collection_uuid       varchar(64)    null,
    constraint room_fee_collections_ibfk_1
        foreign key (tenant_contract_id) references tenant_contracts (id),
    constraint room_fee_collections_ibfk_2
        foreign key (apartment_room_id) references apartment_rooms (id),
    constraint room_fee_collections_ibfk_3
        foreign key (tenant_id) references tenants (id)
);

create table room_fee_collection_histories
(
    id                     bigint auto_increment
        primary key,
    room_fee_collection_id bigint         null,
    paid_date              datetime       null,
    price                  decimal(18, 2) null,
    constraint room_fee_collection_histories_ibfk_1
        foreign key (room_fee_collection_id) references room_fee_collections (id)
);

create index room_fee_collection_id
    on room_fee_collection_histories (room_fee_collection_id);

create index apartment_room_id
    on room_fee_collections (apartment_room_id);

create index tenant_contract_id
    on room_fee_collections (tenant_contract_id);

create index tenant_id
    on room_fee_collections (tenant_id);

create index apartment_room_id
    on tenant_contracts (apartment_room_id);

create index tenant_id
    on tenant_contracts (tenant_id);

create table water_usages
(
    id                bigint auto_increment
        primary key,
    apartment_room_id bigint   null,
    usage_number      bigint   null,
    input_date        datetime null,
    constraint water_usages_ibfk_1
        foreign key (apartment_room_id) references apartment_rooms (id)
);

create index apartment_room_id
    on water_usages (apartment_room_id);

