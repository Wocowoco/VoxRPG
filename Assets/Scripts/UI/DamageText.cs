using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    public Animator DamageTextAnimator;
    private Text damageText;
    private float posX, posY, posZ;

    private float randX = 0.0f;
    private float randY = 0.0f;
    private float randZ = 0.0f;

    public Transform EnemyPos
    {
        set
        {
            posX = value.position.x;
            posY = value.position.y;
            posZ = value.position.z;
        }
    }

    void OnEnable()
    {
        //Get the animation length, to know when to destroy
        AnimatorClipInfo[] clipInfo = DamageTextAnimator.GetCurrentAnimatorClipInfo(0);
        Destroy(this.gameObject, clipInfo[0].clip.length);

        //Get the text from the child (damage number to be shown)
        damageText = DamageTextAnimator.GetComponent<Text>();

        //Get random pos for the damage number
        randX = Random.Range(-0.25f, 0.25f);
        randY = Random.Range(-0.25f, 0.25f);
        randZ = Random.Range(-0.25f, 0.25f);
    }

    private void Update()
    {
        //Get the location it should be printed on the screen
        Vector2 screenPos = Camera.main.WorldToScreenPoint(new Vector3(posX + randX, posY+ randY + 0.5f , posZ + randZ));
        transform.position = screenPos;
    }

    public void SetDamageText(string amountOfDamage)
    {
        //Set the text to the damage number
        damageText.text = amountOfDamage;
    }
}
