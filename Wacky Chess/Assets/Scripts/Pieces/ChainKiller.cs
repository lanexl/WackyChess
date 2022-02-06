﻿using System.Collections;
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
    /// Returns a list of possible tiles that this Chain Killer Piece can move to. Chain Killer Pieces can attack any piece in a forward diagonal square and continue attacking in a chain up to 3 pieces total.
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
        */
        List<Tile> tempTileList = new List<Tile>();
        return tempTileList;
    }
}
