namespace Vehicles.Core.DTO
{
    public class VehicleImageDTO
    {
        public long Id { get; set; }

        public string Url { get; set; } = null!;

        public long VehicleId { get; set; }
        public VehicleDTO? Vehicle { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
