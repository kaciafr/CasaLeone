using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TicketAnimation : MonoBehaviour
{
    public RectTransform ticket;
    public CanvasGroup ticketCanvasGroup; 

    public Vector2 posVisible = new Vector2(-600f, 400f);
    public Vector2 posCache   = new Vector2(-600f, 800f);

    void Start()
    {
        ResetTicket();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TicketImpression();
    }

    private void ResetTicket()
    {
        ticket.anchoredPosition = posCache;
        ticket.localScale       = new Vector3(1f, 0f, 1f);
        ticketCanvasGroup.alpha = 0f;
    }

    private void TicketImpression()
    {
        ticket.DOKill();
        ResetTicket();

        Sequence seq = DOTween.Sequence();

        seq.Append(
            ticketCanvasGroup.DOFade(1f, 0.15f)
        );

        seq.Append(
            ticket.DOAnchorPos(posVisible, 0.5f)
                .SetEase(Ease.OutCubic)
        );

        seq.Join(
            ticket.DOScaleY(1f, 1.5f)
                .SetEase(Ease.OutExpo)
        );

        seq.Join(
            ticket.DORotate(new Vector3(0f, 0f, -2f), 0.3f)
                .SetEase(Ease.OutSine)
                .SetDelay(0.3f)
        );
        seq.Append(
            ticket.DORotate(Vector3.zero, 0.2f)
                .SetEase(Ease.OutSine)
        );

        seq.Append(
            ticket.DOPunchScale(new Vector3(0.03f, 0.05f, 0f), 0.4f, 6, 0.4f)
        );

        seq.Append(
            ticket.DOShakePosition(0.35f, new Vector3(3f, 1f, 0f), 30, 90f)
        );

        seq.Append(
            ticket.DOAnchorPosY(posVisible.y - 8f, 0.2f)
                .SetEase(Ease.OutBounce)
        );

        seq.Play();
    }
}
