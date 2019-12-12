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
    public EnemyPatrol enemyPatrol;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        if (isBoss)
        {
            keyfab.SetActive(false);
        }
    }

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
            enemyIsDead = true;
            enemyPatrol.pathFinder.Agent.isStopped = true;
            anim.SetBool("onDeath", true);
            anim.SetBool("playOnce", true);
            Invoke("OnlyPlayOnce", 0.1f);
            Invoke("OnDeath", 3);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    public void OnlyPlayOnce()
    {
        anim.SetBool("playOnce", false);
    }
}
