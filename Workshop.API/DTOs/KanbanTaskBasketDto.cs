using System;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class KanbanTaskBasketDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region CarSpec
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string EngineDescription { get; set; }
        public int Power { get; set; }
        #endregion 
    }
}