using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool enemyIsDead;
    public int maxHp;
    public int currentHp;
    public Animator anim;
    public bool isBoss;
    public GameObject keyfab;

    public void OnDeath()
    {
        if(isBoss)
        {
            keyfab.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void CheckDeath()
    {
        if(currentHp <= 0)
        {
            anim.SetBool("onDeath", true);
            Invoke("OnDeath", 5);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
}
