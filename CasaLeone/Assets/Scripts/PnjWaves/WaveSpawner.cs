using System.Collections;
using System.Collections.Generic;
using Pnj;
using UnityEngine;

namespace PnjWaves
{
    public class WaveSpawner : MonoBehaviour
    {
        [Header("Profils de vague (0 = pressés, 1 = nombreux)")]
        public WaveProfile[] profiles = new WaveProfile[2];

        [Header("Types de clients (un asset par type)")]
        public ClientTypeSO[] clientTypes;

        [Header("Spawn")]
        public Transform spawnPoint;
        public float waitBetweenWaves = 5f;

        public int CurrentWave { get; private set; } = 0;
        public int BestWave { get; private set; } = 0;

        private const string BEST_WAVE_KEY = "BestWave";
        private List<GameObject> activeClients = new List<GameObject>();

        // ─────────────────────────────────────────────
        //  Démarrage
        // ─────────────────────────────────────────────

        void Start()
        {
            BestWave = PlayerPrefs.GetInt(BEST_WAVE_KEY, 0);
            StartCoroutine(RunWaves());
        }

        // ─────────────────────────────────────────────
        //  Boucle principale
        // ─────────────────────────────────────────────

        IEnumerator RunWaves()
        {
            while (true)
            {
                CurrentWave++;

                if (CurrentWave > BestWave)
                {
                    BestWave = CurrentWave;
                    PlayerPrefs.SetInt(BEST_WAVE_KEY, BestWave);
                    PlayerPrefs.Save();
                }

                WaveProfile profile = profiles[(CurrentWave - 1) % profiles.Length];
                Debug.Log($"Vague {CurrentWave} — {profile.label}");

                yield return StartCoroutine(SpawnWave(profile));

                yield return new WaitUntil(() => AllClientsGone());

                yield return new WaitForSeconds(waitBetweenWaves);
            }
        }

        // ─────────────────────────────────────────────
        //  Spawn d'une vague
        // ─────────────────────────────────────────────

        IEnumerator SpawnWave(WaveProfile profile)
        {
            int groupCount = Random.Range(profile.minGroups, profile.maxGroups + 1);

            for (int g = 0; g < groupCount; g++)
            {
                // Tire un type UNE FOIS pour tout le groupe → groupe homogène
                ClientTypeSO groupType = PickRandomClientType();

                if (groupType == null)
                {
                    Debug.LogWarning("WaveSpawner : aucun type de client disponible.");
                    yield break;
                }

                int clientsInGroup = Random.Range(
                    profile.minClientsPerGroup,
                    profile.maxClientsPerGroup + 1
                );


                for (int c = 0; c < clientsInGroup; c++)
                {
                    SpawnClient(profile, groupType);
                    yield return new WaitForSeconds(profile.delayBetweenClients);
                }

                yield return new WaitForSeconds(profile.delayBetweenGroups);
            }
        }

        // ─────────────────────────────────────────────
        //  Spawn d'un client individuel
        // ─────────────────────────────────────────────

        void SpawnClient(WaveProfile profile, ClientTypeSO clientType)
        {
            // Choisit une variante aléatoire parmi celles du type
            GameObject prefab = clientType.PickRandomVariant();
        

            GameObject client = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

            PnjMove cb = client.GetComponent<PnjMove>();
            if (cb != null)
            {
                cb.whaitingTime = profile.patience;
                cb.eatTime = profile.eatSpeed;
            }
            else
            {
                Debug.LogWarning($"WaveSpawner : le prefab {prefab.name} n'a pas de PnjTest.");
            }

            activeClients.Add(client);
        }

        // ─────────────────────────────────────────────
        //  Tirage aléatoire d'un type (par poids)
        // ─────────────────────────────────────────────

        ClientTypeSO PickRandomClientType()
        {
            if (clientTypes == null || clientTypes.Length == 0) return null;

            float total = 0f;
            foreach (var ct in clientTypes)
                if (ct != null) total += ct.weight;

            if (total <= 0f) return null;

            float roll = Random.Range(0f, total);
            float cumul = 0f;

            foreach (var ct in clientTypes)
            {
                if (ct == null) continue;
                cumul += ct.weight;
                if (roll <= cumul) return ct;
            }

            return clientTypes[clientTypes.Length - 1];
        }

        // ─────────────────────────────────────────────
        //  Utilitaires
        // ─────────────────────────────────────────────

        bool AllClientsGone()
        {
            activeClients.RemoveAll(c => c == null);
            return activeClients.Count == 0;
        }

        [ContextMenu("Reset Best Wave")]
        public void ResetBestWave()
        {
            PlayerPrefs.DeleteKey(BEST_WAVE_KEY);
            BestWave = 0;
            Debug.Log("Best Wave réinitialisé.");
        }
    }
}