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

public class GameBoard : MonoBehaviour
{
    public int dimension = 10;
    public GameObject defaultPrefab;

    private List<GameObject> pieces;
    private Rect area;
    private GameObject dragTarget;

    public float TileLength { get { return transform.localScale.x / dimension; } }
    public Rect Area { get { return area; } }
    public List<GameObject> Pieces { get { return pieces; } }
    public bool SetupPhase;
    public int totalPieces { get { return 16; } }

    private GameObject tileSelector;
    public GameObject TileSelector { get { return tileSelector; } }
    private Color green = new Color(0, 255, 0);
    public Color Green { get { return green; } }
    private Color red = new Color(255, 0, 0);
    public Color Red { get { return red; } }

    // Start is called before the first frame update
    void Start()
    {
        SetupPhase = true;

        tileSelector = GameObject.Find("TileSelector");
        tileSelector.transform.localScale = new Vector3(TileLength, TileLength, 1f);

        pieces = new List<GameObject>();
        area = new Rect(transform.position.x - transform.localScale.x / 2f, transform.position.y - transform.localScale.y / 2f, transform.localScale.x, transform.localScale.y);
    }

    public void PlacePiece(GameObject piecePrefab, Vector2 tile)
    {
        if (GetEntityAt((int)tile.x, (int)tile.y) != null)
        {
            return; 
        }

        GameObject newPiece = Instantiate(piecePrefab, TileToTransform(tile), Quaternion.identity);
        newPiece.transform.localScale = new Vector3(TileLength, TileLength, 1);
        pieces.Add(newPiece);

        newPiece.GetComponent<GridItem>().grid = this;
    }

    public Vector3 TileToTransform(Vector2 tile)
    {
        float gridLength = transform.localScale.x;

        Vector2 bottomLeftTile = new Vector2(transform.position.x, transform.position.y);
        bottomLeftTile -= new Vector2(gridLength / 2f, gridLength / 2f);
        bottomLeftTile += new Vector2(TileLength / 2f, TileLength / 2f);
        bottomLeftTile += tile * TileLength;

        Vector3 test = new Vector3(bottomLeftTile.x, bottomLeftTile.y, 0);
        return test;
    }

    public Vector2 WorldToTile(Vector3 screenLocation)
    {
        Vector2 bottomLeft = new Vector2(transform.position.x - transform.localScale.x / 2f, transform.position.y - transform.localScale.y / 2f);
        float xDist = screenLocation.x - bottomLeft.x;
        float yDist = screenLocation.y - bottomLeft.y;
        return new Vector2(Mathf.Floor(xDist / TileLength), Mathf.Floor(yDist / TileLength));
    }

    public GameObject GetEntityAt(int x, int y)
    {
        foreach (GameObject entity in pieces)
        {
            GridItem script = entity.GetComponent<GridItem>();
            if (script.GridPosition.x == x && script.GridPosition.y == y)
            {
                return entity;
            }
        }

        return null;
    }

    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && y < dimension && x < dimension;
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
                for (int i = 0; i < pieces.Count; i++)
                {
                    Unit unitScript = pieces[i].GetComponent<Unit>();
                    Vector3 pos = pieces[i].transform.position;
                    float scale = pieces[i].transform.localScale.x;
                    Rect area = new Rect(pos.x - scale / 2f, pos.y - scale / 2f, scale, scale);
                    if (area.Contains(worldMouse) && unitScript != null && unitScript.IsAlly)
                    {
                        dragTarget = pieces[i];
                        pieces.RemoveAt(i);
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
                    hovered = WorldToTile(worldMouse);
                    tileSelector.transform.position = TileToTransform(hovered);

                    if (hovered.y < PlaceLimit && GetEntityAt((int)hovered.x, (int)hovered.y) == null)
                    {
                        tileSelector.GetComponent<SpriteRenderer>().color = green;
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
                        dragTarget.transform.position = TileToTransform(hovered);
                        dragTarget.GetComponent<Unit>().MatchTileToPosition();
                    }
                    else
                    {
                        // default to tile started from
                        dragTarget.transform.position = TileToTransform(dragTarget.GetComponent<Unit>().GridPosition);
                    }

                    pieces.Add(dragTarget);
                    dragTarget = null;
                    tileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
                }
            }
        }
        else
        { // battle phase
            // clear dead units
            for (int i = 0; i < pieces.Count; i++)
            {
                if (IsUnit(pieces[i]))
                {
                    if (pieces[i].GetComponent<Unit>().Health <= 0)
                    {
                        DestroyImmediate(pieces[i]);
                        pieces.RemoveAt(i);
                        i--;

                    }
                }
            }
        }

    }
}
