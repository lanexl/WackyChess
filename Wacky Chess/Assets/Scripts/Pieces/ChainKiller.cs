using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainKiller : Piece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns a list of possible tiles that this Chain Killer Piece can move to. 
    /// Chain Killer Pieces can move up to three times in a diagonal direction of the player's choice, and can attack any enemy tiles it lands on.
    /// </summary>
    /// <returns></returns>
    public override List<float> PossibleMoves()
    {
        /*
         * Possible Moves: 
         * Up-Left: x-1.5, y+1.5
         * Up-Right: x+1.5, y+1.5
         * Down-Left: x-1.5, y-1.5
         * Down-Right: x+1.5, y-1.5
        */
        List<float> tempFloatList = new List<float>();
        return tempFloatList;
    }
}
