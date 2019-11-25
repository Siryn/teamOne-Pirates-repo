using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunReload : MonoBehaviour
{
    public float reloadTime = 6;
    public float timer;
    public bool timesUp;

    public Reload reloader;

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
                yield return null;
            }
            yield return null;
        }
    }

    public void ReloadHandGun()
    {
        StartCoroutine(WaitToReload());
    }
}
