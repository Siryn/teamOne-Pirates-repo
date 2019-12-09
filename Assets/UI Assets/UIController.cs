using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuPanel, instructionsPanel, creditsPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OnClickInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void OnClickCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void OnClickBack()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OnClickPlay()
    {
        mainMenuPanel.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
