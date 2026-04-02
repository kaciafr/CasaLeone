using UnityEngine;

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

    [Header("Comportement client, cool si on choppe et multiplie les setting des pnj")]
    public float patience;
    public float eatSpeed;
}