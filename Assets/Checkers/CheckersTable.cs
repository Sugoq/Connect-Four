using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersTable : MonoBehaviour
{
    [SerializeField]
    CheckersPiece[,] table = new CheckersPiece[8,8];
    [SerializeField] GameObject whitePiece, blackPiece;
    [SerializeField] Transform origin;
    
    void Start()
    {
        
        for(int i =0; i < 3; i++)
        {
            for(int j= (i % 2 == 0 ? 0 : 1); j<8; j += 2)
            {
                CheckersPiece piece = Instantiate(whitePiece, transform).GetComponent<CheckersPiece>();
                piece.position = new Vector2Int(j, i);
                piece.transform.position = new Vector3(j, i) + origin.position;
                table[i, j] = piece;
            }
        }

        for (int i = 5; i < 8; i++)
        {
            for (int j = (i % 2 == 0 ? 0 : 1); j < 8; j += 2)
            {
                CheckersPiece piece = Instantiate(blackPiece, transform).GetComponent<CheckersPiece>();
                piece.position = new Vector2Int(j , i);
                piece.transform.position = new Vector3(j , i) + origin.position;
                table[i, j] = piece;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


