using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour {

    private static DamageText damageTextPrefab;
    private static DamageText critTextPrefab;
    private static GameObject ui;
    private static DamageText instance;

    public static void Initialize()
    {
        if (!damageTextPrefab)
        {
            damageTextPrefab = Resources.Load<DamageText>("UI/DamageTextNode");
            critTextPrefab = Resources.Load<DamageText>("UI/CritTextNode");
        }
        ui = GameObject.Find("UI");
    }

    public static void CreateDamageText(string amountOfDamage, GameObject enemy)
    {
        //Create a new blanc damagetext
        instance = Instantiate(damageTextPrefab);
        instance.EnemyPos = enemy.transform;

        //Add it to the UI
        instance.transform.SetParent(ui.transform, false);

        //Make it show the correct damage amount
        instance.SetDamageText(amountOfDamage);
    }


    public static void CreateCritText(string amountOfDamage, GameObject enemy)
    {
        //Create a new blanc damagetext
        instance = Instantiate(critTextPrefab);
        instance.EnemyPos = enemy.transform;

        //Add it to the UI
        instance.transform.SetParent(ui.transform, false);

        //Make it show the correct damage amount
        instance.SetDamageText(amountOfDamage);
    }
}
