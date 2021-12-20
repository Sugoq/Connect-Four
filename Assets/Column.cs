using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public int index;
    public Ball[] balls;
    public Transform[] positions;
    public GameObject arrow;
    public Transform initialPosition;

    public bool IsFull()
    {
        for(int i=0; i<balls.Length; i++)
        {
            if (balls[i] == null) return false;
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
