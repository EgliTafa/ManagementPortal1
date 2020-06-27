using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagmentPortal_0_3.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        public int Area { get; set; }
        public string Location { get; set; }

        [Display(Name ="Description")]
        public string Descriptiom { get; set; }
        public string Owner { get; set; }

        [Display(Name ="Number Of Workers")]
        public int NumberOfWorkers { get; set; }

        public int SectorId { get; set; }
        public virtual Sector Sector { get; set; }

        public Manager Manager { get; set; }
        public int ManagerId { get; set; }
    }
}
