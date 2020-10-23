using System;
using UnityEngine;

public class KeyboardDetector : MonoBehaviour
{
    private GameObject[] boxes;
    private SceneLoader sceneLoader;
    private CanvasMemory canvasMemory;

    //public static event Action<SwipeData> OnSwipe = delegate { };

    private void Start()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        //Debug Only
        canvasMemory = (GameObject.Find("CanvasMemory") != null) ? GameObject.Find("CanvasMemory").GetComponent<CanvasMemory>() :
                                                                   GameObject.Find("CanvasMemory(Clone)").GetComponent<CanvasMemory>();
    }

    private void Update()
    {
        if (sceneLoader.gameEnded != true)
        {
            bool flag = true;
            SwipeDirection direction = DetectSwipe();
            if (direction != SwipeDirection.NULL)
            {
                foreach (GameObject box in boxes)
                {
                    if (Mathf.Abs(box.GetComponent<BoxMovement>().moveProgress - 1f) > Mathf.Epsilon)
                    {
                        flag = false;
                        Debug.Log("DONT MOVE");
                        break;
                    }
                }
                if (flag)
                {
                    foreach (GameObject box in boxes)
                    {
                        box.GetComponent<BoxMovement>().TryToMove(direction);
                    }
                }
            }
        }
    }
    //Detect Keyboard
    private SwipeDirection DetectSwipe()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return SwipeDirection.W;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            return SwipeDirection.A;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return SwipeDirection.S;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return SwipeDirection.D;
        }
        else
        {
            return SwipeDirection.NULL;
        }
    }

}
