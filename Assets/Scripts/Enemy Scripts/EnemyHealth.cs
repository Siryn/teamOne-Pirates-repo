using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp;
    public int currentHp;

    public void OnDeath()
    {

    }

    public void CheckDeath()
    {
        if(currentHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
}
