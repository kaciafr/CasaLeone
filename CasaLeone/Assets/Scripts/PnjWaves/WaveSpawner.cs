using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Profils (0 = pressés, 1 = détendus)")]
    public WaveProfile[] profiles = new WaveProfile[2];

    [Header("Clients disponibles")]
    public ClientEntry[] clientEntries;

    [Header("Spawner")]
    public Transform spawnPoint;
    public float waitBetweenWaves = 5f;

    public int CurrentWave { get; private set; } = 0;
    public int BestWave { get; private set; } = 0;

    private const string BEST_WAVE_KEY = "BestWave";
    private List<GameObject> activeClients = new List<GameObject>();

    void Start()
    {
        BestWave = PlayerPrefs.GetInt(BEST_WAVE_KEY, 0);
        StartCoroutine(RunWaves());
    }

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

            WaveProfile profile = profiles[(CurrentWave - 1) % 2];
            Debug.Log($"Vague {CurrentWave} — {profile.label}");

            yield return StartCoroutine(SpawnWave(profile));

            yield return new WaitUntil(() => AllClientsGone());

            yield return new WaitForSeconds(waitBetweenWaves);
        }
    }

    IEnumerator SpawnWave(WaveProfile profile)
    {
        int groupCount = Random.Range(profile.minGroups, profile.maxGroups + 1);

        for (int g = 0; g < groupCount; g++)
        {
            int clientsInGroup = Random.Range(
                profile.minClientsPerGroup,
                profile.maxClientsPerGroup + 1
            );

            for (int c = 0; c < clientsInGroup; c++)
            {
                SpawnClient(profile);
                yield return new WaitForSeconds(profile.delayBetweenClients);
            }

            yield return new WaitForSeconds(profile.delayBetweenGroups);
        }
    }

    void SpawnClient(WaveProfile profile)
    {
        GameObject prefab = PickRandomClient();
        if (prefab == null)
        {
            Debug.LogWarning("WaveSpawner : aucun prefab valide dans clientEntries.");
            return;
        }

        GameObject client = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        PnjTest cb = client.GetComponent<PnjTest>();
        if (cb != null)
        {
            cb.patience = profile.patience;
            cb.eatSpeed = profile.eatSpeed;
        }
        else
        {
            Debug.LogWarning($"WaveSpawner : le prefab {prefab.name} n'a pas de ClientBehavior.");
        }

        activeClients.Add(client);
    }

    GameObject PickRandomClient()
    {
        if (clientEntries == null || clientEntries.Length == 0) return null;

        float total = 0f;
        foreach (var e in clientEntries)
            if (e.prefab != null) total += e.weight;

        if (total <= 0f) return null;

        float roll = Random.Range(0f, total);
        float cumul = 0f;

        foreach (var e in clientEntries)
        {
            if (e.prefab == null) continue;
            cumul += e.weight;
            if (roll <= cumul) return e.prefab;
        }

        return clientEntries[clientEntries.Length - 1].prefab;
    }

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
    }
}