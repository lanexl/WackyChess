using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variable to determine which player this is
    // Player 1 = white
    // Player 2 = black
    public int playerID;
    List<Piece> pieceList = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
        // Create and add all 16 pieces to the piecelist
        pieceList.Add(new ZigZagger());
        pieceList.Add(new ZigZagger());
        pieceList.Add(new ChainKiller());
        pieceList.Add(new ChainKiller());
        pieceList.Add(new Cannon());
        pieceList.Add(new Cannon());
        pieceList.Add(new SuicideBomber());
        pieceList.Add(new Vip());
        for (int i = 0; i < 8; i++)
        {
            pieceList.Add(new CannonFodder());
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Method to remove a piece that has been captured from this players piece list. Piece will be destroyed and then removed from the list
    /// </summary>
    public void removePiece(Piece pieceToRemove)
    {
        pieceList.Remove(pieceToRemove);
    }
}
