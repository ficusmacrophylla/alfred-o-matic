﻿using Alfredo.Models;
using System.Security.Cryptography.X509Certificates;

namespace Alfredo.Services
{
    public static class PizzaService
    {
        //servizio che implementa la parte CRUD di gestione dei dati (Create, Read, Update, Delete)
        static List<Pizza> Pizzas { get; }
        static int nextId = 3;
        static PizzaService()
        {
            Pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Classic Italian", Price = 20.00M, Size = PizzaSize.Large, IsGlutenFree = false },
                new Pizza { Id = 2, Name = "Veggie", Price = 15.00M, Size = PizzaSize.Small, IsGlutenFree = true }
            };
        }

        public static List<Pizza> GetAll() => Pizzas;
        public static Pizza? Get(int id) => Pizzas.FirstOrDefault(x => x.Id == id);

        public static void Add (Pizza pizza)
        {
            pizza.Id = nextId++;
            Pizzas.Add(pizza);
        }

        public static void Delete(int id) 
        {
            var pizza = Get(id);
            if (pizza is null)
                return;
            Pizzas.Remove(pizza);
        }

        public static void Update(Pizza pizza)
        {
            var index = Pizzas.FindIndex( p => p.Id == pizza.Id);
            if (index == -1)
                return;

            Pizzas[index] = pizza;
        }
    }
}
