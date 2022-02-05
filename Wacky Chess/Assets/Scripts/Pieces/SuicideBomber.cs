using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomber : Piece
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
    /// Returns a list of possible tiles that this Suicide Bomber Piece can move to. Suicide Bomber pieces move 1 square 
    /// adjacent and may use a turn to arm a bomb. At the end of the next turn, bomb detonates and detroys this piece and any adjacent pieces
    /// (friendly or enemy). Piece cannot move and arm bomb during the same turn. Also cannot move after bomb is armed.
    /// </summary>
    /// <returns></returns>
    public override List<Tile> PossibleMoves()
    {
        List<Tile> tempTileList = new List<Tile>();
        return tempTileList;
    }
}
