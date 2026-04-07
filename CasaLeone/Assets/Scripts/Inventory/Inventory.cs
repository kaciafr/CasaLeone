using System.Collections.Generic;
using ListForEat;

namespace Inventory
{
    public class Inventory: Singleton<Inventory>
    {
        public List<Ingrediente> ingredientes = new List<Ingrediente>();

        private void Start()
        {
            ingredientes.Clear();
        }

        public void AddIngrediente(Ingrediente ingrediente)
        {
            ingredientes.Add(ingrediente);
        }

        public void RemoveIngrediente(Ingrediente ingrediente)
        {
            ingredientes.Remove(ingrediente);
        }
    }
}
