using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    protected Vector2 gridPosition;
    public Vector2 GridPosition { get { return gridPosition; } }
    public GameGrid grid; // when added to the grid, the grid sets this variable to itself
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start begin");

        gridPosition = grid.WorldToTile(transform.position);
    }
}
