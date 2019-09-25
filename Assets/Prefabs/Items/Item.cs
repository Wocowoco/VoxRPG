using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 1)]
public class Item : ScriptableObject {

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private bool isUseable = false;

    private SlotScript slot;

    [SerializeField]
    private GameObject itemObject;


    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }


    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public bool IsUseable
    {
        get
        {
            return isUseable;
        }
    }

    public SlotScript MySlot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public GameObject MyItemObject
    {
        get
        {
            return itemObject;
        }
    }

    virtual public void Use()
    {

    }

    public void Remove()
    {
        //If the item is in a slot, remove it from that slot
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }

    }
}
