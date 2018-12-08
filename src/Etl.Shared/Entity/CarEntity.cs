using System;
using System.Collections.Generic;

namespace Etl.Shared.Entity
{
    public class CarEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Offer { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string ProductionYear { get; set; }
        public string Mileage { get; set; }
        public string Capacity { get; set; }
        public string Fuel { get; set; }
        public string HorsePower { get; set; }
        public string Transmission { get; set; }
        public string DrivingGear { get; set; }
        public string Type { get; set; }
        public string DoorsNumber { get; set; }
        public string SeatsNumber { get; set; }
        public string Colour { get; set; }
        public string IsMetallic { get; set; }
        public string Condition { get; set; }
        public string FirstRegistration { get; set; }
        public string IsRegisteredInPoland { get; set; }
        public string CountryOfOrigin { get; set; }
        public string IsFirstOwner { get; set; }
        public string NoAccidents { get; set; }
        public string ServiceHistory { get; set; }
        public string VIN { get; set; }
        public string ParticleFilter { get; set; }
        public List<string> Equipment { get; set; }
        public string Description { get; set; }

    }
}