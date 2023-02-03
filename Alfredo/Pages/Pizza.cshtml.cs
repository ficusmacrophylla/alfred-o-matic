using Alfredo.Models;
using Alfredo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Alfredo.Pages
{
    public class PizzaModel : PageModel
    {
        //PageModel della pagina Pizza, si occupa di gestire le richieste e di procedere alla verifica di validazione dei dati provenienti dalla
        //pagina e di comunicare con il servizio per la memorizzazione/aggiornamento dei dati persistenti.
        [BindProperty]
        public Pizza NewPizza { get; set; } = new();
        public List<Pizza> Pizzas = new();

        public string GlutenFreeText(Pizza pizza) //funzione che si accede dall' html come @Model.GlutenFreeText(pizza)
        {
            return pizza.IsGlutenFree ? "Gluten Free" : "Not Gluten Free";
        }
        public void OnGet()
        { //eseguito al caicamento della pagina, popola la lista dei dati con le pizze disponibili richieste al servizio
            Pizzas = PizzaService.GetAll();
        }

        public IActionResult OnPostAsync() //si può usare anche OnPost() a seconda delle necessità
        {
            if (!ModelState.IsValid)
                return Page();
            PizzaService.Add(NewPizza);
            return RedirectToAction("Get"); //aggiunge la pizza e riesegue il metodo onget
        }

        public IActionResult OnPostDelete(int id) //acceduto tramite form con tag asp-page-handler="Delete" 
        {
            PizzaService.Delete(id);
            return RedirectToAction("Get");
        }
    }
}
