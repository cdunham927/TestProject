using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionMeleeHitBox : MonoBehaviour
{
    public string target;
    public float atk;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(target))
        {
            var ob = collision.GetComponent<IDamageable<float>>();
            ob.Damage(atk);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(target))
        {
            var ob = collision.GetComponent<IDamageable<float>>();
            ob.Damage(atk);
        }
    }
}
