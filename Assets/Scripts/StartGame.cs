using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip gameStartAudio;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        StartCoroutine(LoadLastLevel(1));
    }


    IEnumerator LoadLastLevel(int level)
    {
        audioSource.PlayOneShot(gameStartAudio);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(level);
    }
}
