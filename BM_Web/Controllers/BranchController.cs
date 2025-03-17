using BM_Web.Models;
using BM_Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BM_Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly ApiService _apiService;

        public BranchController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // List Branches
        public async Task<IActionResult> Index()
        {
            var branches = await _apiService.GetAsync<List<Branch>>("branch");
            return View(branches);
        }

        // Create Branch (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Create Branch (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.PostAsync("branch", branch);
                if (success) return RedirectToAction("Index");
            }
            return View(branch);
        }

        // Edit Branch (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var branch = await _apiService.GetAsync<Branch>($"branch/{id}");
            if (branch == null) return NotFound();
            return View(branch);
        }

        // Edit Branch (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Branch branch)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.PutAsync($"branch/{id}", branch);
                if (success) return RedirectToAction("Index");
            }
            return View(branch);
        }

        // Delete Branch (GET)
        public async Task<IActionResult> Delete(Guid id)
        {
            var branch = await _apiService.GetAsync<Branch>($"branch/{id}");
            if (branch == null) return NotFound();
            return View(branch);
        }

        // Delete Branch (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _apiService.DeleteAsync($"branch/{id}");
            if (success) return RedirectToAction("Index");
            return View();
        }
    }
}
