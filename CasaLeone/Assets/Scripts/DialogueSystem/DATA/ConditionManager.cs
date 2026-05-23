using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.DATA
{
    public static class ConditionManager
    {
        private static Dictionary<string, bool> states = new Dictionary<string, bool>();
        public static event Action<string, bool> onConditionChanged;

        
        public static void SetCondition(string id, bool value)
        {
            states[id] = value;
            onConditionChanged?.Invoke(id, value);
        }

        public static bool CheckCondition(string id)
        {
            bool result = states.ContainsKey(id) && states[id];
            Debug.Log($"CheckCondition({id}) = {result}");
            return result;
        }

        public static void Clear()
        {
            states.Clear();
        }
    }
}