using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuPanel, instructionsPanel, creditsPanel;
    public WeaponController weaponController;
    public Text feedbackText;

    void Start()
    {
        Screen.SetResolution(1024, 576, false);

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
        Invoke("CanShoot", 1f);
        StartCoroutine(WaitToFade(4f));
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator WaitToFade(float waitTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            feedbackText.CrossFadeAlpha(0.0f, 2f, false);
        }
    }

    public void CanShoot()
    {
        weaponController.canfire = true;
    }
}
