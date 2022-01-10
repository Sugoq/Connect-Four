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
    public bool activia;
    [SerializeField] Color initialColor;
    
    
    

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
    }
    private void OnMouseDown()
    {
        
        if (isChosen)
        {
            StopHighlight();
            HighlightController.instance.ClearHighlight();
            isChosen = !isChosen;
        }
        else if ((CheckersTable.instance.isWhite && color == PieceColor.WHITE)||    
                 (!CheckersTable.instance.isWhite && color == PieceColor.BLACK))
        {
             
            
            
            
            
            List<Vector2Int> availablePositions = CheckersTable.instance.GetAvailablePositions(position, color);
            HighlightController.instance.HighlightPositions(availablePositions,this);   
            StartCoroutine(PieceHighlight(0.5f));
            
            isChosen = !isChosen;
        }
            
           
       
        
        
    }
    public void Move(Vector2Int position)
    {
       
        CheckersTable.instance.SetTable(this.position, null);
        this.position = position;
        transform.position = position + CheckersTable.instance.GetOrigin();
        CheckersTable.instance.SetTable(this.position,this);
        HighlightController.instance.ClearHighlight();
        StopHighlight();
            CheckersTable.instance.isWhite = !CheckersTable.instance.isWhite;



    }


}
public enum PieceColor{
    WHITE, BLACK
}






