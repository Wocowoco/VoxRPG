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

    override public void OnPointerClick(PointerEventData eventData)
    {
        //Checking EventData

        //Check if leftclick
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //If double leftclick
            if (eventData.clickCount >= 2)
            {
                UseItem();
            }
        }

        //Check if rightclick
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    override public void UseItem()
    {
        //If the bagslot is not empty
        if (!IsEmpty)
        {
            //If the bag can be added to the inventory, remove it, otherwise keep it equipped.
            if (InventoryScript.MyInstance.RemoveBagFromBagSlot(MyBag, 0))
            {
                MyBag.MyBagScript.RemoveBag();
                RemoveItem(MyBag);
            }
        }
    }
}
