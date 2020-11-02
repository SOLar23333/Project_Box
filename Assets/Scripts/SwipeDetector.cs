using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerUpPosition;
    private Vector2 fingerDownPosition;

    [SerializeField] private float minDistanceForSwipe = 20f;

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
            SwipeDirection direction = DetectTouchInput();
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

    public SwipeDirection DetectTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                if (SwipeDistanceCheckMet())
                {
                    if (fingerUpPosition.y > fingerDownPosition.y && fingerUpPosition.x > fingerDownPosition.x)
                    {
                        fingerDownPosition = fingerUpPosition;
                        return SwipeDirection.W;
                    }
                    else if (fingerUpPosition.y > fingerDownPosition.y && fingerUpPosition.x < fingerDownPosition.x)
                    {
                        fingerDownPosition = fingerUpPosition;
                        return SwipeDirection.A;
                    }
                    else if (fingerUpPosition.y < fingerDownPosition.y && fingerUpPosition.x < fingerDownPosition.x)
                    {
                        fingerDownPosition = fingerUpPosition;
                        return SwipeDirection.S;
                    }
                    else if (fingerUpPosition.y < fingerDownPosition.y && fingerUpPosition.x > fingerDownPosition.x)
                    {
                        fingerDownPosition = fingerUpPosition;
                        return SwipeDirection.D;
                    }
                }
                else
                {
                    if (sceneLoader.canvas.activeSelf)
                    {
                        Debug.Log("1");
                        canvasMemory.isCanvasEnabled = false;
                        sceneLoader.canvas.GetComponent<UIController>().GraduallyDisappearCanvas();
                        StartCoroutine(sceneLoader.canvas.GetComponent<UIController>().GraduallyDisappearCanvas());
                        //canvas.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("0");
                        canvasMemory.isCanvasEnabled = true;
                        sceneLoader.canvas.gameObject.SetActive(true);
                        StartCoroutine(sceneLoader.canvas.GetComponent<UIController>().GraduallyAppearCanvas());
                        GameObject optionCanvas = GameObject.Find("OptionCanvas");
                        StartCoroutine(optionCanvas.GetComponent<UIController>().GraduallyDisappearCanvas());
                    }
                }
            }
        }
        return SwipeDirection.NULL;
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerUpPosition.y - fingerDownPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerUpPosition.x - fingerDownPosition.x);
    }

}

public enum SwipeDirection
{
    W,
    A,
    S,
    D,
    NULL
}
