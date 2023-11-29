using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Technocitt.Database.Tables;
using Technocitt.Models;
using Technocitt.Services;
using Technocitt.ViewModels;

namespace Technocitt.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DashboardController : Controller
    {
        IPropertiesProvider propertiesProvider;
        public DashboardController(IPropertiesProvider _propertiesProvider)
        {
            this.propertiesProvider = _propertiesProvider;
        }
         

        public async Task<IActionResult> Index()
        { 
            PropertiesViewModel model = new PropertiesViewModel();
            var   list = await propertiesProvider.getAllProperties();

            foreach (var item in list)
            {
                model.Properties.Add(new MyProperty { ID = item.ID, Name = item.Name });
            }
              
            return View(model);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create(MyProperty model)
        {
            if (ModelState.IsValid)
            { 
                await propertiesProvider.create(model.Name);
                TempData["SuccessMessage"] = "Property was added successfully";
                return RedirectToAction("Index");
            }else
            {
                return View();
            } 
        }

        [HttpGet]
        public async Task<IActionResult> edit(int id)
        {
            var property = await propertiesProvider.getProperty(id);
            if (property != null)
            {
                MyProperty model = new MyProperty { ID = property.ID, Name = property.Name };
                return View(model);
            }
            else
            {
                TempData["ErrorMessage"] = "Property not found";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> update(MyProperty model)
        {
            if (ModelState.IsValid)
            {
                await propertiesProvider.update(model.ID, model.Name);
                TempData["SuccessMessage"] = "Property was updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> delete(int id)
        { 
             propertiesProvider.delete(id);
            TempData["SuccessMessage"] = "Property deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
