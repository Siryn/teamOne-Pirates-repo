using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHp;
    public float currentHp;

    public Text healthText, gameResultsText, scoreResultsText;
    public Slider healthBar;

    public GameObject endScreen;

    public void Start()
    {
        healthText.text = "HP  " + currentHp + "/" + maxHp;

        endScreen.SetActive(false);
    }

    public void OnDeath()
    {

    }

    public void CheckDeath()
    {
        if (currentHp <= 0)
        {
            gameObject.SetActive(false);
            endScreen.SetActive(true);
            gameResultsText.text = "Game Over";
            scoreResultsText.text = "The Captain wins this round, lass.";
        }
    }

    public void TakeDamage(float damage)
    {
        print("ouch");
        currentHp -= damage;
        healthText.text = "HP  " + currentHp + "/" + maxHp;
        healthBar.value = currentHp / maxHp;
    }
}
