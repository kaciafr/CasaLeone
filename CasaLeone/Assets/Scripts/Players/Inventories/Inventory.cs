using System;
using System.Collections.Generic;
using ListForEat;
using UnityEngine;

namespace Inventories
{
    public class Inventory: MonoBehaviour
    {
        public List<Ingrediente> dish = new List<Ingrediente>();
        public Action<Ingrediente> TheOrder;
        public Action<Ingrediente> GiveOrder;
        private void Start()
        {
            dish.Clear();
        }

        public void AddDish(Ingrediente ingrediente)
        {
            
            if (dish.Count >= 3)
            {
                var removed = dish[0];
                dish.RemoveAt(0);
                GiveOrder?.Invoke(removed);
            }
            dish.Add(ingrediente);
            TheOrder?.Invoke(ingrediente);
            
        }

        public void RemoveDish(Ingrediente ingrediente)
        {
            dish.Remove(ingrediente);
            GiveOrder?.Invoke(ingrediente);
        }
    }
}
