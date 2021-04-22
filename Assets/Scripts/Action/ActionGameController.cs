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

    private void Awake()
    {
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
                var ob = FindObjectsOfType<MonoBehaviour>().OfType<IKillable>();
                foreach (IKillable d in ob)
                {
                    d.Die();
                }
            }
        }
    }

    public ShopItem GetRandomItem(List<ShopItem> iList)
    {
        int index = Random.Range(0, iList.Count);
        ShopItem it = iList[index];
        return it;
    }
}
