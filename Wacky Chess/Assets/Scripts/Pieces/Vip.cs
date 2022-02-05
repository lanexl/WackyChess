using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vip : Piece
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
    /// Returns a list of possible tiles that this Vip Piece can move to. Vip Pieces move and attack any adjacent square.
    /// </summary>
    /// <returns></returns>
    public override List<Tile> PossibleMoves()
    {
        List<Tile> tempTileList = new List<Tile>();
        return tempTileList;
    }
}
