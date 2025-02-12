using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class RubyController : MonoBehaviour
{
    
    public int maxHealth = 5;
    public float moveSpeed = 4.0f;
    public int health { get { return currentHealth; } }
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;
    bool isInvincible;
    private float invincibleTimer;
    private int currentHealth;
    private Rigidbody2D rb2d;
    private Vector2 position;
    Animator animator;
    private Vector2 lookDirection = new Vector2(1,0);
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //QualitySettings.vSyncCount = 0;      
        //Application.targetFrameRate = 10;    0.1*10 으로 이동하여 초당 1유닛만큼 이동        

        currentHealth = maxHealth;
        position = rb2d.position;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");       
        // GetAxisLaw를 사용하면 -1,1값이 넘어온다
        // float vertical = Input.GetAxisRaw("Vertical");
        // Debug.Log($"H:{horizontal}");
        // Debug.Log($"V:{vertical}");
        //Debug.Log(horizontal);
        //Debug.LogWarning("Warnig");
        //Debug.LogError("Error!!!");
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
       
        //position.x += moveSpeed * horizontal * Time.deltaTime;
        //position.y += moveSpeed * vertical * Time.deltaTime;
        position += move * moveSpeed * Time.deltaTime;
        rb2d.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if ( invincibleTimer < 0)
                 isInvincible = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }
    public void ChangeHealth(int amount)
    {
        
        //Debug.Log($"{currentHealth / maxHealth}");  // string 보관용
        if ( amount < 0)
        {
           
            if (isInvincible)
                return;

                isInvincible = true;
                invincibleTimer = timeInvincible;
                animator.SetTrigger("Hit");
        }
     currentHealth = Mathf.Clamp( currentHealth +amount, 0, maxHealth);
     Debug.Log($"{currentHealth}/{maxHealth}");
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(
            projectilePrefab,
            rb2d.position + Vector2.up * 0.5f,
            quaternion.identity);
        Projectile projectile = projectileObject.
                    GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
 }
/*
충돌 ~ collider  {OnCollisionEnter2D, OnCollisionStay2D, OnCollisionExit2D}
                  OnTriggerEnter2D, OnTriggerStay2D , OnTriggerExit2D
*/