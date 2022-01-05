using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersPiece : MonoBehaviour
{
    public Vector2Int position;
    public bool isQueen;
    public PieceColor color;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum PieceColor{
    WHITE, BLACK
}
