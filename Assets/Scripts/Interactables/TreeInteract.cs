using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteract : Interactable {

    public GameObject LogToSpawn;

    public GameObject BarkToSpawn;

     

    float timePassed = 0.0f;
    private bool isActive = false;
    private GameObject[] logsArr;
    private GameObject[] barkArr;
    [SerializeField]
    private int axeTierReq = 0;

    public int AxeTierReq
    {
        get
        {
            return axeTierReq;
        }
    }

    override public void OnInteract()
    {
        //Only make the tree chopable if the player has an axe that has a high enough tier
        if (InventoryScript.MyInstance.InvAxeTier >= AxeTierReq)
        {
            //Set the interact on active
            isActive = true;

            //Make the tree disappear
            this.GetComponent<MeshRenderer>().enabled = false;
            BoxCollider[] boxArr = GetComponents<BoxCollider>();
            for (int j = 0; j < boxArr.Length; j++)
            {
                boxArr[j].enabled = false;
            }

            //------
            //BARK
            //------
            //How much bark to spawn

            int amount = GetAmount(4, 30);
            barkArr = new GameObject[amount];
            for (int i = 0; i < amount; i++)
            {

                //Spawn a bark, slightly above the ground
                Vector3 spawnLoc = new Vector3(transform.position.x, transform.position.y + (i * 0.1f), transform.position.z);
                barkArr[i] = Instantiate(BarkToSpawn, spawnLoc, Quaternion.Euler(0f, Random.Range(0f, 180f), 0f));
                //For all bark spawned, disable their collision with items
                barkArr[i].GetComponentInChildren<Collider>().enabled = false;
                //Shoot the bark away from the tree
                Vector3 shootForce = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(10.0f, 15.0f), Random.Range(-4.0f, 4.0f));
                shootForce.Normalize();
                barkArr[i].GetComponentInChildren<Rigidbody>().AddForce(shootForce * 250f);
            }


            //------
            //LOGS
            //------

            //How many logs to spawn
            //
            //Add code to check for which axe is being used, and give logs according to modifiers. Currently always one log is spawned.
            //
            amount = 1;
            logsArr = new GameObject[amount];
            for (int i = 0; i < amount; i++)
            {
                //Spawn a log, slightly above the ground
                Vector3 spawnLoc = new Vector3(transform.position.x, transform.position.y + (i * 0.1f), transform.position.z);
                logsArr[i] = Instantiate(LogToSpawn, spawnLoc, Quaternion.Euler(0f, Random.Range(0f, 180f), 0f));
                //For all logs spawned, disable their collision with items
                logsArr[i].GetComponentInChildren<Collider>().enabled = false;
                //Shoot the log away from the tree
                Vector3 shootForce = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(10.0f, 15.0f), Random.Range(-4.0f, 4.0f));
                shootForce.Normalize();
                logsArr[i].GetComponentInChildren<Rigidbody>().AddForce(shootForce * 250f);
            }
        }
        
    }

    private void Update()
    {
        if (timePassed >= 0.5f)
        {
            //For all bark spawned, enable collision with items
            for (int i = 0; i < barkArr.Length; i++)
            {
                barkArr[i].GetComponentInChildren<Collider>().enabled = true;
            }

            //For all logs spawned, enable collision with items
            for (int i = 0; i < logsArr.Length; i++)
            {
                logsArr[i].GetComponentInChildren<Collider>().enabled = true;
            }
            //Delete this GameObject
            Destroy(this.gameObject);
        }
        else if (isActive == true)
        {
            timePassed += Time.deltaTime;
        }
    }

    private int GetAmount(int amountOfRolls, int chance)
    {
        int amount = 0;

        for (int i = 0; i < amountOfRolls; i++)
        {
            //If value is higher than chance, add one to amount
            if (Random.Range(0,101) <= chance) 
            {
                amount++;
            }
        }


        return amount;
    }
}
