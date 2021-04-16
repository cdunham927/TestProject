using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int uses;
    public float amt;

    public override void Use()
    {
        base.Use();
        //Debug.Log("Using consumable");
        FindObjectOfType<ActionPlayerController>().Heal(amt);
        uses--;
        if (uses <= 0) Remove();
    }

    public override void Remove()
    {
        base.Remove();
    }
}
