using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Use()
    {
        base.Use();
        //Debug.Log("Using weapon");
        Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, -90 + Random.Range(-accuracy, accuracy)));
    }
}
