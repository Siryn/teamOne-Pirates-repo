using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour
{
    private int score = 0;
    private bool hasKey = false;

    public Text scoreText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            score++;
            Destroy(other.gameObject);
            scoreText.text = "Yer Loot: " + score;
        }

        if(other.CompareTag("Key"))
        {
            hasKey = true;
            Debug.Log("got the key");
        }

        if(other.CompareTag("Chest"))
        {
            //level over
            score += 100;
            
        }

        if (other.CompareTag("Door") && hasKey == true)
        {
            Destroy(other.gameObject);
        }
    }

}
