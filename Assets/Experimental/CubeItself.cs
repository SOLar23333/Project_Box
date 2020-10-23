using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeItself : MonoBehaviour
{

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = transform.TransformDirection(Vector3.back);
        //rigidbody.velocity = transform.position;
    }
}
