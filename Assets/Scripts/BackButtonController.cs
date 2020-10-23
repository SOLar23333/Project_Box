using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{

    //AudioSource audioSource;
    //[SerializeField] AudioClip returnAudio;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainPage()
    {
        Debug.Log("Button Pressed");
        StartCoroutine(LoadMainPage());
    }


    IEnumerator LoadMainPage()
    {
        //audioSource.PlayOneShot(returnAudio);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(0);
    }
}
