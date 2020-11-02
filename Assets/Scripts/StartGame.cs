using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

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
        string current;
        try 
        {
            CurrentlevelSaving currentlevelSaving = JsonUtility.FromJson<CurrentlevelSaving>( File.ReadAllText( Application.persistentDataPath + "/current_level_save.json" ) );
            current = currentlevelSaving.current;
        }
        catch
        {
            current = "T1Level1";
        }
        StartCoroutine(LoadLastLevel(current));
    }


    IEnumerator LoadLastLevel(string level)
    {
        audioSource.PlayOneShot(gameStartAudio);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(level);
    }
}
