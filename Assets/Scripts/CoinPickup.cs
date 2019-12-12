using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour
{
    private int score = 0;
    private bool hasKey = false;

    public Text scoreText, feedbackText, gameResultsText, scoreResultsText;
    public GameObject keyIcon, endScreen;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            score += 15;
            Destroy(other.gameObject);
            scoreText.text = "$ " + score;
        }

        else if(other.CompareTag("Key"))
        {
            hasKey = true;
            Debug.Log("got the key");
            Destroy(other.gameObject);
            keyIcon.SetActive(true);
        }

        else if(other.CompareTag("Chest"))
        {
            endScreen.SetActive(true);
            gameResultsText.text = "Victory!";
            scoreResultsText.text = "Loot Recovered:\n$" + score;
            score += 100;
            scoreText.text = "$ " + score;
            Destroy(other.gameObject);
        }

        else if(other.CompareTag("Door") && hasKey)
        {
            Destroy(other.gameObject);
            keyIcon.SetActive(false);
        }
        else if(other.CompareTag("Door") && !hasKey)
        {
            feedbackText.CrossFadeAlpha(1.0f, 0.0f, false);
            feedbackText.text = "It's locked.";
            feedbackText.CrossFadeAlpha(0.0f, 3f, false);
        }
    }

}
