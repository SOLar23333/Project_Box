using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBotton : MonoBehaviour
{
    public Text btntext;
    AudioSource audioSource;
    [SerializeField] AudioClip gameStartAudio;
    private bool ispassed;
    private int theme;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().color = new Color32(0,0,0,120);
        GameObject fu = this.transform.parent.gameObject;
        GameObject ye = fu.transform.parent.gameObject;
        theme = ye.GetComponent<ThemeInformationSet>().theme;
        audioSource = GetComponent<AudioSource>();
        Text[] texts = this.gameObject.GetComponentsInChildren<Text>();
        btntext = texts[0];
        string levelName = "T"+theme.ToString()+"Level"+btntext.text;
        string[] passedLevels = ye.GetComponent<ThemeInformationSet>().passedLevels;
        ispassed = Findstrin(levelName,passedLevels);
        //Debug.Log(ispassed);



        if (ispassed)
        {
            this.GetComponent<Image>().color = new Color32(0,255,0,120);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool Findstrin(string a, string[] b)
    {
        foreach(string j in b)
        {
            if (j == a) return true;
        }
        return false;
    }

    public void EnterLevel()
    {
        StartCoroutine(Load_Level(btntext));
    }

    IEnumerator Load_Level(Text a)
    {
        Debug.Log(a.text);
        audioSource.PlayOneShot(gameStartAudio);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("T"+theme.ToString()+"Level"+a.text);
    }

}
