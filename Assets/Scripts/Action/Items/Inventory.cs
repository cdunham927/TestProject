using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    int iSlot;
    //Item curItem;
    SpriteRenderer rend;
    float itemCools;
    public Item firstItem;
    public Item secondItem;
    public Item thirdItem;

    public void AddItem(Item i)
    {
        //Item newItem = gameObject.AddComponent<Item>();
        //newItem.Initialize(i);
        if (!items.Contains(i))
        {
            Item newItem = Instantiate(i);
            newItem.transform.SetParent(transform);
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localRotation = Quaternion.identity;
            items.Add(newItem);
            newItem.gameObject.SetActive(false);
        }
    }
    public void RemoveItem(Item i)
    {
        if (items.Contains(i))
        {
            iSlot = 0;
            items.Remove(i);
            Destroy(i.gameObject);
            //curItem = items[iSlot];
            items[iSlot].gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        AddItem(firstItem);
        EquipItem(iSlot);
    }

    public void EquipItem(int slot)
    {
        items[iSlot].gameObject.SetActive(false);
        iSlot = slot;
        //rend.sprite = items[iSlot].iSprite;
        //curItem = items[iSlot];
        items[iSlot].gameObject.SetActive(true);
    }

    public void EquipNextItem()
    {
        iSlot = (iSlot + 1) % items.Count;
    }

    public void SetCools(float c)
    {
        itemCools = c;
    }

    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * 10f);

        if (Input.GetMouseButton(0) && itemCools <= 0)
        {
            //curItem.Use();
        }

        if (Input.GetMouseButtonDown(1))
        {
            EquipItem((iSlot + 1) % items.Count);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveItem(items[iSlot]);
            EquipItem((iSlot + 1) % items.Count);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(secondItem);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem(thirdItem);
        }
    }
}
