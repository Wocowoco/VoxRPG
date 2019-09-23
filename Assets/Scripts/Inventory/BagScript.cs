using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour {


    [SerializeField]
    private GameObject SlotPrefab;
    private List<SlotScript> slotList = new List<SlotScript>();

    public List<SlotScript> MySlots
    {
        get
        {
            return slotList;
        }
    }

    public void AddSlots(int slotCount)
    {
        //For each slot created, add it to the slotslist
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(SlotPrefab, transform).GetComponent<SlotScript>();
            slotList.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        //Check all the slots of the bag
        foreach (SlotScript slot in slotList)
        {
            //Add the item to the first empty slot it could find
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                return true;
            }
        }

        //Couldn't place the item in one of the slots, so this bag is full
        return false;

    }

    public bool CheckIfBagIsEmpty()
    {
        //Check all the slots of the bag
        foreach (SlotScript slot in slotList)
        {
            //Check if the slot is empty
            if (!slot.IsEmpty)
            {
                //If slot is not empty, the bag is not empty
                return false;
            }
        }

        //All slots were empty, so the bag was empty
        return true;
    }

    public void RemoveBag()
    {
        Destroy(this.gameObject);
    }
}
