namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// PayType enum: defines payment method for electricity/water.
    /// </summary>
    public enum PayType
    {
        /// <summary>
        /// Payment per person (e.g. 80k per person)
        /// </summary>
        PerPerson = 1,

        /// <summary>
        /// Fixed payment per room (e.g. 200k per room)
        /// </summary>
        FixedPerRoom = 2,

        /// <summary>
        /// Payment by usage (e.g. 4k per unit)
        /// </summary>
        ByUsage = 3
    }
}
