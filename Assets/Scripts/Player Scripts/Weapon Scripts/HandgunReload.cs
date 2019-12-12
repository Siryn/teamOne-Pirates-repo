using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandgunReload : MonoBehaviour
{
    public float reloadTime = 6;
    public float timer;
    public bool timesUp;
    public Text handGunReloadtext;
    public Reload reloader;
    public WeaponController weaponController;

    public void Start()
    {
        Color c = handGunReloadtext.color;
        c.a = 0f;
        handGunReloadtext.color = c;
    }

    public IEnumerator WaitToReload()
    {
        timer = 0;

        while (timesUp == false)
        {
            timer += Time.deltaTime;

            if(timer >= reloadTime)
            {
                timesUp = true;
                reloader.shotsFiredInClip = 0;
                reloader.maxAmmo = 3;
                weaponController.canSwitch = true;
                OnHandGunBeingReloaded();
                Invoke("OnEndOfReloadText", 3f);
                yield return null;
            }
            yield return null;
        }
    }

    public void ReloadHandGun()
    {
        timesUp = false;
        StartCoroutine(WaitToReload());
    }

    public void OnHandGunBeingReloaded()
    {
        StartCoroutine(FadeIn(handGunReloadtext));
    }

    public void OnEndOfReloadText()
    {
        StartCoroutine(FadeOut(handGunReloadtext));
    }


    IEnumerator FadeIn(Text text)
    {
        for (float i = 0.1f; i < 1; i += 0.1f)
        {
            Color c = text.color;
            c.a = (i * 2);
            text.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut(Text text)
    {
        for (float i = 1f; i >= -0.1; i -= 0.1f)
        {
            Color c = text.color;
            c.a = i;
            text.color = c;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
