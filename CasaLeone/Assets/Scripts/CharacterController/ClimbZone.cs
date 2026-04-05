using DG.Tweening;
using UnityEngine;

public class ClimbZone : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private GameObject climbFeedBack;

    [Header("Settings")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float duration = 0.2f;

    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("Inputs")]
    [SerializeField] private InputManager inputPlayer1;
    [SerializeField] private InputManager inputPlayer2;

    private readonly Vector3 _targetScale = new Vector3(0.5f, 0.5f, 0.5f);
    private bool _canClimb = false;
    private bool _isClimbing = false;

    private void Update()
    {
        if (_canClimb && !_isClimbing)
        {
            if (inputPlayer1 != null && inputPlayer1.IsInteracting)
                SlideStairs(player1);

            if (inputPlayer2 != null && inputPlayer2.IsInteracting)
                SlideStairs(player2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsPlayer(other)) return;

        _canClimb = true;

        if (climbFeedBack == null) return;

        climbFeedBack.SetActive(true);
        climbFeedBack.transform.DOKill(true);
        climbFeedBack.transform.localScale = Vector3.zero;
        climbFeedBack.transform.DOScale(_targetScale, 0.5f).SetEase(Ease.OutBack);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsPlayer(other)) return;

        _canClimb = false;

        if (climbFeedBack == null) return;

        climbFeedBack.transform.DOKill(true);

        Sequence exitSeq = DOTween.Sequence();
        exitSeq.Append(climbFeedBack.transform
            .DOScale(_targetScale * 1.2f, 0.2f)
            .SetEase(Ease.OutBack));
        exitSeq.Append(climbFeedBack.transform
            .DOScale(Vector3.zero, 0.3f)
            .SetEase(Ease.InBack));
        exitSeq.OnComplete(() => climbFeedBack.SetActive(false));
    }

    private void SlideStairs(GameObject player)
    {
        if (points == null || points.Length == 0)
        {
            Debug.LogWarning("[ClimbZone] Aucun point défini !");
            return;
        }

        if (player == null) return;

        _isClimbing = true;

        Vector3[] waypoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
            waypoints[i] = points[i].position;

        Sequence stairsSeq = DOTween.Sequence();

        stairsSeq.Append(player.transform
            .DORotate(new Vector3(0f, 0f, 45f), 0.3f)
            .SetEase(Ease.OutSine));

        stairsSeq.Append(player.transform
            .DOPath(waypoints, duration, PathType.CatmullRom)
            .SetEase(Ease.InOutSine)
            .SetOptions(false));

        stairsSeq.Append(player.transform
            .DORotate(Vector3.zero, 0.3f)
            .SetEase(Ease.InSine));

        stairsSeq.OnComplete(() => _isClimbing = false);
    }

    private bool IsPlayer(Collider2D other)
    {
        return other.CompareTag("Player") ||
               (other.transform.parent != null &&
                other.transform.parent.CompareTag("Player"));
    }
}
