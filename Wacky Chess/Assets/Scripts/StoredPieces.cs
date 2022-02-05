using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredPieces : MonoBehaviour
{
    public int iconWidth = 1;
    public GameGrid grid;
    public GameObject iconPrefab;
    public GameObject tempPrefab;

    private List<GameObject> unitIcons; // unit prefabs to be placed on the board
    private Rect[] buttons; // locations on screen that unit icons snap to
    private int selected; // index of currently selected unit to be placed

    // Start is called before the first frame update
    void Start()
    {
        selected = -1;

        // define snap points
        buttons = new Rect[10];
        for (int i = 0; i < 10; i += 2)
        {
            buttons[i] = new Rect(-7.75f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
            buttons[i + 1] = new Rect(-6.25f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
        }

        // load purchased units from shop
        unitIcons = new List<GameObject>();

        // temporary for test
        GameObject icon = Instantiate(iconPrefab);
        icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
        icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
        unitIcons.Add(icon);

        icon = Instantiate(iconPrefab);
        icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
        icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
        unitIcons.Add(icon);

        icon = Instantiate(iconPrefab);
        icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
        icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
        unitIcons.Add(icon);

        PlaceIcons();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grid.SetupPhase)
        {
            return;
        }

        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // make all borders invisible by default
        for (int i = 0; i < unitIcons.Count; i++)
        { // uses units count as limit so it will ony select if a unit is actually there
            if (i == selected)
            {
                // ignore selected icon
                continue;
            }

            SetBorderColor(i, new Color(0f, 0f, 0f, 0f));
            if (buttons[i].Contains(worldMouse))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SetBorderColor(i, grid.Green);
                    selected = i;
                }
                else
                {
                    SetBorderColor(i, grid.Green);
                }
                break;
            }
        }

        if (selected >= 0)
        {
            bool canPlace = false;

            // look for grid tile to place
            grid.TileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
            if (grid.Area.Contains(worldMouse))
            {
                Vector2 hoveredTile = grid.WorldToTile(worldMouse);

                grid.TileSelector.transform.position = grid.TileToTransform(hoveredTile); // snap selector to tile

                // determine if unit can be placed there
                if (hoveredTile.y < grid.PlaceLimit && grid.GetEntityAt((int)hoveredTile.x, (int)hoveredTile.y) == null)
                {
                    grid.TileSelector.GetComponent<SpriteRenderer>().color = grid.Green;
                    canPlace = true;
                }
                else
                {
                    grid.TileSelector.GetComponent<SpriteRenderer>().color = grid.Red;
                }
            }

            // actually place unit
            if (canPlace && Input.GetMouseButtonUp(0) && !grid.Dragging)
            { // up so it doesn't instantly pick up and also works with click and drag
                grid.PlaceEntity(unitIcons[selected].GetComponent<PieceStorage>().UnitPrefab, grid.WorldToTile(worldMouse));
                GameObject deleteThis = unitIcons[selected];
                unitIcons.RemoveAt(selected);
                Destroy(deleteThis);
                selected = -1; // nothing selected
                PlaceIcons(); // move icons
                grid.TileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
            }
        }
    }

    private void PlaceIcons()
    {
        for (int i = 0; i < unitIcons.Count; i++)
        {
            unitIcons[i].transform.position = buttons[i].center;
        }
    }

    // sets the border of an icon to the input color
    private void SetBorderColor(int index, Color newColor)
    {
        unitIcons[index].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }
}
