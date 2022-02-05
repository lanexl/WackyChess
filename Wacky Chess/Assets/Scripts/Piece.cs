using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Tile
{
    // Start is called before the first frame update
    void Start()
    {
        // The tile this piece is currently on
        // Tile currentLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Method to return a list of tiles that this piece can move to. This will be overridden by each piece type for their movement rules.
    /// </summary>
    /// <returns></returns>
    public virtual List<Tile> PossibleMoves()
    {
        List<Tile> tempTileList = new List<Tile>();
        return tempTileList;
    }

    /// <summary>
    /// Method to move this piece to a new location
    /// </summary>
    /// <param name="newLocation"></param>
    public void MovePiece(Tile newLocation)
    {

    }

    public void MatchTileToPosition()
    {
        gridPosition = grid.WorldToTile(transform.position);
    }
}
