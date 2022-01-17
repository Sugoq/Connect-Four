using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    CheckersPiece piece;
    Vector2Int position;
    public void Setup(CheckersPiece piece, Vector2Int position)
    {
        this.position = position;
        this.piece = piece;
    }

   
    void Update()
    {

    }
    private void OnMouseDown()
    {
        piece.Move(position);

    }
    
}   


