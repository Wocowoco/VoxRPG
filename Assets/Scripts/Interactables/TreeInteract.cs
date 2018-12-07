using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteract : Interactable {

    public GameObject LogToSpawn;
    public int MinAmount = 1;
    public int MaxAmount = 3;

    float timePassed = 0.0f;
    private bool isActive = false;
    private GameObject[] logsArr;



    override public void OnInteract()
    {
        //Set the interact on active
        isActive = true;

        //How many logs to spawn
        int amount = Random.Range(MinAmount,MaxAmount);
        logsArr = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            //Make the tree disappear
            this.GetComponent<MeshRenderer>().enabled = false;
            BoxCollider[] boxArr = GetComponents<BoxCollider>();
            for (int j = 0; j < boxArr.Length; j++)
            {
                boxArr[j].enabled = false;
            }

            //Spawn a log, slightly above the ground
            Vector3 spawnLoc = new Vector3(transform.position.x, transform.position.y + (i * 0.1f), transform.position.z);
            logsArr[i] = Instantiate(LogToSpawn, spawnLoc, Quaternion.Euler(0f, Random.Range(0f, 180f), 0f));
            //For all logs spawned, disable their collision with items
            logsArr[i].GetComponentInChildren<SphereCollider>().enabled = false;
            //Shoot the log away from the tree
            Vector3 shootForce = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(10.0f, 15.0f), Random.Range(-4.0f, 4.0f));
            shootForce.Normalize();
            logsArr[i].GetComponent<Rigidbody>().AddForce(shootForce * 250f);
            


        }

        
    }

    private void Update()
    {
        if (timePassed >= 0.5f)
        {
            for (int i = 0; i < logsArr.Length; i++)
            {
                //For all logs spawned, enable collision with items
                //logsArr[i].GetComponentInChildren<SphereCollider>().enabled = true;
            }
            //Delete this GameObject
            Destroy(this.gameObject);
        }
        else if (isActive == true)
        {
            timePassed += Time.deltaTime;
        }


    }
}
