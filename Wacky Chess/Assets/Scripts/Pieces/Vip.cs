using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vip : Piece
{
    protected bool inCheck;
    // Start is called before the first frame update
    void Start()
    {
        inCheck = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns a list of possible tiles that this Vip Piece can move to. Vip Pieces move and attack any adjacent square.
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
         * Left: x-1.5
         * Right: x+1.5
         * Up: y+1.5
         * Down: x+1.5
        */
        List<float> tempFloatList = new List<float>();
        return tempFloatList;
    }
}
