using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject white, black;
    private void Awake()
    {
        instance = this;
    }

    public void EndGame(PieceColor color)
    {
        white.SetActive(color == PieceColor.WHITE);
        black.SetActive(color == PieceColor.BLACK);

    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
