using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region  public val
    public float moveSpeed = 4.0f;
    public bool vertical;
    public float changTime = 3.0f;
    public int needFix = 3;
    public ParticleSystem smokeEffect;

    public AudioClip walkClip;


    #endregion

    #region  private val
    private Rigidbody2D rb2d;
    float timer;
    int direction = 1;
    private Animator animator;
    public RubyController Ruby;
    private Vector2 position;
    private bool broken;
    private int fixedCount;
    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changTime;
        position = rb2d.position;
        animator = GetComponent<Animator>();
        broken = true;
        fixedCount = 0;
    }

    // Animator 에서 쓸 내용은 코드랑 대소문자 꼭 똑같이하기 !!
    void Update()
    {
        if (!broken)
        {
            return;
        }

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

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        //if (gameObject.TryGetComponent<RubyController>(out var player))
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        //   broken = false;
        //   rb2d.simulated = false;
        //   animator.SetTrigger("Fixed");
        fixedCount++;
        if (fixedCount >= needFix)
        {
            broken = false;
            rb2d.simulated = false;
            animator.SetTrigger("Fixed");
            smokeEffect.Stop();
            // Ruby 에게 fixd 알리기
            GameObject.FindWithTag("RUBY");
            // jambi 에게 fixed 알리기
            NPC jambi = GameObject.FindWithTag("JAMBI").GetComponent<NPC>();
           if ( jambi.NoticeRobotFixed())
           {
            
           }
        }
    }
}
