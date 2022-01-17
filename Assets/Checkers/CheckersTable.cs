using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckersTable : MonoBehaviour
{
    public bool isWhite = true;
    public static CheckersTable instance;
    [SerializeField]
    CheckersPiece[,] table = new CheckersPiece[8,8];
    [SerializeField] public GameObject whitePiece, blackPiece;
    [SerializeField] Transform origin;
    List<CheckersPiece> whitePieces = new List<CheckersPiece>(), blackPieces = new List<CheckersPiece>();



    private void Awake()
    {
        instance = this;
    }
    public void StopPieceHighlight(CheckersPiece exception = null)
    {
        foreach (CheckersPiece piece in whitePieces)
        {
            piece.StopHighlight();
        }
        foreach (CheckersPiece piece in blackPieces)
        {
            piece.StopHighlight();
        }
        if (exception != null) exception.StartHighlight();
        
    }
 
    public void DestroyPiece(CheckersPiece piece)
    {
        SetTable(piece.position, null);
        if (piece.color == PieceColor.BLACK) blackPieces.Remove(piece);
        else whitePieces.Remove(piece);
        Destroy(piece.gameObject);
        if (whitePieces.Count == 0) UIController.instance.EndGame(PieceColor.BLACK);
        else if (blackPieces.Count == 0) UIController.instance.EndGame(PieceColor.WHITE);
            
    }
    public void DestroyPiece(Vector2Int position)
    {
        if (GetTable(position) != null) DestroyPiece(GetTable(position));

    }
    public void ChangeTurn()
    {
        isWhite = !isWhite;
        var list = isWhite ? whitePieces : blackPieces;
        List<CheckersPiece> canEat = new List<CheckersPiece>();
        foreach(CheckersPiece piece in list)
        {
            if(piece.isQueen && CheckQueenAnyKillable(piece.position, piece.color))
            {
                canEat.Add(piece);
            }       
            
            else if(CheckAnyKillable(piece.position, piece.color))
            {
                canEat.Add(piece);
            }
        }
        if (canEat.Count > 0)
        {
            foreach (CheckersPiece piece in list) piece.selectable = false;
            foreach (CheckersPiece piece in canEat) piece.selectable = true;
        }
        else foreach (CheckersPiece piece in list) piece.selectable = true;
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
                whitePieces.Add(piece);
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
                blackPieces.Add(piece);
            }
        }
        
    }
    public Vector2 GetOrigin() => origin.position;
    public void SetTable(Vector2Int position, CheckersPiece piece)
    {
        table[position.y, position.x] = piece;
    }
    public CheckersPiece GetTable(Vector2Int position)
    {
        return table[position.y,position.x];
    }
    public bool CheckAnyKillable(CheckersPiece piece) =>CheckAnyKillable(piece.position,piece.color);
    public bool CheckAnyKillable(Vector2Int position, PieceColor color)
    {
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = -1; j < 2; j += 2)
            {

                if (InsideBounds(position.x + j, position.y + i))
                {
                    if (table[position.y + i, position.x + j] != null && table[position.y + i, position.x + j].color != color)
                    {
                        if (InsideBounds(position.x + 2 * j, position.y + 2 * i))
                        {
                            if (table[position.y + 2 * i, position.x + 2 * j] == null)
                            {
                                //print($"linha: {position.y +2 * i}, coluna: {position.x +2 *j}");
                                return true;
                            }    
                        }
                    }
                }
            }
        }
        return false;
    }

    public List<Vector2Int> GetQueenAvailablePositions(Vector2Int position, PieceColor color)
    {
        List<Vector2Int> move = new List<Vector2Int>();
        List<Vector2Int> kill = new List<Vector2Int>();
        QueenDiagonal(position, color, new Vector2Int(-1, -1), ref move, ref kill, false);
        QueenDiagonal(position, color, new Vector2Int(-1, 1), ref move, ref kill, false);
        QueenDiagonal(position, color, new Vector2Int(1, -1), ref move, ref kill, false);
        QueenDiagonal(position, color, new Vector2Int(1, 1), ref move, ref kill, false);
        if (kill.Count > 0) return kill;
        return move;
    }
    public bool CheckQueenAnyKillable(Vector2Int position, PieceColor color)
    {
        List<Vector2Int> move = new List<Vector2Int>();
        List<Vector2Int> kill = new List<Vector2Int>();
        QueenDiagonal(position, color, new Vector2Int(-1, -1), ref move, ref kill, false);
        QueenDiagonal(position, color, new Vector2Int(-1, 1), ref move, ref kill, false);
        QueenDiagonal(position, color, new Vector2Int(1, -1), ref move, ref kill, false);
        QueenDiagonal(position, color, new Vector2Int(1, 1), ref move, ref kill, false);
        return kill.Count > 0;
        
    }
    private void QueenDiagonal(Vector2Int pos, PieceColor color, Vector2Int dir, ref List<Vector2Int> move, ref List<Vector2Int> kill, bool foundEnemy)
    {
        Vector2Int currPos = pos + dir;
        if (!InsideBounds(currPos.x, currPos.y)) return;
        CheckersPiece piece = GetTable(currPos);
        if (piece == null)
        {
            if (foundEnemy) kill.Add(currPos);
            else move.Add(currPos);
        }
        else if (piece.color == color) return;
        else
        {
            if (!foundEnemy) foundEnemy = true;
            else return;
        }
        QueenDiagonal(currPos, color, dir, ref move, ref kill, foundEnemy);
    }
        
      






    public List<Vector2Int> GetAvailablePositions(Vector2Int position, PieceColor color)
    {
       
        List<Vector2Int> availablePositions = new List<Vector2Int>();

        for(int i = -1; i<2; i += 2)
        {
            for(int j = -1; j<2; j += 2)
            {
                
                if(InsideBounds(position.x + j, position.y + i))
                {
                    if (table[position.y + i, position.x + j] != null && table[position.y + i, position.x + j].color != color)
                    {
                        if (InsideBounds(position.x +2 * j, position.y +2* i))
                        {
                            if (table[position.y + 2 * i, position.x + 2 * j]==null)
                                availablePositions.Add(new Vector2Int(position.x + 2 * j, position.y + 2 * i));                       
                        }
                    }
                }
            }
        }
        if (availablePositions.Count > 0) return availablePositions;
        
        int k = (color == PieceColor.WHITE ? 1 : -1);
        for (int j = -1; j < 2; j += 2)
        {
            if (InsideBounds(position.x + j, position.y + k))
            {
                if (table[position.y + k, position.x + j] == null)
                {
                    availablePositions.Add(new Vector2Int(position.x + j, position.y + k));
                }
            }    
        }  
        return availablePositions;
    }
    bool InsideBounds(int x, int y)
    {
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }
        







    void Update()
    {
        
    }
}


