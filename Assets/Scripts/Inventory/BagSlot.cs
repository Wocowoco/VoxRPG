using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagSlot : SlotScript {

    private Bag bag;

    [SerializeField]
    private Sprite full, empty;

public Bag MyBag
    {
        get
        {
            return bag;
        }

        set
        {
            bag = value;

            //If the slot is not empty, show to bag's icon on the slot
            if (value != null)
            {
                icon.sprite = bag.MyIcon;
                icon.color = Color.white;
            }

            //If the bag is empty, don't show any icon
            else
            {
                icon.color = new Vector4(0f, 0f, 0f, 0f);
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
