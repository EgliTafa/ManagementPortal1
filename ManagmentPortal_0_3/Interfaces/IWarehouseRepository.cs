using ManagmentPortal_0_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagmentPortal_0_3.Interfaces
{
    public interface IWarehouseRepository
    {
        IEnumerable<Warehouse> Warehouses { get; }
        Warehouse GetWarehouseById(int warehouseId);
    }
}
