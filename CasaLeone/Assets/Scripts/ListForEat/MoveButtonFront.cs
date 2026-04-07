using System;
using System.Collections.Generic;
using DG.Tweening;
using Item;
using UnityEngine;

namespace ListForEat
{
    public class MoveButtonFront : MonoBehaviour
    {
        [System.Serializable]
        public class ButtonAndPanel
        {
            public RectTransform button;
            public GameObject panel;
        }
        private bool isClicked = true;
        [SerializeField]private List<ButtonAndPanel> buttons;
        [SerializeField] private float moveOffset;
        [SerializeField] private float duration;
        
        private Dictionary<RectTransform, Vector2> initialPositions = new Dictionary<RectTransform, Vector2>();
        public Action<MoveButtonFront> OnClick;

        [SerializeField] private InfoList checkInfo;

        void Start()
        {
            foreach (var btn in buttons)
            {
                initialPositions[btn.button] = btn.button.anchoredPosition;
            }
        }

        public void SelectButton(RectTransform clicked)
        {
            foreach (var item in buttons)
            {
                if (item.button == clicked)
                {
                    item.button.DOAnchorPosY(initialPositions[item.button].y + moveOffset, duration);
                    OnClick?.Invoke(this);
                    item.button.SetAsLastSibling();
                    item.panel.SetActive(true);
                    checkInfo.ClosesInfo();
                }
                else
                {
                    item.button.DOAnchorPos(initialPositions[item.button], duration);
                    item.panel.SetActive(false);
                }
            }
        }
    }
}


