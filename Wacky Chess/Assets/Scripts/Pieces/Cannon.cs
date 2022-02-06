using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Piece
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
    /// Returns a list of possible tiles that this Cannon Piece can move to. Cannon Pieces move to 1 adjacent square. 
    /// It attacks by shooting a piece within 2 rows in front of it in a 3X3 row. Cannon may not move and attack in the same turn.
    /// </summary>
    /// <returns></returns>
    public override List<Tile> PossibleMoves()
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
        List<Tile> tempTileList = new List<Tile>();
        return tempTileList;
    }
}
