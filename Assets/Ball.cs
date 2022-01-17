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
        for(int i=0; i<column.balls.Length; i++)
        {
            if (column.balls[i] == null)
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
            for(float t = 0; t<1; t += Time.deltaTime/time)
            {
                transform.position = Vector3.Lerp(initialPosition, finalPosition, t);
                yield return null;
            }
            transform.position = finalPosition;
            column.balls[index] = this;
            GameController.instance.isFalling = false;

            int sameColor = 0;
            for (int i = column.index; i<GameController.instance.columns.Length; i++) 
            {
                if (GameController.instance.columns[i].balls[index] == null) break;
                if (color == GameController.instance.columns[i].balls[index].color) sameColor++;
                else break;
            }
            for (int i = column.index -1; i >= 0; i--)
            {
                if (GameController.instance.columns[i].balls[index] == null) break;
                if (color == GameController.instance.columns[i].balls[index].color) sameColor++;
                else break;
            }
            if (sameColor >= 4) print("oi vc e gei");

            sameColor = 0;
            for (int i = index; i >=0; i--)
            {
                if (color == column.balls[i].color) sameColor++;
                else break;
            }
            if (sameColor >= 4) print("oi vc e gei vertical");

            sameColor = 0;
            for (int i = column.index, j = index;  i < GameController.instance.columns.Length; i++, j--)
            {
                if (j < 0) break;  
                if (GameController.instance.columns[i].balls[j] == null) break;
                if (color == GameController.instance.columns[i].balls[j].color)sameColor++;
                else break;
            }
        
            for (int i = column.index-1, j = index+1; i >= 0; i--, j++)
            {
                if (j >= GameController.instance.columns.Length) break;
                if (GameController.instance.columns[i].balls[j] == null) break;
                if (color == GameController.instance.columns[i].balls[j].color) sameColor++;
                else break;
            }
            if (sameColor >= 4) print("oi diagonau direita");

            sameColor = 0;
            for (int i = column.index, j = index; i < GameController.instance.columns.Length; i++, j++)
            {
                if (j >= GameController.instance.columns.Length) break;
                if (GameController.instance.columns[i].balls[j] == null) break;
                if (color == GameController.instance.columns[i].balls[j].color) sameColor++;
                else break;
            }

            for (int i = column.index - 1, j = index -1; i >= 0; i--, j--)
            {
                if (j<0) break;
                if (GameController.instance.columns[i].balls[j] == null) break;
                if (color == GameController.instance.columns[i].balls[j].color) sameColor++;
                else break;
            }
            if (sameColor >= 4) print("oi diagonau esquerda");









        }
    }    
    
}

public enum BallColor {
    RED, BLUE 
}
    