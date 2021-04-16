using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Item it;
    ActionPlayerController player;
    Inventory inv;
    SpriteRenderer rend;
    Text text;

    private void Awake()
    {
        player = FindObjectOfType<ActionPlayerController>();
        inv = FindObjectOfType<Inventory>();
        rend = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<Text>();

        rend.sprite = it.iSprite;
        string a = it.iName + "\n" + it.description;
        text.text = a;

    }

    private void Update()
    {
        text.color = (player.money >= it.cost) ? Color.green : Color.red;
    }

    public void BuyItem()
    {
        if (player.money >= it.cost)
        {
            player.AddMoney(-it.cost);
            inv.AddItem(it);
            if (!it.multipleBuys) Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        BuyItem();
    }
}
