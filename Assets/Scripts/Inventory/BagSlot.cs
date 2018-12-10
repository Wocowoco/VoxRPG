using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagSlot : SlotScript {

    private Bag bag;

    public Bag MyBag
    {
        get
        {
            return (Bag)MyItem;
        }

    }

    override public void RemoveItem(Item item)
    {
        //Only remove items if there is an item
        if (items.Count > 0)
        {
            items.Pop();
        }

        //If this was the last item, remove the item's icon from the slot
        if (items.Count == 0)
        {
            icon.color = new Color(0, 0, 0, 0);

        }
    }

    override public void UseItem()
    {
        //If the slot is not empty
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.AddItem(MyItem);
            MyBag.MyBagScript.RemoveBag();
            RemoveItem(MyBag);
        }
    }
}
