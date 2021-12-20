using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallColor color;
    public Column column;


    public void Setup(Column column)
    {
        this.column = column;
        StartCoroutine(Fall());
    }

    IEnumerator Fall() 
    {
        int index = -1;
        for(int i=0; i<column.full.Length; i++)
        {
            if (column.full[i] == false)
            {
                index = i;
                break;
            }
            
        }
        if (index != -1)
        {
            Vector3 finalPosition = column.positions[index].position;
            Vector3 initialPosition = column.initialPosition.position;
            float time = (finalPosition - initialPosition).magnitude / (column.positions[0].position - initialPosition).magnitude;
            for(float t = 0; t<1; t += Time.deltaTime*time)
            {
                transform.position = Vector3.Lerp(initialPosition, finalPosition, t);
                yield return null;
            }
            transform.position = finalPosition;
            column.full[index] = true;
            GameController.instance.isFalling = false;
        }    
    }    
    
}

public enum BallColor {
    RED, BLUE 
}
    