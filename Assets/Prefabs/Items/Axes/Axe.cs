using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "Items/Axe", order = 1)]
public class Axe : Item {

    [SerializeField]
    private int axeTier = 0;

    public int MyAxeTier
    {
        get
        {
            return axeTier;
        }
    }

}
