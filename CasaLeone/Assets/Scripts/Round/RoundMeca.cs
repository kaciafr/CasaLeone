using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Round
{
    public class RoundMeca : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pnjSpawn = new List<GameObject>();
        [SerializeField] private GameObject spawnPos;
        private int rand;
        [SerializeField] private int maxClientInTheRestaurant;

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            rand = Random.Range(1,maxClientInTheRestaurant);
            for (int i = 0; i < rand; i++)
            {
                var randomPNJ = pnjSpawn[Random.Range(0, pnjSpawn.Count)];
                Instantiate(randomPNJ, spawnPos.transform.position, Quaternion.identity);
            }
        }
    }
}
