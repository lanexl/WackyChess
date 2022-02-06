using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right,
    Diagonal
}


public class GameGrid : MonoBehaviour
{
    public int dims = 10;
    private List<Piece> bluePieceList;
    private List<Piece> redPieceList;
    private Rect area;
    private Piece dragTarget; // Piece being clicked and dragged
    public bool SetupPhase; // true: setup phase, false: battle phase
    private GameObject tileSelector;
    private Color lightBlue = new Color(40f / 255f, 1f, 229f / 255f);
    private Color lightGreen = new Color(134f / 255f, 1f, 40f / 255f);
    private Color red = new Color(186f / 255f, 27f / 255f, 27f / 255f);
    bool bluesTurn;
    bool redsTurn;

    public float TileLength { get { return transform.localScale.x / dims; } } // assumes grid is square and width = scale
    public Rect Area { get { return area; } }
    public List<Piece> BluePieceList { get { return bluePieceList; } }
    public List<Piece> RedPieceList { get { return redPieceList; } }
    public int PlaceLimit { get { return 4; } }
    public GameObject TileSelector { get { return tileSelector; } }
    public Color LightBlue { get { return lightBlue; } }
    public Color LightGreen { get { return lightGreen; } }
    public Color Red { get { return red; } }
    public bool Dragging { get { return dragTarget != null; } }


    // Start is called before the first frame update
    void Start()
    {
        SetupPhase = true;
        bluesTurn = true;
        redsTurn = false;
        tileSelector = GameObject.Find("TileSelector");
        tileSelector.transform.localScale = new Vector3(TileLength, TileLength, 1f);
        bluePieceList = new List<Piece>();
        redPieceList = new List<Piece>();
        area = new Rect(transform.position.x - transform.localScale.x / 2f, transform.position.y - transform.localScale.y / 2f, transform.localScale.x, transform.localScale.y);
    }

    /// <summary>
    /// Adds a new Piece to the board, if a piece is already there, does nothing
    /// </summary>
    /// <param name="piecePrefab"></param>
    /// <param name="tile"></param>
    public void PlacePiece(Piece piecePrefab, Vector2 tile, string pieceColor)
    {
        if(GetPieceAt((int)tile.x, (int)tile.y) != null)
        {
            return; // Don't place a piece on top of an existing one
        }

        Piece newPiece = Instantiate(piecePrefab, TileToTransform(tile), Quaternion.identity);
        newPiece.transform.localScale = new Vector3(TileLength, TileLength, 1); // Fill the tile entirely

        if(pieceColor == "blue")
        {
            newPiece.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
            bluePieceList.Add(newPiece);
        }
        else if(pieceColor == "red")
        {
            newPiece.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            redPieceList.Add(newPiece);
        }

        newPiece.GetComponent<Tile>().grid = this;
    }

    /// <summary>
    /// converts a tile location to a transform position. 0,0 is the bottom left tile of the grid
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    public Vector3 TileToTransform(Vector2 tile)
    {
        float gridLength = transform.localScale.x; // should be same as y because square

        Vector2 bottomLeftTile = new Vector2(transform.position.x, transform.position.y); // middle of grid
        bottomLeftTile -= new Vector2(gridLength / 2f, gridLength / 2f); // shift to bottom left corner
        bottomLeftTile += new Vector2(TileLength / 2f, TileLength / 2f); // shift to center of tile
        bottomLeftTile += tile * TileLength; // move to center of correct tile

        Vector3 test = new Vector3(bottomLeftTile.x, bottomLeftTile.y, 0);
        return test;  // add z component
    }

    /// <summary>
    /// return the tile that the input point is located in. Be sure to convert mouse position to world coordinates first
    /// </summary>
    /// <param name="screenLocation"></param>
    /// <returns></returns>
    public Vector2 WorldToTile(Vector3 screenLocation)
    {
        Vector2 bottomLeft = new Vector2(transform.position.x - transform.localScale.x / 2f, transform.position.y - transform.localScale.y / 2f);
        float xDist = screenLocation.x - bottomLeft.x;
        float yDist = screenLocation.y - bottomLeft.y;
        return new Vector2(Mathf.Floor(xDist / TileLength), Mathf.Floor(yDist / TileLength));
    }

