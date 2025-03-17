using BM_Web.Models;
using BM_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BM_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApiService _apiService;

        public EmployeeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // List Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _apiService.GetAsync<List<Employee>>("employee");
            return View(employees);
        }

        // Create Employee (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Create Employee (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.PostAsync("employee", employee);
                if (success) return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Edit Employee (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _apiService.GetAsync<Employee>($"employee/{id}");
            if (employee == null) return NotFound();
            return View(employee);
        }

        // Edit Employee (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.PutAsync($"employee/{id}", employee);
                if (success) return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Delete Employee (GET)
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _apiService.GetAsync<Employee>($"employee/{id}");
            if (employee == null) return NotFound();
            return View(employee);
        }

        // Delete Employee (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _apiService.DeleteAsync($"employee/{id}");
            if (success) return RedirectToAction("Index");
            return View();
        }
    }
}
