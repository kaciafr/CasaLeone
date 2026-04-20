using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public int ID;
        public Sprite icon;
        public string name;
        public string description;
        public GameObject prefab;

    }
}
