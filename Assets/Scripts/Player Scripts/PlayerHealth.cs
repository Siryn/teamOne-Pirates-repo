using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp;
    public int currentHp;

    public void OnDeath()
    {

    }

    public void CheckDeath()
    {
        if (currentHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
