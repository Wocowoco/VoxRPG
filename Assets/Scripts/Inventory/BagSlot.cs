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

    override public void UseItem()
    {
        //If the slot is not empty
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.AddItem(MyItem);
            RemoveItem(MyItem);
        }
    }


    override public void OnPointerClick(PointerEventData eventData)
    {
        //Checking EventData
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UseItem();
        }
    }
}
