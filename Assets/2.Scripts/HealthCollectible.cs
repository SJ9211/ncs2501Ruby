using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //public ParticleSystem getEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //RubyController controller = other.GetComponent<RubyController>();
        
        if ( other.TryGetComponent<RubyController>(out var controller))
       //if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                //Instantiate(getEffect, transform);
                controller.ChangeHealth(1);
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject);
            }
        }
    }
}
