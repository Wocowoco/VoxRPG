using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    [SerializeField]
    private BagSlot bagSlot;
    [SerializeField]
    private BagSlot fixedBag;

    //Make it so there's only one inventoryScript
    private static InventoryScript instance;

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

    public BagSlot MyBagSlot
    {
        get
        {
            return bagSlot;
        }
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
        //Spawn bag in bagslot
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(4);
            bag.Use();
        }

        //Debug: Spawn bag in inventory
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(4);
            AddItem(bag);
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

    public void AddItem(Item item)
    {

        //Check if the fixed bag can take an item
        if (fixedBag.MyBag.MyBagScript.AddItem(item) == true)
        {
            //If true, the item was succesfully placed in a slot in the bag.
            return;
        }

        //Check the second equiped bag if there's space for the item, if there is a second bag equipped equiped
        else if (bagSlot.MyBag.MyBagScript != null && bagSlot.MyBag.MyBagScript.AddItem(item) == true)
        {
            //If true, the item was succesfully placed in a slot in the bag.
            return;
        }
    }

}
