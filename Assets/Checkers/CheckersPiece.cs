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

    [Header("Highlight references")]
    public GameObject topLeft;
    public GameObject topRight, downLeft, downRight;




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
    private void OnMouseDown()
    {
        
        if (isChosen)
        {
            StopAllCoroutines();
            topRight.SetActive(false); topLeft.SetActive(false); downRight.SetActive(false); downLeft.SetActive(false);
            spriteRenderer.color = initialColor;
        }
        else
        {
            topLeft.SetActive(true); topRight.SetActive(true); downLeft.SetActive(true); downRight.SetActive(true);
            StartCoroutine(PieceHighlight(0.5f));
        }
        isChosen = !isChosen;
       
        
        
    }
    public void Move(Vector2 position)
    {
        this.position += Vector2Int.RoundToInt((Vector3)position - transform.position);
        transform.position = position;
    }
}
public enum PieceColor{
    WHITE, BLACK
}






