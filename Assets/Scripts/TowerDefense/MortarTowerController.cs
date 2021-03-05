using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTowerController : BaseTower
{
    public override void Shoot()
    {
        Instantiate(bullet, bulSpawn[0].transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
        Instantiate(flash, bulSpawn[0].transform.position, transform.rotation);
        cools = shootSpd;
    }
}
