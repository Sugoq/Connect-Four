using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    public static HighlightController instance;
    public GameObject highlightPrefab;
    

    List<GameObject> highlightedPrefabs = new List<GameObject>();
    public void HighlightPositions(List<Vector2Int> positions, CheckersPiece piece)
    {
        ClearHighlight();
        Vector2 origin = CheckersTable.instance.GetOrigin();
        foreach (Vector2Int position in positions)
        {
            GameObject g = Instantiate(highlightPrefab, position + origin, Quaternion.identity);
            g.GetComponent<PieceMover>().Setup(piece, position);
            highlightedPrefabs.Add(g);
        }

    }

    public void ClearHighlight()
    {
        foreach (GameObject g in highlightedPrefabs) Destroy(g);
        highlightedPrefabs.Clear();
    }

    private void Awake() => instance = this;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
