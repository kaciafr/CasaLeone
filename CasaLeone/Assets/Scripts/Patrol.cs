using System;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    private int _currentwaypointIndex = 0;
    private readonly float _speed = 2f;
    private readonly float _waitTime = 0f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    public static PlayerMovement Instance { get; private set; }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.multiplier = 0.3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.multiplier = 1f;
        }
    }


    void Update()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            _waiting = false;
        }

        Transform wp = waypoints[_currentwaypointIndex];

        if (Vector2.Distance(transform.position, wp.position) < 0.01f)
        {
            transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;
            _currentwaypointIndex = (_currentwaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                wp.position,
                Time.deltaTime * _speed
            );

            Vector2 direction = (wp.position - transform.position).normalized;
            if (direction.x < 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                transform.localScale = new Vector3(1f, 1f, 1f);  // droite
        }
    }
}