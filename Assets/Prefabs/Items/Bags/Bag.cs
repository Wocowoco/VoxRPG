using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bag", menuName ="Items/Bag", order =1)]
public class Bag : Item {

    private int amountOfSlots;

    [SerializeField]
    private GameObject bagPrefab;

    public BagScript MyBagScript { get; set; }

    public int AmountOfSlots
    {
        get
        {
            return amountOfSlots;
        }
    }


    //Set the number of slots of the bag
    public void Initialize(int amountOfSlots)
    {
        this.amountOfSlots = amountOfSlots;
    }


    public void Use()
    {
        //Check if there the bag slot is still empty before adding it
        if (InventoryScript.MyInstance.MyBagSlot.MyBag == null)
        {
            MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(amountOfSlots);

            //Tell the Inventory which bag we are equipping
            InventoryScript.MyInstance.AddBag(this);
        }
    }

    //Use this only to declare the starting slots of your inventory (fake bag)
    public void FixedUse()
    {
        MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
        MyBagScript.AddSlots(amountOfSlots);
    }

}
