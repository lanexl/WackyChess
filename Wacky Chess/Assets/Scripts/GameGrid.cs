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
    public GameObject defaultPrefab; // set in editor
    public GameObject wallPrefab; // set in editor

    private List<GameObject> entities; // units and obstacles
    private Rect area;
    private GameObject dragTarget; // unit being clicked and dragged

    public float TileLength { get { return transform.localScale.x / dims; } } // assumes grid is square and width = scale
    public Rect Area { get { return area; } }
    public List<GameObject> Entities { get { return entities; } }
    public bool SetupPhase; // true: setup phase, false: battle phase
    public int PlaceLimit { get { return 4; } }

    private GameObject tileSelector;
    public GameObject TileSelector { get { return tileSelector; } }
    private Color lightBlue = new Color(40f / 255f, 1f, 229f / 255f);
    public Color LightBlue { get { return lightBlue; } }
    private Color lightGreen = new Color(134f / 255f, 1f, 40f / 255f);
    public Color LightGreen { get { return lightGreen; } }
    private Color red = new Color(186f / 255f, 27f / 255f, 27f / 255f);
    public Color Red { get { return red; } }
    public bool Dragging { get { return dragTarget != null; } }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start begin");
        SetupPhase = true;

        tileSelector = GameObject.Find("TileSelector");
        tileSelector.transform.localScale = new Vector3(TileLength, TileLength, 1f);

        entities = new List<GameObject>();
        area = new Rect(transform.position.x - transform.localScale.x / 2f, transform.position.y - transform.localScale.y / 2f, transform.localScale.x, transform.localScale.y);
        Debug.Log("Start end");

    }

    // adds a new entity to the board, if a unit is already there, does nothing
    public void PlaceEntity(GameObject entityPrefab, Vector2 tile)
    {
        if (GetEntityAt((int)tile.x, (int)tile.y) != null)
        {
            return; // don't place a unit on top of an existing one
        }

        GameObject newEntity = Instantiate(entityPrefab, TileToTransform(tile), Quaternion.identity);
        newEntity.transform.localScale = new Vector3(TileLength, TileLength, 1); // fill tile entirely
        entities.Add(newEntity);

        newEntity.GetComponent<Tile>().grid = this;
        // object sets its own grid location
    }

    // converts a tile location to a transform position. 0,0 is the bottom left tile of the grid
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

    // return the tile that the input point is located in. Be sure to convert mouse position to world coordinates first
    public Vector2 WorldToTile(Vector3 screenLocation)
    {
        Vector2 bottomLeft = new Vector2(transform.position.x - transform.localScale.x / 2f, transform.position.y - transform.localScale.y / 2f);
        float xDist = screenLocation.x - bottomLeft.x;
        float yDist = screenLocation.y - bottomLeft.y;
        return new Vector2(Mathf.Floor(xDist / TileLength), Mathf.Floor(yDist / TileLength));
    }

    // returns the unit located at the input tile. Assumes one unit per tile
    public GameObject GetEntityAt(int x, int y)
    {
        foreach (GameObject entity in entities)
        {
            Tile script = entity.GetComponent<Tile>();
            if (script.GridPosition.x == x && script.GridPosition.y == y)
            {
                return entity;
            }
        }

        return null;
    }

    // each grid object is either a unit or an obstacle
    public bool IsUnit(GameObject entity)
    {
        return entity.GetComponent<Piece>() != null;
    }

    // determines if a tile location exists on the grid
    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && y < dims && x < dims;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMouse.z = 0f;

        if (SetupPhase)
        {
            // check if picking up unit
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    Piece unitScript = entities[i].GetComponent<Piece>();
                    Vector3 pos = entities[i].transform.position;
                    float scale = entities[i].transform.localScale.x;
                    Rect area = new Rect(pos.x - scale / 2f, pos.y - scale / 2f, scale, scale);
                    if (area.Contains(worldMouse) && unitScript != null)
                    {
                        dragTarget = entities[i];
                        entities.RemoveAt(i);
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

                    if (hovered.y < PlaceLimit && (hovered.Equals(dragTarget.GetComponent<Piece>().GridPosition) || GetEntityAt((int)hovered.x, (int)hovered.y) == null))
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

                    //entities.Add(dragTarget);
                    dragTarget = null;
                    tileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
                }
            }
        }
        else
        { // battle phase
            // clear dead units
            for (int i = 0; i < entities.Count; i++)
            {
                if (IsUnit(entities[i]))
                {
                    //if (entities[i].GetComponent<Piece>().Health <= 0)
                    //{
                    //    DestroyImmediate(entities[i]);
                    //    entities.RemoveAt(i);
                    //    i--;
                    //
                    //}
                }
            }
        }
    }
}
