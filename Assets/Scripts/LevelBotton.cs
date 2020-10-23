using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBotton : MonoBehaviour
{
    public Text BtnText;
    AudioSource audioSource;
    [SerializeField] AudioClip gameStartAudio;
    [SerializeField] bool IsPassed;
    [SerializeField] int theme;

    // Start is called before the first frame update
    void Start()
    {
        //sth wrong with this.....
        this.GetComponent<Image>().color = new Color32(0,0,0,120);
        if (IsPassed)
        {
            this.GetComponent<Image>().color = new Color32(0,255,0,120);
        }
        audioSource = GetComponent<AudioSource>();
        Text[] texts = this.gameObject.GetComponentsInChildren<Text>();
        BtnText = texts[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterLevel()
    {
        Debug.Log("NMSL");
        StartCoroutine(Load_Level(BtnText));
    }

    IEnumerator Load_Level(Text a)
    {
        Debug.Log(a.text);
        audioSource.PlayOneShot(gameStartAudio);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Level"+a.text);
    }
}
