using Alfredo.Models;
using Alfredo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Alfredo.Pages
{
    public class NetworkModel : PageModel
    {
        WOLService service = new WOLService();

        [BindProperty]
        public Computer NewComputer { get; set; } = new();
        public List<Computer> Computers = new();
        
        public string ConnectionStatusText(Computer computer)
        {
            return computer.Status.ToString();
        }
        public void OnGet()
        {
            Computers = service.GetAll();
        }

        public IActionResult OnPostAsync() 
        {
            if (!ModelState.IsValid)
                return Page();
            service.Add(NewComputer);
            return RedirectToAction("Get"); 
        }
        public IActionResult OnPostDelete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Get");
        }
        public IActionResult OnPostCheckStatus(int id) 
        {
            service.UpdateStatus(id);
            return RedirectToAction("Get");
        }
        public IActionResult OnPostCheckStatusForAll(int id)
        {
            service.UpdateStatusForAll();
            return RedirectToAction("Get");
        }
        public IActionResult OnPostWake(int id)
        {
            service.Wake(id);
            return RedirectToAction("Get");
        }

    }
}