    /// <summary>
    /// Returns the piece located on the tile at the specified coords
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Piece GetPieceAt(float x, float y)
    {
        foreach(Piece piece in bluePieceList)
        {
            Tile script = piece.GetComponent<Tile>();
            if(script.GridPosition.x == x && script.GridPosition.y == y)
            {
                return piece;
            }
        }
        foreach (Piece piece in redPieceList)
        {
            Tile script = piece.GetComponent<Tile>();
            if (script.GridPosition.x == x && script.GridPosition.y == y)
            {
                return piece;
            }
        }
        // Piece is not on the board if this point is reached
        return null;
    }

    /// <summary>
    /// Determines if a tile location exists on the grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && y < dims && x < dims;
    }

    public void MovePiece(float xCoord, float yCoord, Piece pieceToMove)
    {
        if (GetPieceAt(xCoord, yCoord) != null)
        {
           // There is already a piece on this square, so capture it

        }
        pieceToMove.gameObject.transform.position = new Vector3(xCoord, yCoord);
        pieceToMove.transform.localScale = new Vector3(TileLength, TileLength, 1);
        pieceToMove.GetComponent<Tile>().grid = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMouse.z = 0f;

        if (SetupPhase)
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < bluePieceList.Count; i++)
                {
                    Piece unitScript = bluePieceList[i].GetComponent<Piece>();
                    Vector3 pos = bluePieceList[i].transform.position;
                    float scale = bluePieceList[i].transform.localScale.x;
                    Rect area = new Rect(pos.x - scale / 2f, pos.y - scale / 2f, scale, scale);
                    if (area.Contains(worldMouse) && unitScript != null)
                    {
                        dragTarget = bluePieceList[i];
                        bluePieceList.RemoveAt(i);
                    }
                }

                for (int i = 0; i < redPieceList.Count; i++)
                {
                    Piece unitScript = redPieceList[i].GetComponent<Piece>();
                    Vector3 pos = redPieceList[i].transform.position;
                    float scale = redPieceList[i].transform.localScale.x;
                    Rect area = new Rect(pos.x - scale / 2f, pos.y - scale / 2f, scale, scale);
                    if (area.Contains(worldMouse) && unitScript != null)
                    {
                        dragTarget = redPieceList[i];
                        redPieceList.RemoveAt(i);
                    }
                }
            }

            // move dragged unit to mouse and highlight available spaces
            if (dragTarget != null)
            {
                tileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
                dragTarget.transform.position = worldMouse;

                // highlight place tile
                bool canPlace = false;
                Vector2 hovered = Vector2.zero;


                if (area.Contains(worldMouse))
                {
                    Debug.Log("hovering");
                    hovered = WorldToTile(worldMouse);
                    tileSelector.transform.position = TileToTransform(hovered);

                    if (hovered.y < PlaceLimit && (hovered.Equals(dragTarget.GetComponent<Piece>().GridPosition) || GetPieceAt((int)hovered.x, (int)hovered.y) == null))
                    {
                        tileSelector.GetComponent<SpriteRenderer>().color = lightGreen;
                        canPlace = true;
                    }
                    else
                    {
                        tileSelector.GetComponent<SpriteRenderer>().color = red;
                    }
                }

                // place unit back on board when released
                if (!Input.GetMouseButton(0))
                {
                    if (canPlace)
                    {
                        Debug.Log("Unit Placeable");

                        dragTarget.transform.position = TileToTransform(hovered);
                        dragTarget.GetComponent<Piece>().MatchTileToPosition();
                    }
                    else
                    {
                        // default to tile started from
                        dragTarget.transform.position = TileToTransform(dragTarget.GetComponent<Piece>().GridPosition);
                    }

                    dragTarget = null;
                    tileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
                }
            }
        }
        // Setup phase is done, turn phase code
        else
        {
            Vector2 hoveredTile = WorldToTile(worldMouse);
            // Blues Turn
            if (bluesTurn == true)
            {
                // loop through blue pieces and check if they are being hovered over
                for(int i = 0; i < bluePieceList.Count; i++)
                {
                    if(bluePieceList[i].GridPosition == hoveredTile)
                    { 
                        // These debugs prove our piece lists aren't working properly
                        Debug.Log(bluePieceList[i].gameObject);
                        Debug.Log("Hovering!!!!!");
                        //TileSelector.GetComponent<SpriteRenderer>().color = LightGreen;
                    }
                }
            }

            // Reds Turn
            if(redsTurn == true)
            {

            }
        }
        
    }
}
