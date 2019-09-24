using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {
    [SerializeField]
    private Item item;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //IF the player enters the range of the item, try to add it to the inventory
        if (other.gameObject.tag == "Player")
        {
            if (InventoryScript.MyInstance.AddItem(item))
            {
                //Item succesfully added, delete this
                Destroy(this.gameObject);
            }
        }
    }
}
