using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] GameObject redBall, blueBall;
    [SerializeField] bool isBlue;
    public bool isFalling;
    public Column[] columns;
    int closestIndex = -1;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !isFalling)
        {
            Vector3 mousepos = Input.mousePosition;
            Vector3 worldpos = Camera.main.ScreenToWorldPoint(mousepos);
            //print(worldpos);

            closestIndex = -1;
            float closestDist = 999999;
            for(int i = 0; i < columns.Length; i++)
            {
                float dist = Mathf.Abs (worldpos.x - columns[i].transform.position.x);
                if(dist < closestDist)
                {
                    closestIndex = i;
                    closestDist = dist;
                }  
            }
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i].arrow.SetActive(i == closestIndex);
            }





        }   
        else if (Input.GetMouseButtonUp(0) && !columns[closestIndex].IsFull())
        {
            Instantiate(isBlue ? blueBall : redBall, columns[closestIndex].initialPosition.position, Quaternion.identity).GetComponent<Ball>().Setup(columns[closestIndex]);
            closestIndex = -1;
            isBlue =!isBlue;
            isFalling = true;
        }
        else
        {
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i].arrow.SetActive(false);

            }
            closestIndex = -1;
        }
    }
}
