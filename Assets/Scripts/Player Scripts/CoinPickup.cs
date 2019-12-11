using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour
{
    private int score;

    public Text scoreText;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            score++;
            Destroy(other.gameObject);
            scoreText.text = "Yer Loot: " + score;
        }
    }
}
