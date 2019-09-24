using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour {

    private static DamageText damageTextPrefab;
    private static GameObject ui;
    private static GameObject hitObject;
    private static DamageText instance;

    public static void Initialize()
    {
        if (!damageTextPrefab)
        {
            damageTextPrefab = Resources.Load<DamageText>("UI/DamageTextNode");
        }
        ui = GameObject.Find("UI");
    }

    public static void CreateDamageText(string amountOfDamage, GameObject enemy)
    {
        hitObject = enemy;
        
        //Create a new blanc damagetext
        instance = Instantiate(damageTextPrefab);
        instance.Enemy = enemy;

        //Add it to the UI
        instance.transform.SetParent(ui.transform, false);
        //Get the location it should be printed on the screen
        Vector2 screenPos = Camera.main.WorldToScreenPoint(hitObject.transform.position);
        instance.transform.position = screenPos;

        //Make it show the correct damage amount
        instance.SetDamageText(amountOfDamage);
    }
}
