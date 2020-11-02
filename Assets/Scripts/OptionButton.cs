﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    private CanvasGroup targetCanvasGroup;
    private CanvasGroup originCanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        GameObject optionCanvas = GameObject.Find("OptionCanvas");
        GameObject originCanvas = GameObject.Find("Canvas");
        originCanvasGroup = originCanvas.GetComponent<CanvasGroup>();
        targetCanvasGroup = optionCanvas.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoOptionPage()
    {
        StartCoroutine(LoadOptionCanvas(1.0f, 0.3f));
    }

    IEnumerator LoadOptionCanvas(float aValue, float aTime)
    {
        float alpha = targetCanvasGroup.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            float newAlpha = Mathf.Lerp(alpha, aValue, t);
            //Debug.Log(newAlpha);
            originCanvasGroup.alpha = 1.0f - newAlpha;
            targetCanvasGroup.alpha = newAlpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
    }
}
