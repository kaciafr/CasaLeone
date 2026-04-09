using UnityEngine;

namespace PnjWaves
{
    public enum ClientType { Dog, Racoon, Horse, Bear, Gorilla, Eagle }

    [CreateAssetMenu(menuName = "Client/Client Type")]
    public class ClientTypeSO : ScriptableObject
    {
        public ClientType type;
        
        [Header("Client")]
        public Sprite clientSprite;
        
        [Header("+ c ho, + il apparait souvent")]
        public float weight = 1f;

        [Header("les varients là")]
        public GameObject[] variants;
    
        public GameObject PickRandomVariant()
        {
            return variants[Random.Range(0, variants.Length)];
        }
    }
}