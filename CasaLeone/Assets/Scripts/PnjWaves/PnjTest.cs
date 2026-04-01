using System.Collections;
using UnityEngine;

public class PnjTest : MonoBehaviour
{
    public float patience;
    public float eatSpeed;

    private enum State { WalkIn, WaitingToOrder, Eating, Leaving }
    private State state = State.WalkIn;

    private float timer = 0f;

    void Start()
    {
        StartCoroutine(LifeCycle());
    }

    IEnumerator LifeCycle()
    {
        // 1. Arrive
        state = State.WalkIn;
        Debug.Log($"{gameObject.name} arrive.");
        yield return new WaitForSeconds(1f);

        // 2. Attend de commander (limité par la patience)
        state = State.WaitingToOrder;
        Debug.Log($"{gameObject.name} attend de commander. Patience : {patience}s");
        float waited = 0f;
        while (waited < patience)
        {
            waited += Time.deltaTime;

            // Simule un serveur qui prend la commande après 3s
            if (waited >= 3f)
            {
                Debug.Log($"{gameObject.name} a commandé.");
                break;
            }

            yield return null;
        }

        if (waited >= patience)
        {
            Debug.Log($"{gameObject.name} a perdu patience et s'en va !");
            Leave();
            yield break;
        }

        // 3. Mange (durée inversement proportionnelle à eatSpeed)
        state = State.Eating;
        float eatDuration = 10f / eatSpeed;
        Debug.Log($"{gameObject.name} mange pendant {eatDuration:F1}s.");
        yield return new WaitForSeconds(eatDuration);

        // 4. Part
        Leave();
    }

    public void Leave()
    {
        state = State.Leaving;
        Debug.Log($"{gameObject.name} quitte le restaurant.");
        Destroy(gameObject);
    }

    void OnGUI()
    {
        // Affiche l'état au-dessus du PNJ en world space (debug visuel)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
        if (screenPos.z > 0)
        {
            GUI.Label(
                new Rect(screenPos.x - 60, Screen.height - screenPos.y - 10, 120, 24),
                $"[{state}]"
            );
        }
    }
}
