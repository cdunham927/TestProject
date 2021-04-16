using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public override void Use()
    {
        base.Use();
        //Debug.Log("Using weapon");
        for (int i = 0; i < 5; i++)
        {
            Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, -90 +  Random.Range(-accuracy, accuracy)) * Quaternion.Euler(0, 0, -10f + (i * 5f)));
        }
    }
}
