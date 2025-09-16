using Microsoft.EntityFrameworkCore;

namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// AppDbContext: Entity Framework database context for the project.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentRoom> ApartmentRooms { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantContract> TenantContracts { get; set; }
        public DbSet<RoomFeeCollection> RoomFeeCollections { get; set; }
        public DbSet<RoomFeeCollectionHistory> RoomFeeCollectionHistories { get; set; }
        public DbSet<WaterUsage> WaterUsages { get; set; }
        public DbSet<ElectricityUsage> ElectricityUsages { get; set; }
        public DbSet<ContractMonthlyCost> ContractMonthlyCosts { get; set; }
        public DbSet<MonthlyCost> MonthlyCosts { get; set; }
        public DbSet<Prefecture> Prefectures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.Role).HasColumnName("role");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
            });
            modelBuilder.Entity<Admin>(entity => {
                entity.ToTable("admins");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.Role).HasColumnName("role");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
            });
            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("apartments");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.ProvinceId).HasColumnName("province_id");
                entity.Property(e => e.DistrictId).HasColumnName("district_id");
                entity.Property(e => e.WardId).HasColumnName("ward_id");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.ImagePath).HasColumnName("image_path");
            });
            modelBuilder.Entity<ApartmentRoom>(entity => {
                entity.ToTable("apartment_rooms");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");
                entity.Property(e => e.RoomNumber).HasColumnName("room_number");
                entity.Property(e => e.DefaultPrice).HasColumnName("default_price");
                entity.Property(e => e.MaxTenant).HasColumnName("max_tenant");
            });
            modelBuilder.Entity<Tenant>(entity => {
                entity.ToTable("tenants");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Tel).HasColumnName("tel");
                entity.Property(e => e.IdentityCardNumber).HasColumnName("identity_card_number");
                entity.Property(e => e.Email).HasColumnName("email");
            });
            modelBuilder.Entity<TenantContract>(entity => {
                entity.ToTable("tenant_contracts");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ApartmentRoomId).HasColumnName("apartment_room_id");
                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
                entity.Property(e => e.PayPeriod).HasColumnName("pay_period");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.ElectricityPayType).HasColumnName("electricity_pay_type");
                entity.Property(e => e.ElectricityPrice).HasColumnName("electricity_price");
                entity.Property(e => e.ElectricityNumStart).HasColumnName("electricity_num_start");
                entity.Property(e => e.WaterPayType).HasColumnName("water_pay_type");
                entity.Property(e => e.WaterPrice).HasColumnName("water_price");
            });
            modelBuilder.Entity<RoomFeeCollection>(entity => {
                entity.ToTable("room_fee_collections");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.TenantContractId).HasColumnName("tenant_contract_id");
                entity.Property(e => e.ApartmentRoomId).HasColumnName("apartment_room_id");
                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
                entity.Property(e => e.ElectricityNumberBefore).HasColumnName("electricity_number_before");
                entity.Property(e => e.ElectricityNumberAfter).HasColumnName("electricity_number_after");
                entity.Property(e => e.WaterNumberBefore).HasColumnName("water_number_before");
                entity.Property(e => e.WaterNumberAfter).HasColumnName("water_number_after");
                entity.Property(e => e.ChargeDate).HasColumnName("charge_date");
                entity.Property(e => e.TotalDebt).HasColumnName("total_debt");
            });
            modelBuilder.Entity<RoomFeeCollectionHistory>(entity => {
                entity.ToTable("room_fee_collection_histories");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.RoomFeeCollectionId).HasColumnName("room_fee_collection_id");
                entity.Property(e => e.PaidDate).HasColumnName("paid_date");
                entity.Property(e => e.Price).HasColumnName("price");
            });
            modelBuilder.Entity<WaterUsage>(entity => {
                entity.ToTable("water_usages");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ApartmentRoomId).HasColumnName("apartment_room_id");
                entity.Property(e => e.UsageNumber).HasColumnName("usage_number");
                entity.Property(e => e.InputDate).HasColumnName("input_date");
            });
            modelBuilder.Entity<ElectricityUsage>(entity => {
                entity.ToTable("electricity_usages");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ApartmentRoomId).HasColumnName("apartment_room_id");
                entity.Property(e => e.UsageNumber).HasColumnName("usage_number");
                entity.Property(e => e.InputDate).HasColumnName("input_date");
            });
            modelBuilder.Entity<ContractMonthlyCost>(entity => {
                entity.ToTable("contract_monthly_costs");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.TenantContractId).HasColumnName("tenant_contract_id");
                entity.Property(e => e.MonthlyCostId).HasColumnName("monthly_cost_id");
                entity.Property(e => e.PayType).HasColumnName("pay_type");
                entity.Property(e => e.Price).HasColumnName("price");
            });
            modelBuilder.Entity<MonthlyCost>(entity => {
                entity.ToTable("monthly_costs");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
            });
            modelBuilder.Entity<Prefecture>(entity => {
                entity.ToTable("prefectures");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.WardId).HasColumnName("ward_id");
                entity.Property(e => e.WardName).HasColumnName("ward_name");
                entity.Property(e => e.WardNameEn).HasColumnName("ward_name_en");
                entity.Property(e => e.WardLevel).HasColumnName("ward_level");
                entity.Property(e => e.DistrictId).HasColumnName("district_id");
                entity.Property(e => e.DistrictName).HasColumnName("district_name");
                entity.Property(e => e.ProvinceId).HasColumnName("province_id");
                entity.Property(e => e.ProvinceName).HasColumnName("province_name");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
