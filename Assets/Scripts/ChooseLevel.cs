using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip ButtonAudio;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterLevelChoosingPage()
    {
        StartCoroutine(LoadLastLevel1());
    }


    IEnumerator LoadLastLevel1()
    {
        audioSource.PlayOneShot(ButtonAudio);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("LevelChoosingPage");
    }
}
