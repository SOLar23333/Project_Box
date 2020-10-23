using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIController : MonoBehaviour
{

    GameObject currentLevel;
    private CanvasGroup canvasGroup;
    private float appearTime = 0.3f;

    private void OnEnable()
    {
        currentLevel = GameObject.Find("CurrentLevel");
        canvasGroup = GetComponent<CanvasGroup>();
        currentLevel.GetComponent<Text>().text = "Level " + (SceneManager.GetActiveScene().buildIndex).ToString();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator AdjustCanvasAlpha(float aValue, float aTime)
    {
        float alpha = canvasGroup.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            float newAlpha = Mathf.Lerp(alpha, aValue, t);
            //Debug.Log(newAlpha);
            canvasGroup.alpha = newAlpha;
            yield return null;
        }
        yield return new WaitForSeconds(appearTime);
    }

    public IEnumerator GraduallyAppearCanvas()
    {
        StartCoroutine(AdjustCanvasAlpha(1.0f, appearTime));
        yield return new WaitForSeconds(appearTime);
    }

    public IEnumerator GraduallyDisappearCanvas()
    {
        StartCoroutine(AdjustCanvasAlpha(0.0f, appearTime));
        yield return new WaitForSeconds(appearTime);
        gameObject.SetActive(false);

    }
}
