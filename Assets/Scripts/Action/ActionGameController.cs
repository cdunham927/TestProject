using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionGameController : MonoBehaviour
{
    public List<ShopItem> shopItems = new List<ShopItem>();
    [HideInInspector]
    public List<ShopItem> commonItems = new List<ShopItem>();
    [HideInInspector]
    public List<ShopItem> uncommonItems = new List<ShopItem>();
    [HideInInspector]
    public List<ShopItem> rareItems = new List<ShopItem>();
    [HideInInspector]
    public List<ShopItem> legendaryItems = new List<ShopItem>();
    [HideInInspector]
    public List<Item> consumableItems = new List<Item>();

    public float dmg = 5;
    public GameObject[] spawnPoints;
    public GameObject enemy;
    public List<GameObject> enemies;
    public float timeBetweenSpawns = 8f;
    float cooldown;

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        for (int i = 0; i < shopItems.Count; i++)
        {

            if (shopItems[i].it.GetComponent<Consumable>() != null)
            {
                //Debug.Log("Consumable");
                consumableItems.Add(shopItems[i].it);
            }

            switch (shopItems[i].it.iRarity)
            {
                case Item.rarity.common:
                    commonItems.Add(shopItems[i]);
                    break;
                case Item.rarity.uncommon:
                    uncommonItems.Add(shopItems[i]);
                    break;
                case Item.rarity.rare:
                    rareItems.Add(shopItems[i]);
                    break;
                case Item.rarity.legendary:
                    legendaryItems.Add(shopItems[i]);
                    break;
            }
        }
    }

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;

        if (cooldown <= 0) SpawnEnemy();

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                var ob = FindObjectsOfType<MonoBehaviour>().OfType<IDamageable<float>>();
                foreach (IDamageable<float> d in ob)
                {
                    d.Damage(dmg);
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                ActionEnemyController[] en = FindObjectsOfType<ActionEnemyController>();
                foreach(ActionEnemyController e in en)
                {
                    e.Die();
                }
                //var ob = FindObjectsOfType<MonoBehaviour>().OfType<IKillable>();
                //foreach (IKillable d in ob)
                //{
                    //d.Die();
                //}
            }
        }
    }

    public void SpawnEnemy()
    {
        GameObject o = GetEnemy();
        o.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        o.SetActive(true);

        cooldown = timeBetweenSpawns;
    }
    
    public GameObject GetEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                return enemies[i];
            }
        }

        GameObject en = Instantiate(enemy, transform.position, Quaternion.identity);
        enemies.Add(en);
        en.SetActive(false);

        return en;
    }

    public ShopItem GetRandomItem(List<ShopItem> iList)
    {
        int index = Random.Range(0, iList.Count);
        ShopItem it = iList[index];
        return it;
    }
}
