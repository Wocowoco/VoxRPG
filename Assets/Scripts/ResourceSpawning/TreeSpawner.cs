using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour {

    public int MaxTrees;
    public int AmountOfTrees;
    public float DistanceBetween = 0.5f;
    [Range(1,100)]
    public int ForceAmount = 100;
    public static int DifferentTrees;
    public GameObject[] TreeToSpawn = new GameObject[DifferentTrees];

    private float sizeX;
    private float sizeZ;
    private Vector3[] savedPosArr;
    private int currentTree = 0;
    private int errorStack = 0;
    private int stack = 0;
    // Use this for initialization
    void Start()
    {
        //Set arr size
        savedPosArr = new Vector3[AmountOfTrees];

        //Get the size of this spawnfield
        sizeX = this.transform.localScale.x / 2.0f;
        sizeZ = this.transform.localScale.z / 2.0f;

        float posX = this.transform.position.x;
        float posZ = this.transform.position.z;

        //Choose a random spawnlocation within the spawnfield;
        float spawnLocX = Random.Range((posX - sizeX), (posX + sizeX));
        float spawnLocZ = Random.Range((posZ - sizeZ), (posZ + sizeZ));
        Vector3 spawnLoc = new Vector3(spawnLocX, transform.position.y, spawnLocZ);
        SpawnTree(spawnLoc);


        //Spawn all remaining trees
        //Check if the spawnlocation is close a already spawned location, if that's the case, choose a new location
        for (int i = 0; i < AmountOfTrees - 1; i++)
        {
            bool isValidLocation = false;

            while (isValidLocation == false)
            {
                stack++;
                spawnLocX = Random.Range((posX - sizeX), (posX + sizeX));
                spawnLocZ = Random.Range((posZ - sizeZ), (posZ + sizeZ));

                for (int j = 0; j < currentTree; j++)
                {
                    float xLow = savedPosArr[j].x - DistanceBetween;
                    float xHigh = savedPosArr[j].x + DistanceBetween;
                    float zLow = savedPosArr[j].z - DistanceBetween;
                    float zHigh = savedPosArr[j].z + DistanceBetween;

                    //Check X-axis
                    if (spawnLocX < xLow || spawnLocX > xHigh)
                    {
                        //Check Z-axis
                        if (spawnLocZ < zLow || spawnLocZ > zHigh)
                        {
                            //Do nothing: It's a valid spot to spawn, check other trees in array

                            //On the last check
                            if (j+1 == currentTree)
                            {
                                //Once all trees have been checked, allow it to spawn.
                                isValidLocation = true;
                                errorStack = 0;
                            }
                        }
                        else
                        {
                            //Break when space is already occupied
                            errorStack++;
                            break;
                        }
                    }//End if loop (check if in range of another)
                    else
                    {
                        //Break when space is already occupied
                        errorStack++;
                        break;
                    }


                }//End for loop (check pos)

                //Exit early if not all trees could be placed
                if (errorStack == ForceAmount * 10)
                {
                    isValidLocation = true;
                }

            }//End while statement
             
            //Exit early if not all trees could be placed
            if (errorStack == ForceAmount * 10)
            {
                break;
            }

            spawnLoc = new Vector3(spawnLocX, transform.position.y, spawnLocZ);
            SpawnTree(spawnLoc);
            isValidLocation = false;



        }//End for loop (all trees)

        //Delete this object once it is done spawning
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void SpawnTree(Vector3 spawnLocation)
    {
        //Add the tree to the list of positions
        savedPosArr[currentTree] = spawnLocation;
        currentTree++;

        //Create the tree
        GameObject tree = Instantiate(TreeToSpawn[Random.Range(0, TreeToSpawn.Length)], spawnLocation, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        tree.transform.SetParent(this.transform.parent);
    }
}
