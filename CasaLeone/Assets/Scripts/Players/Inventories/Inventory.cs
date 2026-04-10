using System;
using System.Collections.Generic;
using Restaurants;
using UnityEngine;

namespace Players.Inventories
{
    [Serializable]
    public class Inventory 
    {
        private const int MaxDish = 3;
        public event Action<Dish> OnDishAdded;
        public event Action<Dish> OnDishRemoved;
        
        
        [SerializeField]
        private List<Dish> dishes = new List<Dish>();

        
        public bool Contains(Dish dish) => dishes.Contains(dish);
        
        public void AddDish(Dish dish)
        {
            dishes.Add(dish);
            OnDishAdded?.Invoke(dish);
            
            if (dishes.Count >= MaxDish)
                RemoveDish(dishes[0]);
        }

        public void RemoveDish(Dish dish)
        {
            if (dishes.Remove(dish))
            {
                OnDishRemoved?.Invoke(dish);
            }
        }
    }
}
