using DG.Tweening;
using UnityEngine;

public class StairsZone : MonoBehaviour
{
    [SerializeField] private GameObject climbFeedBack;

    [SerializeField] private Transform[] points;
    [SerializeField] private float duration = 0.2f;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private InputManager inputPlayer1;
    [SerializeField] private InputManager inputPlayer2;

    public float rotationoffset = -45;

    private readonly Vector3 _targetScale = new Vector3(0.5f, 0.5f, 0.5f);

    private bool _player1InZone = false;
    private bool _player2InZone = false;
    private bool _isClimbing = false;

    private void Update()
    {
        if (_isClimbing) return;

        if (_player1InZone && inputPlayer1 != null && inputPlayer1.IsInteracting)
            SlideStairs(player1);

        if (_player2InZone && inputPlayer2 != null && inputPlayer2.IsInteracting)
            SlideStairs(player2);
    }

    private void OnTriggerEnter(Collider other)      
    {
        if (!IsPlayer(other)) return;

        if (IsSpecificPlayer(other, player1))
            _player1InZone = true;

        if (IsSpecificPlayer(other, player2))
            _player2InZone = true;

        if (climbFeedBack == null) return;

        climbFeedBack.SetActive(true);
        climbFeedBack.transform.DOKill(true);
        climbFeedBack.transform.localScale = Vector3.zero;
        climbFeedBack.transform.DOScale(_targetScale, 0.5f).SetEase(Ease.OutBack);
    }

    private void OnTriggerExit(Collider other)       
    {
        if (!IsPlayer(other)) return;

        if (IsSpecificPlayer(other, player1)) _player1InZone = false;
        if (IsSpecificPlayer(other, player2)) _player2InZone = false;

        if (_player1InZone || _player2InZone) return;

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
        if (points == null || points.Length == 0) return;
        if (player == null) return;

        _isClimbing = true;

        Vector3[] waypoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
            waypoints[i] = points[i].position;

        Vector3 direction = waypoints[waypoints.Length - 1] - waypoints[0];

        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            bool goingRight = direction.x > 0f;
            pm.Turn(goingRight);
        }

        Sequence stairsSeq = DOTween.Sequence();

        stairsSeq.Append(player.transform
            .DOPath(waypoints, duration, PathType.Linear)
            .SetEase(Ease.Linear)
            .SetOptions(false));

        stairsSeq.OnComplete(() => _isClimbing = false);
    }

    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") ||
               (other.transform.parent != null &&
                other.transform.parent.CompareTag("Player"));
    }

    private bool IsSpecificPlayer(Collider other, GameObject player)
    {
        if (player == null) return false;

        Transform current = other.transform;
        while (current != null)
        {
            if (current.gameObject == player) return true;
            current = current.parent;
        }
        return false;
    }
}


// TO DO Test avec FindObeject by name  
// 2 Tag different Player 1 / Player2 
// 
