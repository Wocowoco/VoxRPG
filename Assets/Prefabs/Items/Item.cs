using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    private InvSlotScript slot;


    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }


    public int StackSize
    {
        get
        {
            return stackSize;
        }
    }


    protected InvSlotScript Slot
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
}
