using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;
    private float timer = 0f;
    private float transitionTime = 2f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (isFirstUpdate && timer >= transitionTime)
        {
            isFirstUpdate = false;

            Loader.LoaderCallback();
        }
    }
}
