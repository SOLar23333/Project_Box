using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMemory : MonoBehaviour
{

    public bool isCanvasEnabled = false;
    //public bool isOptionCanvasEnabled = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
