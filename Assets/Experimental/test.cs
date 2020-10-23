/*
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
    [SerializeField] float smoothTime = 1f;

    private string moveDirection = "";
    private float moveDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position;

        rigidBody = GetComponent<Rigidbody>();
        walls = GameObject.FindGameObjectsWithTag("Wall");
        boxes = GameObject.FindGameObjectsWithTag("Box");
        targets = GameObject.FindGameObjectsWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(moveProgress - 1f) < Mathf.Epsilon)
        {
            Debug.Log("ProcessInput");
            ProcessInput();
        }
        else
        {
            Debug.Log("SmoothMovement");
            SmoothMovement();
        }
    }

    private void SmoothMovement()
    {
        moveProgress += 7 * Time.deltaTime / smoothTime;
        if (moveProgress > 1) moveProgress = 1;
        fixedMoveProgress = Mathf.Sin(Mathf.PI / 2 * moveProgress);
        transform.position = Vector3.Lerp(startPos, endPos, fixedMoveProgress);
    }

    private void ProcessInput()
    {
        Vector3 nextPos;
        int boxCount = 0;
        bool flag = true;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDistance = 0f;
            moveDirection = "W";
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
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDistance = 0f;
            moveDirection = "S";
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
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDistance = 0f;
            moveDirection = "A";
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
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDistance = 0f;
            moveDirection = "D";
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
        }
    }

    private void SetDestination()
    {
        moveProgress = 0;
        startPos = transform.position;
        if (moveDirection == "W" || moveDirection == "S")
        {
            endPos = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
        }
        else if (moveDirection == "A" || moveDirection == "D")
        {
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
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

*/