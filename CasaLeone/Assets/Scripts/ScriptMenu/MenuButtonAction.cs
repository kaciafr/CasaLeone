using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScriptMenu
{
    public class MenuButtonAction : MonoBehaviour
    {
        public GameObject optionContainerGO;
        public GameObject restPanelGO;
        public Light sunLight;

        private float slideSpeed = 0.3f;
        private float optionSpeed = 3f;
        private float sunSpeed = 0.2f;
        private bool isOptionOpen = false;

        private Vector3 closedPos;
        private Vector3 openPos;

        private Quaternion lightDefaultRot;
        private Quaternion lightTargetRot = Quaternion.Euler(20f, 0f, 0f);

        void Start()
        {
            closedPos = optionContainerGO.transform.localPosition;
            openPos = closedPos + new Vector3(-300, 0, 0);
            lightDefaultRot = sunLight.transform.rotation;
        }

        void Update()
        {
            Vector3 targetPos = isOptionOpen ? openPos : closedPos;
            optionContainerGO.transform.localPosition = Vector3.Lerp(
                optionContainerGO.transform.localPosition,
                targetPos,
                Time.deltaTime * optionSpeed
            );

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                OpenOptions();
        }

        public void Begin()
        {
            if (restPanelGO != null)
                StartCoroutine(SlidePanelDown());

            StartCoroutine(RotateSun());
        }

        private IEnumerator SlidePanelDown()
        {
            Vector3 startPos = restPanelGO.transform.localPosition;
            Vector3 endPos = startPos + new Vector3(0, -350, 0);
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * slideSpeed;
                restPanelGO.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }

            restPanelGO.transform.localPosition = endPos;
        }

        private IEnumerator RotateSun()
        {
            Quaternion startRot = sunLight.transform.rotation;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * sunSpeed;
                sunLight.transform.rotation = Quaternion.Lerp(startRot, lightTargetRot, t);
                yield return null;
            }

            sunLight.transform.rotation = lightTargetRot;
        }

        public void OpenOptions()
        {
            isOptionOpen = !isOptionOpen;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}