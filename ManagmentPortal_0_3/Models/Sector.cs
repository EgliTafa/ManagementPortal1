using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagmentPortal_0_3.Models
{
    public class Sector
    {
        public int SectorId { get; set; }
        public string SectorName { get; set; }
        public string Description { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }
}
