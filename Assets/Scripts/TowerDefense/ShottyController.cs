using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottyController : BaseTower
{
    public override void Shoot()
    {
        for (int ii = 0; ii < bulSpawn.Length; ii++)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(bullet, bulSpawn[ii].transform.position, transform.rotation * Quaternion.Euler(0, 0, -10f + (i * 5f)) * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
                Instantiate(flash, bulSpawn[ii].transform.position, transform.rotation);
            }
        }
        cools = shootSpd;
    }
}
