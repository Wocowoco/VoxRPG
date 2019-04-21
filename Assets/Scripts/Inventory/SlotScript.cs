using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    protected Stack<Item> items = new Stack<Item>();

    [SerializeField]
    public Image icon;
    public Text StackSize;

    public bool IsEmpty
    {
        get
        {
            //If this slot is empty, return true. Else return false
            if (items.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Item MyItem
    {
        get
        {
            //If the slot is not empty
            if (!IsEmpty)
            {
                return items.Peek();
            }

            return null;
        }
    }

    public bool AddItem(Item item)
    {
        //Add item
        items.Push(item);

        //If the item is stackable, show it's stack count
        if(item.MyStackSize > 1)
        {
            StackSize.text = items.Count.ToString();
        }

        //Set the image of the slot to the image of the item and make it visible
        icon.sprite = item.MyIcon;
        icon.color = Color.white;

        //Let the item know in which slot it is
        item.MySlot = this;
        //Return true if the item was succesfully added
        return true;
    }

    virtual public void RemoveItem(Item item)
    {
        //Only remove items if there is an item
        if (items.Count > 0)
        {
            items.Pop();

            //Lower the stacksize by one if it is stackable
            if (item.MyStackSize > 1)
            {
                StackSize.text = items.Count.ToString();
            }

            //If this was the last item, remove the item's icon from the slot
            if (items.Count == 0)
            {
                icon.color = new Color(0, 0, 0, 0);
                StackSize.text = "";

                //Update tool tiers when removing item
                InventoryScript.MyInstance.UpdateTiers(item, false);
            }

        }
    }

    virtual public void DropItem(Item item)
    {
        if (items.Count > 0)
        {
            //Drop the item in the world at the player's location
            Transform playerTransform = GameManage.MyInstance.playerObject.GetComponent<PlayerMovement>().PlayerModel.transform;
            Vector3 spawnLoc = playerTransform.position + playerTransform.forward * 0.5f;
            GameObject droppedItem = Instantiate(MyItem.MyItemObject, new Vector3(spawnLoc.x, spawnLoc.y + 0.2f, spawnLoc.z), Quaternion.Euler(0f, 0f, 0f));
            Vector3 dropDirection = new Vector3(playerTransform.forward.x + Random.Range(-0.1f, 0.1f), playerTransform.forward.y + Random.Range(-0.1f, 0.1f), playerTransform.forward.z + Random.Range(-0.1f, 0.1f));
            droppedItem.GetComponent<Rigidbody>().AddForce(dropDirection * 350.0f);
            
            //Remove one of the items
            RemoveItem(item);
        }
    }

    virtual public void UseItem()
    {
        //If the slot is not empty
        if (!IsEmpty)
        {
            //If the item is usable, use it
            if (MyItem.IsUseable == true)
            {
                MyItem.Use();
            }
        }
    }

    virtual public void OnPointerClick(PointerEventData eventData)
    {
        //Checking EventData

        //Check if leftclick
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //If double leftclick
            if (eventData.clickCount >= 2)
            {
                UseItem();
            }
        }

        //Check if rightclick
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem(MyItem);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Check if leftclick
        if (eventData.button == PointerEventData.InputButton.Left)
        {

        }

    }


    public bool StackItem(Item item)
    {
        //If not empty, check if it is the same item, and if the stack isn't maxed out yet
        if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            StackSize.text = items.Count.ToString();
            return true;
        }

        //If failed, return false
        return false;
    }


}
