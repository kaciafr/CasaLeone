using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    using UnityEngine;

    public class StairsActivate : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("ENTER : " + collision.gameObject.name);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("STAY : " + collision.gameObject.name);
        }
    }
}