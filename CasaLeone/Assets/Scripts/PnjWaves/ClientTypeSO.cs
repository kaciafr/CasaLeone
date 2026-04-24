using UnityEngine;

namespace PnjWaves
{
    public enum ClientType { Dog, Racoon, Horse, Bear, Gorilla, Eagle }

    public enum ClientPersonality { Basic, BadJoke, Punk, Mean, Unmotivated, Humiliated }

    [CreateAssetMenu(menuName = "Client/Client Type")]
    public class ClientTypeSO : ScriptableObject
    {
        public ClientType type;
        
        [Header("Client")]
        public Sprite clientSprite;
        
        [Header("+ c ho, + il apparait souvent")]
        public float weight = 1f;

        public ClientPersonality personality;

        [Header("Variants visuels")]
        public GameObject[] variants;
        
        [Header("Répliques — impatient")]
        public string[] impatientLines;   

        [Header("Répliques — content")]
        public string[] satisfiedLines;  
        
        [Header("Répliques — en colère")]
        public string[] angryLines;
        
        public int idGroupe;

        public GameObject PickRandomVariant()
        {
            if (variants == null || variants.Length == 0) return null;
            return variants[Random.Range(0, variants.Length)];
        }

        public string PickLine(string[] lines)
        {
            if (lines == null || lines.Length == 0) return "";
            return lines[Random.Range(0, lines.Length)];
        }
        
        public string PickImpatientLine() => PickLine(impatientLines);
        public string PickSatisfiedLine() => PickLine(satisfiedLines);
        public string PickAngryLine()     => PickLine(angryLines);
    }
}