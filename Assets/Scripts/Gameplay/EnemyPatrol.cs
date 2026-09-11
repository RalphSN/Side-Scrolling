using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float patrolDistance = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 startPosition;
    private int direction = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("isWalking", true);

        float distanceFromStart = transform.position.x - startPosition.x;
        if (direction == 1 && distanceFromStart >= patrolDistance)
        {
            direction = -1;
        }
        else if (direction == -1 && distanceFromStart <= -patrolDistance)
        {
            direction = 1;
        }
        transform.localScale = new Vector3(
            direction * Math.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }
}
