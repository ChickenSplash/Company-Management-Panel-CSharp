using Company_Management_Panel_CSharp.Data;
using Company_Management_Panel_CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Company_Management_Panel_CSharp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public CompaniesController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Logo,Website")] Company company)
        {

            if (ModelState.IsValid)
            {
                var ext = Path.GetExtension(company.Logo.FileName).ToLowerInvariant();
                
                if (!IsValidImage(company.Logo, out var errorMessage, ext))
                {
                    ModelState.AddModelError("Logo", errorMessage);
                    return View(company);
                }
                else
                {
                    // --- Generate unique filename ---
                    var fileName = $"{Guid.NewGuid()}{ext}";

                    // --- Combine path to wwwroot/images directory ---
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    // --- Combine full path ---
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // --- Save the file ---
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await company.Logo.CopyToAsync(stream);
                    }

                    // --- Save relative path for the database
                    company.LogoPath = fileName;
                }

                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Logo,Website")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var ext = Path.GetExtension(company.Logo.FileName).ToLowerInvariant();
                
                if (!IsValidImage(company.Logo, out var errorMessage, ext))
                {
                    ModelState.AddModelError("Logo", errorMessage);
                    return View(company);
                } 
                else
                {
                    // Retrieve the existing company from the database
                    var existingCompany = await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                    if (existingCompany == null)
                    {
                        return NotFound();
                    }

                    DeleteLogoFile(existingCompany.LogoPath);

                    var fileName = $"{Guid.NewGuid()}{ext}";
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await company.Logo.CopyToAsync(stream);
                    }

                    company.LogoPath = fileName;
                }

                try
                {
                    _context.Entry(company).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                // Take company.LogoPath (e.g. "a124303a-b0a9-480b-932c-4825df20add0.png") and delete the file found inside of wwwroot/images/
                // Do nothing if company.LogoPath doesnt exist
                DeleteLogoFile(company.LogoPath);

                _context.Companies.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsValidImage(IFormFile? file, out string errorMessage, string? ext)
        {
            errorMessage = string.Empty;

            if (file == null || file.Length == 0)
            {
                errorMessage = "No file uploaded.";
                return false;
            }

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                errorMessage = "Only image files (.jpg, .png, .gif, .bmp, .webp) are allowed.";
                return false;
            }

            if (!file.ContentType.StartsWith("image/"))
            {
                errorMessage = "Invalid file type.";
                return false;
            }

            return true;
        }

        private void DeleteLogoFile(string? logoPath)
        {
            if (string.IsNullOrEmpty(logoPath)) return;

            var fileName = Path.GetFileName(logoPath);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
