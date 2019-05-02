using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KLoveDemo.Models;

namespace KLoveDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            var employees = from s in _context.Employees
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.Department.Contains(searchString)
                                       || s.Department.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.Department);
                    break;
                default:
                    employees = employees.OrderBy(s => s.Department);
                    break;
            }
            return View(await employees.AsNoTracking().ToListAsync());
        }

        // GET: Employee/Create
        public IActionResult AddOrEdit(int id=0)
        {
            if(id == 0)
                return View(new Employee());
            else
                return View(_context.Employees.Find(id));
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,Department,FullName,PhoneNum,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if(employee.EmployeeId == 0)
                {
                    _context.Add(employee);
                }
                else
                {
                    _context.Update(employee);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
