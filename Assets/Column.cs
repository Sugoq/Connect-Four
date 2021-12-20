using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public bool[] full;
    public Transform[] positions;
    public GameObject arrow;
    public Transform initialPosition;

    public bool IsFull()
    {
        for(int i=0; i<full.Length; i++)
        {
            if (!full[i]) return false;
        }
        return true;
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
