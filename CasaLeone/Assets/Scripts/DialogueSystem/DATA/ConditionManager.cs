using System.Collections.Generic;

public static class ConditionManager
{
    private static Dictionary<string, bool> states = new Dictionary<string, bool>();

    public static void SetCondition(string id, bool value)
    {
        states[id] = value;
    }

    public static bool CheckCondition(string id)
    {
        return states.ContainsKey(id) && states[id];
    }

    public static void Clear()
    {
        states.Clear();
    }
}