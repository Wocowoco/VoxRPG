using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    //Make it so there's only one inventoryScript
    private static InventoryScript instance;

    [SerializeField]
    private BagSlot bagSlot;
    [SerializeField]
    private BagSlot fixedBag;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    public void AddItem(Item item)
    {
        //Check all bags for a slot to add the item in.
        //foreach (Bag bag in bags)
        //{
        //    //Check if an item can be added in this bag
        //    if (bag.MyBagScript.AddItem(item) == true)
        //    {
        //        //If true, the item was succesfully placed in a slot in the bag.
        //        return;
        //    }
        //}
    }


    [SerializeField]
    private Item[] items;

    private void Awake()
    {
        //Set a fake bag for the first 10 inventory slots
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(10);
        bag.FixedUse();
        fixedBag.MyBag = bag;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(4);
            bag.Use();
        }
    }




    public void AddBag(Bag bag)
    {
        //If there's no bag equipped yet, equip one
        if (bagSlot.MyBag == null)
        {
            bagSlot.MyBag = bag;

        }
    }

}
