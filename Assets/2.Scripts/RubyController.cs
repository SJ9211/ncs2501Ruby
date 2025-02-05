using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    
    void Start()
    {
        //QualitySettings.vSyncCount = 0;      
        //Application.targetFrameRate = 10;    0.1*10 으로 이동하여 초당 1유닛만큼 이동        
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
        Vector2 position = transform.position;
        position.x += moveSpeed * horizontal * Time.deltaTime;
        position.y += moveSpeed * vertical * Time.deltaTime;
        transform.position = position;
    }
}
