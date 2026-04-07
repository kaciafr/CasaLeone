using UnityEngine;

namespace PnjWaves
{
    [System.Serializable]
    public class WaveProfile
    {
        public string label;

        [Header("Groupes")]
        public int minGroups;
        public int maxGroups;

        [Header("Clients par groupe")]
        public int minClientsPerGroup;
        public int maxClientsPerGroup;

        [Header("Timing")]
        public float delayBetweenClients;
        public float delayBetweenGroups;

        [Header("Comportement client")]
        public float patience;
        public float eatSpeed;
    }
}