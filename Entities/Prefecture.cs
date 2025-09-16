using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// Prefecture entity: represents location information (ward, district, province).
    /// </summary>
    public class Prefecture
    {
        [Key]
        public long Id { get; set; } // PK

        [MaxLength(256)]
        public string WardId { get; set; }

        [MaxLength(256)]
        public string WardName { get; set; }

        [MaxLength(256)]
        public string WardNameEn { get; set; }

        [MaxLength(256)]
        public string WardLevel { get; set; }

        [MaxLength(256)]
        public string DistrictId { get; set; }

        [MaxLength(256)]
        public string DistrictName { get; set; }

        [MaxLength(256)]
        public string ProvinceId { get; set; }

        [MaxLength(256)]
        public string ProvinceName { get; set; }
    }
}
