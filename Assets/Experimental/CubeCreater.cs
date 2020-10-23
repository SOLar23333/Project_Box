using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCreater : MonoBehaviour
{

    GameObject uselessCube;
    [SerializeField] GameObject anotherUselessCube;
    [SerializeField] GameObject refPt;

    // Start is called before the first frame update
    void Start()
    {
        uselessCube = GameObject.Find("JustACube");
        //StartCoroutine("HideCube", 3f);
        StartCoroutine("CreateCube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HideCube(float livetime)
    {
        Debug.Log("222");
        uselessCube.SetActive(false);
        yield return new WaitForSeconds(livetime);
        uselessCube.SetActive(true);
    }

    IEnumerator CreateCube()
    {
        Debug.Log("NMSL111");
        Debug.Log(uselessCube.tag);
        Debug.Log(uselessCube.gameObject.tag);
        Instantiate(anotherUselessCube, refPt.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3);
        Debug.Log("NMSL000");
    }
}
