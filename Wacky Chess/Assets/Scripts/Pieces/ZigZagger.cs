using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagger : Piece
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
    /// Returns a list of possible tiles that this Zig Zagger Piece can move to. Zig Zagger Pieces move in a zig zag (2 diagonal one way, 2 diagonal another way)
    /// </summary>
    /// <returns></returns>
    public override List<Tile> PossibleMoves()
    {
        List<Tile> tempTileList = new List<Tile>();
        return tempTileList;
    }
}
