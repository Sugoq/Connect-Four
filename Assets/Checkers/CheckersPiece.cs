using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersPiece : MonoBehaviour
{
    public Vector2Int position;
    public bool isQueen;
    public PieceColor color;
    SpriteRenderer spriteRenderer;
    [SerializeField] Color finalColor;
    bool isChosen;
    public bool selectable = true;
    [SerializeField] Color initialColor;
    [SerializeField] GameObject crown;
    
    
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PieceHighlight(float timer)
    {
        

        while (true)
        {

            for (float t = 0; t < 1; t += Time.deltaTime / timer)
            {
                spriteRenderer.color = Color.Lerp(initialColor, finalColor, t);
                yield return null;
            }
            spriteRenderer.color = finalColor;
            yield return null;
            for (float t = 0; t < 1; t += Time.deltaTime / timer)
            {
                spriteRenderer.color = Color.Lerp(initialColor, finalColor, 1 - t);
                yield return null;
            }
            spriteRenderer.color = initialColor;
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void StopHighlight()
    {
        StopAllCoroutines();
        spriteRenderer.color = initialColor;
        isChosen = false;
    }
    private void OnMouseDown()
    {
        if (!selectable) return;
        if (isChosen)
        {
            StopHighlight();
            HighlightController.instance.ClearHighlight();
            isChosen = false;
        }
        else if ((CheckersTable.instance.isWhite && color == PieceColor.WHITE)||        
                 (!CheckersTable.instance.isWhite && color == PieceColor.BLACK))
        {

            List<Vector2Int> availablePositions = isQueen ? CheckersTable.instance.GetQueenAvailablePositions(position,color): 
                                                            CheckersTable.instance.GetAvailablePositions(position, color);
            HighlightController.instance.HighlightPositions(availablePositions, this);
            CheckersTable.instance.StopPieceHighlight(this);
            
            isChosen = true;
        }
    }

    public void StartHighlight()
    {
        StopAllCoroutines();
        StartCoroutine(PieceHighlight(0.5f));
    }

    public void Move(Vector2Int position)
    {
        bool killed = false;
        Vector2Int dir = new Vector2Int(position.x>this.position.x?1:-1, position.y>this.position.y? 1:-1);
        for (Vector2Int v = this.position + dir; v != position; v += dir)
        {
            CheckersPiece piece = CheckersTable.instance.GetTable(v);
            print($"Visitando casa {v} ");
            if(piece!= null && piece.color != color)
            {
                CheckersTable.instance.DestroyPiece(piece);
                killed = true; break;
            }
        }
        
        CheckersTable.instance.SetTable(this.position, null);
        this.position = position;
        transform.position = position + CheckersTable.instance.GetOrigin();
        CheckersTable.instance.SetTable(this.position,this);
        if (position.y == 7 && color == PieceColor.WHITE)
        {
            isQueen = true;
            crown.SetActive(true);

        }
        if (position.y == 0 && color == PieceColor.BLACK)
        {
            isQueen = true;
            crown.SetActive(true);
        }
        HighlightController.instance.ClearHighlight();
        if (killed)
        {
            
            if (!CheckersTable.instance.CheckAnyKillable(this))
            {
                CheckersTable.instance.ChangeTurn();
                StopHighlight();
            }
            else
            {
                
                HighlightController.instance.HighlightPositions(CheckersTable.instance.GetAvailablePositions(position, color), this);
            }        
        }
        else
        {
            CheckersTable.instance.ChangeTurn();
            StopHighlight();
        }   
            
       
        


        



    }


}
public enum PieceColor{
    WHITE, BLACK
}






