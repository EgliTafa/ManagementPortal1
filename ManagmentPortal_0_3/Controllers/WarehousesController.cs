using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagmentPortal_0_3.Models;
using System.Data;
using OfficeOpenXml;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ManagmentPortal_0_3.Controllers
{
    [Authorize]
    public class WarehousesController : Controller
    {
        private readonly ManagmentPortal_0_3Context _context;

        public WarehousesController(ManagmentPortal_0_3Context context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            var managmentPortal_0_3Context = _context.Warehouse.Include(w => w.Manager).Include(w => w.Sector);
            return View(await managmentPortal_0_3Context.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse
                .Include(w => w.Manager)
                .Include(w => w.Sector)
                .FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Set<Manager>(), "ManagerId", "ManagerId");
            ViewData["SectorId"] = new SelectList(_context.Set<Sector>(), "SectorId", "SectorId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehouseId,Area,Location,Descriptiom,Owner,NumberOfWorkers,SectorId,ManagerId")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Set<Manager>(), "ManagerId", "ManagerId", warehouse.ManagerId);
            ViewData["SectorId"] = new SelectList(_context.Set<Sector>(), "SectorId", "SectorId", warehouse.SectorId);
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Set<Manager>(), "ManagerId", "ManagerId", warehouse.ManagerId);
            ViewData["SectorId"] = new SelectList(_context.Set<Sector>(), "SectorId", "SectorId", warehouse.SectorId);
            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseId,Area,Location,Descriptiom,Owner,NumberOfWorkers,SectorId,ManagerId")] Warehouse warehouse)
        {
            if (id != warehouse.WarehouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.WarehouseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Set<Manager>(), "ManagerId", "ManagerId", warehouse.ManagerId);
            ViewData["SectorId"] = new SelectList(_context.Set<Sector>(), "SectorId", "SectorId", warehouse.SectorId);
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse
                .Include(w => w.Manager)
                .Include(w => w.Sector)
                .FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouse = await _context.Warehouse.FindAsync(id);
            _context.Warehouse.Remove(warehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouse.Any(e => e.WarehouseId == id);
        }

        public void ExportToExcel()
        {
            List<Warehouse> emplist = _context.Warehouse.Select(x => new Warehouse
            {
                WarehouseId = x.WarehouseId,
                Area = x.Area,
                Location = x.Location,
                Descriptiom = x.Descriptiom,
                Owner = x.Owner,
                NumberOfWorkers = x.NumberOfWorkers,
                //SectorId = x.SectorId,
                Sector = x.Sector,
                Manager = x.Manager,
                //ManagerId = x.ManagerId
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            //ws.Cells["A1"].Value = "Communication";
            //ws.Cells["B1"].Value = "Com1";

            //ws.Cells["A2"].Value = "Report";
            //ws.Cells["B2"].Value = "Report1";

            //ws.Cells["A3"].Value = "Date";
            //ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A1"].Value = "WarehouseId";
            ws.Cells["B1"].Value = "Area";
            ws.Cells["C1"].Value = "Location";
            ws.Cells["D1"].Value = "Descriptiom";
            ws.Cells["E1"].Value = "Owner";
            ws.Cells["F1"].Value = "NumberOfWorkers";
            //ws.Cells["G1"].Value = "SectorId";
            ws.Cells["G1"].Value = "Sector";
            //ws.Cells["I1"].Value = "ManagerId";
            ws.Cells["H1"].Value = "Manager";

            int rowStart = 7;
            foreach (var item in emplist)
            {
                if (item.WarehouseId > 10)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.WarehouseId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Area;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Location;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Descriptiom;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Owner;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.NumberOfWorkers;
                //ws.Cells[string.Format("G{0}", rowStart)].Value = item.SectorId;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Sector.SectorName;
                //ws.Cells[string.Format("I{0}", rowStart)].Value = item.ManagerId;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Manager.ManagerName;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.Body.WriteAsync(pck.GetAsByteArray());
            Response.Body.Close();

        }
    }
}
