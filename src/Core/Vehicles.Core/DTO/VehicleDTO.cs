﻿namespace Vehicles.Core.DTO
{
    public class VehicleDTO
    {
        public long Id { get; set; }

        public long ModelId { get; set; }
        public ModelDTO? Model { get; set; }

        public string? Version { get; set; }
        public FUEL FuelType { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }

        public int Year { get; set; }
        public string? Color { get; set; }
        public int Doors { get; set; }
        public TRANSMISSION Transmission { get; set; }
        public int EngineSize { get; set; }
        public int Power { get; set; }
        public string? Observations { get; set; }

        public bool Opportunity { get; set; }
        public bool Sold { get; set; }
        public DateTime? SoldDate { get; set; }

        public List<VehicleImageDTO> VehicleImages { get; set; } = new();

    }
}
