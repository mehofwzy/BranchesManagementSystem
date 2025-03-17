using BM_Web.Models;
using BM_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BM_Web.Controllers
{
    public class MaintenanceRequestController : Controller
    {
        private readonly ApiService _apiService;

        public MaintenanceRequestController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // List all maintenance requests
        public async Task<IActionResult> Index()
        {
            var requests = await _apiService.GetAsync<List<MaintenanceRequest>>("maintenancerequest");
            return View(requests);
        }

        // Create maintenance request (GET)
        public async Task<IActionResult> Create()
        {
            await PopulateBranchesDropdown();
            return View();
        }

        // Create maintenance request (POST)
        [HttpPost]
        public async Task<IActionResult> Create(MaintenanceRequest request)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.PostAsync("maintenancerequest", request);
                if (success) return RedirectToAction("Index");
            }
            await PopulateBranchesDropdown();
            return View(request);
        }

        // Edit maintenance request (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var request = await _apiService.GetAsync<MaintenanceRequest>($"maintenancerequest/{id}");
            if (request == null) return NotFound();

            await PopulateBranchesDropdown();
            return View(request);
        }

        // Edit maintenance request (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, MaintenanceRequest request)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.PutAsync($"maintenancerequest/{id}", request);
                if (success) return RedirectToAction("Index");
            }
            await PopulateBranchesDropdown();
            return View(request);
        }

        // Delete maintenance request (GET)
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = await _apiService.GetAsync<MaintenanceRequest>($"maintenancerequest/{id}");
            if (request == null) return NotFound();
            return View(request);
        }

        // Delete maintenance request (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _apiService.DeleteAsync($"maintenancerequest/{id}");
            if (success) return RedirectToAction("Index");
            return View();
        }

        // Helper method to populate branch dropdown list
        private async Task PopulateBranchesDropdown()
        {
            var branches = await _apiService.GetAsync<List<Branch>>("branch");
            ViewBag.Branches = new SelectList(branches, "Id", "Name");
        }
    }
}
