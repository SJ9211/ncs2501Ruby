using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController player= other.GetComponent<RubyController>();
        if ( player != null)

        // out var . contrioller 형태 신경X 
       // if ( other.TryGetComponent<RubyController>(out var controller))
        {
            player.ChangeHealth(-1);
        }
    }
}
