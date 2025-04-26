using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortality : MonoBehaviour
{

    public float hp;
    public float maxHp;


    void Awake()
    {
        maxHp = hp;
    }

    public void GiveHp(float giveHp)
    {
        hp += giveHp;
        if(hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void TakeHp(float takeHp)
    {
        hp -= takeHp;

        Debug.Log("Damage: " + takeHp);
        if (hp <= 0f)
        {
            Debug.Log("Die");
            Destroy(this.gameObject);
        }
    }
}
