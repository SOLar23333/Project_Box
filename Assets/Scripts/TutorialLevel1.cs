using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel1 : MonoBehaviour
{

    Animator animator;
    GameObject theBox;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        theBox = GameObject.Find("Box (1)");
        StartCoroutine(WaitForStart());
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isStep1", theBox.transform.position.z < -2.99f);
        if (theBox.transform.position.x < -3f)
        {
            animator.SetTrigger("endGame");
        }
    }

    IEnumerator WaitForStart()
    {
        animator.enabled = false;
        yield return new WaitForSeconds(3.5f);
        animator.enabled = true;
    }
}
