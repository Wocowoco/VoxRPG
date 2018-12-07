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
        MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
        MyBagScript.AddSlots(amountOfSlots);
    }

}
