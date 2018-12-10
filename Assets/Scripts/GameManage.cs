using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    public GameObject playerObject;


    //Make it so there's only one inventoryScript
    private static GameManage instance;

    public static GameManage MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManage>();
            }

            return instance;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
