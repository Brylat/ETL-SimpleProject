using System;

namespace Etl.Shared.Entity
{
    public class CarEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Model { get; set; }
    }
}