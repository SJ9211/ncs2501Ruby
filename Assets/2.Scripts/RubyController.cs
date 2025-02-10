using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;

    public float moveSpeed = 6.0f;
    public int health { get { return currentHealth; } }
    public float timeInvincible = 2.0f;
    bool isInvincible;
    private float invincibleTimer;
    private int currentHealth;
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //QualitySettings.vSyncCount = 0;      
        //Application.targetFrameRate = 10;    0.1*10 으로 이동하여 초당 1유닛만큼 이동        

        currentHealth = maxHealth;
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
        Vector2 position = rb2d.position;
        position.x += moveSpeed * horizontal * Time.deltaTime;
        position.y += moveSpeed * vertical * Time.deltaTime;
        rb2d.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if ( invincibleTimer < 0)
                 isInvincible = false;
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
        }
     currentHealth = Mathf.Clamp( currentHealth +amount, 0, maxHealth);
     Debug.Log($"{currentHealth}/{maxHealth}");
    }
     }
/*
충돌 ~ collider  {OnCollisionEnter2D, OnCollisionStay2D, OnCollisionExit2D}
                  OnTriggerEnter2D, OnTriggerStay2D , OnTriggerExit2D
*/