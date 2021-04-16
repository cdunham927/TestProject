using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string iName;
    public string description;
    public enum rarity { legendary = 1, rare = 10, uncommon = 50, common = 100 };
    public rarity iRarity;
    public int cost;
    public Sprite iSprite;
    public float cooldown;
    protected Inventory inventory;
    public bool multipleBuys;
    protected ActionPlayerController player;
    SpriteRenderer rend;
    protected float cools;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = iSprite;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cools <= 0)
        {
            Use();
        }

        if (cools > 0) cools -= Time.deltaTime;
    }

    //public virtual void Initialize(Item i) { }

    public virtual void Use() 
    {
        //Debug.Log("Using item: " + iName);
        cools = cooldown;
    }

    public virtual void Remove() { inventory.RemoveItem(this); }
}
