using System;
using System.Collections.Generic;
using ListForEat;

namespace Inventories
{
    public class Inventory: Singleton<Inventory>
    {
        public List<Ingrediente> ingredientes = new List<Ingrediente>();
        public Action<Ingrediente> TheOrder;
        public Action<Ingrediente> GiveOrder;
        private void Start()
        {
            ingredientes.Clear();
        }

        public void AddIngrediente(Ingrediente ingrediente)
        {
            
            if (ingredientes.Count >= 3)
            {
                var removed = ingredientes[0];
                ingredientes.RemoveAt(0);
                GiveOrder?.Invoke(removed);
            }
            ingredientes.Add(ingrediente);
            TheOrder?.Invoke(ingrediente);
            
        }

        public void RemoveIngrediente(Ingrediente ingrediente)
        {
            ingredientes.Remove(ingrediente);
            GiveOrder?.Invoke(ingrediente);
        }
    }
}
