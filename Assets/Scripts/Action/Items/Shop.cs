using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Item.rarity shopRarity;
    public float interactDistance;
    float distance;
    public GameObject shopParent;

    ActionPlayerController player;
    ActionGameController cont;

    //For populating shop
    int index;
    ShopItem it;

    private void Awake()
    {
        player = FindObjectOfType<ActionPlayerController>();
        cont = FindObjectOfType<ActionGameController>();

        Invoke("PopulateShop", 0.2f);
    }

    public void PopulateShop()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (shopRarity)
            {
                case Item.rarity.common:
                    it = cont.GetRandomItem(cont.commonItems);
                    //commonItems.Remove(it);
                    break;
                case Item.rarity.uncommon:
                    it = cont.GetRandomItem(cont.uncommonItems);
                    //uncommonItems.Remove(it);
                    break;
                case Item.rarity.rare:
                    it = cont.GetRandomItem(cont.rareItems);
                    //rareItems.Remove(it);
                    break;
                case Item.rarity.legendary:
                    it = cont.GetRandomItem(cont.legendaryItems);
                    //legendaryItems.Remove(it);
                    break;
            }
            GameObject obj = Instantiate(it.gameObject);
            obj.transform.SetParent(shopParent.transform);
            obj.transform.localPosition = new Vector3(-1f + (1f * i), 0, 0);
            //shopItems.RemoveAt(index);
        }
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= interactDistance)
        {
            shopParent.SetActive(true);
        }
        else
        {
            shopParent.SetActive(false);
        }
    }
}
