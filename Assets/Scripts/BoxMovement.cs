using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxMovement : MonoBehaviour
{
    Rigidbody rigidBody;

    public GameObject[] walls;
    public GameObject[] boxes;
    public GameObject[] targets;

    public float moveProgress = 1f;
    private float fixedMoveProgress;
    private Vector3 startPos, endPos;
    [SerializeField] float smoothTime = 0.5f;

    private string moveDirection = "";
    private float moveDistance = 0f;

    //temp change
    //public SwipeDetector swipeDetector;
    //public KeyboardDetector swipeDetector;

    private void OnEnable()
    {
        startPos = transform.position;
        endPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        walls = GameObject.FindGameObjectsWithTag("Wall");
        boxes = GameObject.FindGameObjectsWithTag("Box");
        targets = GameObject.FindGameObjectsWithTag("Target");
        //temp change
        //swipeDetector = GameObject.Find("SwipeDetector").GetComponent<SwipeDetector>();
        //swipeDetector = GameObject.Find("KeyboardDetector").GetComponent<KeyboardDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToMove(SwipeDirection direction)
    {
        if (Mathf.Abs(moveProgress - 1f) < Mathf.Epsilon)
        {
            ProcessInput(direction);
        }
    }

    private IEnumerator SmoothMovement(Vector3 destination)
    {
        Vector3 currentPos = transform.position;
        for (moveProgress = 0.0f; moveProgress < 1.0f; moveProgress += Time.deltaTime / smoothTime)
        {
            float fixedMoveProgress = Mathf.Sin(Mathf.PI / 2 * moveProgress);
            transform.position = Vector3.Lerp(currentPos, destination, fixedMoveProgress);
            yield return null;
        }
        moveProgress = 1.0f;
        transform.position = destination;
    }

    private void ProcessInput(SwipeDirection direction)
    {
        Vector3 nextPos;
        int boxCount = 0;
        bool flag = true;

        switch (direction)
        {
            case SwipeDirection.W:
                moveDistance = 0f;
                moveDirection = "W";
                //Debug.Log(direction);
                while (flag)
                {
                    nextPos = new Vector3(transform.localPosition.x + moveDistance + 2, transform.localPosition.y, transform.localPosition.z);
                    if (CheckPosClear(nextPos, walls))
                    {
                        if (!CheckPosClear(nextPos, boxes)) boxCount++;
                        moveDistance += 2;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                moveDistance -= boxCount * 2;
                SetDestination();
                break;

            case SwipeDirection.A:
                moveDistance = 0f;
                moveDirection = "A";
                //Debug.Log(direction);
                while (flag)
                {
                    nextPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + moveDistance + 2);
                    if (CheckPosClear(nextPos, walls))
                    {
                        if (!CheckPosClear(nextPos, boxes)) boxCount++;
                        moveDistance += 2;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                moveDistance -= boxCount * 2;
                SetDestination();
                break;

            case SwipeDirection.S:
                moveDistance = 0f;
                moveDirection = "S";
                //Debug.Log(direction);
                while (flag)
                {
                    nextPos = new Vector3(transform.localPosition.x + moveDistance - 2, transform.localPosition.y, transform.localPosition.z);
                    if (CheckPosClear(nextPos, walls))
                    {
                        if (!CheckPosClear(nextPos, boxes)) boxCount++;
                        moveDistance -= 2;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                moveDistance += boxCount * 2;
                SetDestination();
                break;

            case SwipeDirection.D:
                moveDistance = 0f;
                moveDirection = "D";
                //Debug.Log(direction);
                while (flag)
                {
                    nextPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + moveDistance - 2);
                    if (CheckPosClear(nextPos, walls))
                    {
                        if (!CheckPosClear(nextPos, boxes)) boxCount++;
                        moveDistance -= 2;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                moveDistance += boxCount * 2;
                SetDestination();
                break;

            default:
                break;
        }
    }

    private void SetDestination()
    {
        //If no movement required, return
        if (Mathf.Abs(moveDistance) < Mathf.Epsilon)
        {
            return;
        }
        moveProgress = 0;
        startPos = transform.position;
        if (moveDirection == "W" || moveDirection == "S")
        {
            endPos = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMovement(endPos));
        }
        else if (moveDirection == "A" || moveDirection == "D")
        {
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
            StartCoroutine(SmoothMovement(endPos));
        }
        else
        {
            Debug.Log("Something is wrong");
        }
        
    }

    private bool CheckPosClear(Vector3 currentPos, GameObject[] objs)
    {
        foreach (GameObject obj in objs)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider.bounds.Contains(currentPos))
            {
                return false;
            }
        }
        return true;
    }
}

