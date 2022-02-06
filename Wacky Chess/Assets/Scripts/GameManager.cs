using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Integer indicating which players turn it currently is
        // 1 = Player 1's / Whites turn
        // 2 = Player 2'1 / Blacks turn
        //int activePlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void HelpLoad()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Method to Check for win conditions at the end of each turn
    /// </summary>
    /// <returns></returns>
    public bool CheckWinConditions()
    {
        return false;
    }


}
