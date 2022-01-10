using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersTable : MonoBehaviour
{
    public bool isWhite = true;
    public static CheckersTable instance;
    [SerializeField]
    CheckersPiece[,] table = new CheckersPiece[8,8];
    [SerializeField] public GameObject whitePiece, blackPiece;
    [SerializeField] Transform origin;

    private void Awake()
    {
        instance = this;
    }
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
    public Vector2 GetOrigin() => origin.position;
    public void SetTable(Vector2Int position, CheckersPiece piece)
    {
        table[position.y, position.x] = piece;
    }
    public List<Vector2Int> GetAvailablePositions(Vector2Int position, PieceColor color)
    {
       
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        if(color == PieceColor.WHITE)
        {
            if (position.y + 1 < 8 && position.x + 1 < 8)
            {
                if (table[position.y + 1, position.x + 1] == null)
                    availablePositions.Add(new Vector2Int(position.x + 1, position. y + 1));

                else if (table[position.y + 1, position.x + 1].color != color)

                    if (position.y + 2 < 8 && position.x + 2 < 8 && table[position.y + 2, position.x + 2] == null)
                        availablePositions.Add(new Vector2Int(position.x + 2, position.y + 2));
            }
            if (position.y + 1 < 8 && position.x - 1 >= 0)
            {
                if (table[position.y + 1, position.x - 1] == null)
                    availablePositions.Add(new Vector2Int(position.x - 1, position.y + 1));

                else if (table[position.y + 1, position.x - 1].color != color)

                    if (position.y + 2 < 8 && position.x - 2 >= 0 && table[position.y + 2, position.x - 2] == null)
                        availablePositions.Add(new Vector2Int(position.x - 2, position.y + 2));
            }
        
        }
        else
        {
            if (position.y - 1 >= 0 && position.x + 1 < 8)
            {
                if (table[position.y - 1, position.x + 1] == null)
                    availablePositions.Add(new Vector2Int(position.x + 1, position.y - 1));
                else if (table[position.y - 1, position.x + 1].color != color)
                    if (position.y - 2 >= 0 && position.x + 2 < 8 && table[position.y - 2, position.x + 2] == null)
                        availablePositions.Add(new Vector2Int(position.x + 2, position.y - 2));


            }
            if (position.y - 1 >= 0 && position.x - 1 >= 0)
            {
                if (table[position.y - 1, position.x - 1] == null)
                    availablePositions.Add(new Vector2Int(position.x - 1, position.y - 1));
                else if (table[position.y -1, position.x -1].color != color)
                    if (position.y - 2 >= 0 && position.x - 2 >= 0 && table[position.y - 2, position.x - 2] == null)
                        availablePositions.Add(new Vector2Int(position.x - 2, position.y - 2));


            }
        }
        return availablePositions;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


