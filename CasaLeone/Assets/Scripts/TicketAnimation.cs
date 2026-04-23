using DG.Tweening;
using UnityEngine;

public class TicketAnimation : MonoBehaviour
{
    public RectTransform ticket;
    public CanvasGroup   ticketCanvasGroup;
    public int           printSteps = 10;
    public float         stepMove   = 0.01f;
    public float         stepPause  = 0.02f;

    private float _startY;
    private float _finalY;

    void Awake()
    {
        _finalY = ticket.anchoredPosition.y;
        _startY = _finalY + ticket.rect.height;
    }

    void Start() => ResetTicket();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PrintTicket();
    }

    private void ResetTicket()
    {
        ticket.anchoredPosition = new Vector2(ticket.anchoredPosition.x, _startY);
        ticketCanvasGroup.alpha = 0f;
    }

    private void PrintTicket()
    {
        DOTween.Kill(ticket);
        ResetTicket();

        float stepSize = ticket.rect.height / printSteps;

        Sequence seq = DOTween.Sequence();
        seq.Append(ticketCanvasGroup.DOFade(1f, 0.08f));
        seq.AppendInterval(0.1f);

        for (int i = 1; i <= printSteps; i++)
        {
            float targetY = _startY - stepSize * i;
            seq.Append(ticket.DOAnchorPosY(targetY, stepMove).SetEase(Ease.OutExpo));
            seq.AppendInterval(stepPause);
        }
        
        seq.Append(
            ticket.DOPunchScale(new Vector3(0.03f, 0.05f, 0f), 0.4f, 6, 0.4f)
        );

        seq.Append(
            ticket.DOShakePosition(0.35f, new Vector3(3f, 1f, 0f), 60, 90f)
        );
        
        seq.Play();
    }
}



