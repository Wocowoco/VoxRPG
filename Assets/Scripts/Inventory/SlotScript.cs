using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    private bool isEmpty;

    private Stack<Item> items = new Stack<Item>();

    [SerializeField]
    public Image icon;

    public bool IsEmpty
    {
        get
        {
            //If this slot is empty, return true. Else return false;
            return items.Count == 0;
        }
    }

    public bool AddItem(Item item)
    {
        //Add item
        items.Push(item);

        //Set the image of the slot to the image of the item and make it visible
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        //Return true if the item was succesfully added
        return true;
    }
}
