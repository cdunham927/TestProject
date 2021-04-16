using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public GameObject projectile;
    public float atk;
    public float accuracy;

    /*public override void Initialize(Item i) 
    {
        Debug.Log("Initializing");
        iName = i.iName;
        description = i.description;
        cost = i.cost;
        iSprite = i.iSprite;
        cooldown = i.cooldown;
        Weapon w = i.GetComponent<Weapon>();
        projectile = w.projectile;
        atk = w.atk;
    }*/

    public override void Remove()
    {
        base.Remove();
    }
}
