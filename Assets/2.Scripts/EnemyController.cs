using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float moveSpeed = 4.0f;
    public bool vertical;
    public float changTime = 3.0f;

    private Rigidbody2D rb2d;
    float timer;
    int direction = 1;
    private Animator animator;

    private Vector2 position;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changTime;
        position = rb2d.position;
        animator = GetComponent<Animator>();
    }

    // Animator 에서 쓸 내용은 코드랑 대소문자 꼭 똑같이하기 !!
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changTime;
        }

        if (vertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
            position.y += moveSpeed * direction * Time.deltaTime;
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
            position.x += moveSpeed * direction * Time.deltaTime;
        }
        rb2d.MovePosition(position);


        void OnCollisionEnter2D(Collision2D other)
        {
           // RubyController player = other.gameObject.GetComponent<RubyController>();
            
            if ( gameObject.TryGetComponent<RubyController>(out var player))
           // if (player != null)
            {
                player.ChangeHealth(-1);
            }
        }
    }
}
