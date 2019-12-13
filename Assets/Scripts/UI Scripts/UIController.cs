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
    public AudioSource buttonClick;

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
        buttonClick.Play();
    }

    public void OnClickCredits()
    {
        creditsPanel.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickBack()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        buttonClick.Play();
    }

    public void OnClickPlay()
    {
        mainMenuPanel.SetActive(false);
        Invoke("CanShoot", 1f);
        StartCoroutine(WaitToFade(4f));
        buttonClick.Play();
    }

    public void OnClickQuit()
    {
        Application.Quit();
        buttonClick.Play();
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        buttonClick.Play();
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
