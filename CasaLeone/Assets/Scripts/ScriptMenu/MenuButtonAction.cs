using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScriptMenu
{
    public class MenuButtonAction : MonoBehaviour
    {
        public GameObject optionContainerGO;
        public GameObject restPanelGO;

        private float slideSpeed = 0.3f;
        private float optionSpeed = 3f;
        private float sunSpeed = 0.2f;
        private bool isOptionOpen = false;

        private Vector3 closedPos;
        private Vector3 openPos;
        
        void Start()
        {
            closedPos = optionContainerGO.transform.localPosition;
            openPos = closedPos + new Vector3(-530, 0, 0);
        }

        void Update()
        {
            Vector3 targetPos = isOptionOpen ? openPos : closedPos;
            optionContainerGO.transform.localPosition = Vector3.Lerp(
                optionContainerGO.transform.localPosition,
                targetPos,
                Time.deltaTime * optionSpeed
            );
            
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