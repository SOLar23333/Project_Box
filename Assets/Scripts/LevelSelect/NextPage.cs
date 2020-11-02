using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickNextPageButton()
    {
        StartCoroutine(LoadNextPage());
    }

    IEnumerator LoadNextPage()
    {
        yield return new WaitForSeconds(0.3f);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("1234");
        if (currentLevel == SceneManager.sceneCountInBuildSettings - 1) yield break;
        Debug.Log("5678");
        SceneManager.LoadScene(currentLevel + 1);
    }
}
