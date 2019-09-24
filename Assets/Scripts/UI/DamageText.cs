using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    public Animator DamageTextAnimator;
    private Text damageText;
    private GameObject enemy;

    public GameObject Enemy
    {
        set
        {
            enemy = value;
        }
    }

    void OnEnable()
    {
        //Get the animation length, to know when to destroy
        AnimatorClipInfo[] clipInfo = DamageTextAnimator.GetCurrentAnimatorClipInfo(0);
        Destroy(this.gameObject, clipInfo[0].clip.length);

        //Get the text from the child (damage number to be shown)
        damageText = DamageTextAnimator.GetComponent<Text>();
    }

    private void Update()
    {
        Debug.Log("Updating number");
        //Get the location it should be printed on the screen
        Vector2 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
        transform.position = screenPos;
    }

    public void SetDamageText(string amountOfDamage)
    {
        //Set the text to the damage number
        damageText.text = amountOfDamage;
    }
}
