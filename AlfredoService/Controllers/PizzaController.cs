using AlfredoService.Models;
using AlfredoService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlfredoService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController()
        {
           
        }
        //Implementaziopne metodi REST
        //GET ALL
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();
        //GET BY ID
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);
            if (pizza is null)
                return NotFound(); //corrisponde alla generazione di un errore con codice HTTP 404
            return pizza;
        }
        //POST
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            //salva una pizza e ritorna
            //IActionResult è un tipo di dato di ritorno che prevede una risposta di tipo codice HTTP.
            //è ControllerBase ad occuparsi della creazione di una richiesta http coretta con il codice corretto e di inviarla effettivamennte
            //così come è sempre ControllerBase a gestire la serializzazioen in json dei dati per l'invio e la ricezione da parte dell'API
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza); 
            //ritorna una nuova azione (HttpLocation) Get che però recuperi la nuova pizza creata (che abbia quindi l'url della pizza)
            //ATTENZIONE: è implicito che pizza sarà nel body della richiesta solo perchè il controller è marcato con l'attributo [ApiController]
        }
        //PUT
        [HttpPut]
        public IActionResult Update(int Id, Pizza pizza)
        {
            if (Id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(Id);

            if (existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);
            return NoContent(); //corrisponde a codice HTTP 204 "indicates that a request has succeeded, but that the client doesn't need to navigate away from its current page"

        }

        //DELETE
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                PizzaService.Delete(Id);
                return NoContent(); //pizza eliminata!
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
