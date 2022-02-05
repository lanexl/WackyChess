using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredPieces : MonoBehaviour
{
    public int iconWidth = 1;
    public GameGrid grid;
    //public GameObject iconPrefab;
    //public GameObject tempPrefab;

    // Piece Prefabs
    public ZigZagger zigZaggerPrefab;
    public ChainKiller chainKillerPrefab;
    public Cannon cannonPrefab;
    public Bomber bomberPrefab;
    public Vip vipPrefab;
    public CannonFodder cannonFodderPrefab;

    //private List<GameObject> unitIcons; // unit prefabs to be placed on the board
    private Rect[] buttons; // locations on screen that unit icons snap to
    private int selected; // index of currently selected unit to be placed

    public List<Piece> whitePieceList = new List<Piece>();
    public List<Piece> blackPieceList = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
        // Create and Instantiate White Pieces
        ZigZagger whiteZigZagger1 = Instantiate(zigZaggerPrefab);
        ZigZagger whiteZigZagger2 = Instantiate(zigZaggerPrefab);
        ChainKiller whiteChainKiller1 = Instantiate(chainKillerPrefab);
        ChainKiller whiteChainKiller2 = Instantiate(chainKillerPrefab);
        Cannon whiteCannon1 = Instantiate(cannonPrefab);
        Cannon whiteCannon2 = Instantiate(cannonPrefab);
        Bomber whiteBomber = Instantiate(bomberPrefab);
        Vip whiteVip = Instantiate(vipPrefab);
        CannonFodder whiteCannonFodder1 = Instantiate(cannonFodderPrefab);
        CannonFodder whiteCannonFodder2 = Instantiate(cannonFodderPrefab);
        CannonFodder whiteCannonFodder3 = Instantiate(cannonFodderPrefab);
        CannonFodder whiteCannonFodder4 = Instantiate(cannonFodderPrefab);
        CannonFodder whiteCannonFodder5 = Instantiate(cannonFodderPrefab);
        CannonFodder whiteCannonFodder6 = Instantiate(cannonFodderPrefab);
        CannonFodder whiteCannonFodder7 = Instantiate(cannonFodderPrefab);
        CannonFodder whiteCannonFodder8 = Instantiate(cannonFodderPrefab);

        whitePieceList.Add(whiteZigZagger1);
        whitePieceList.Add(whiteZigZagger2);
        whitePieceList.Add(whiteChainKiller1);
        whitePieceList.Add(whiteChainKiller2);
        whitePieceList.Add(whiteCannon1);
        whitePieceList.Add(whiteCannon2);
        whitePieceList.Add(whiteBomber);
        whitePieceList.Add(whiteVip);
        whitePieceList.Add(whiteCannonFodder1);
        whitePieceList.Add(whiteCannonFodder2);
        whitePieceList.Add(whiteCannonFodder3);
        whitePieceList.Add(whiteCannonFodder4);
        whitePieceList.Add(whiteCannonFodder5);
        whitePieceList.Add(whiteCannonFodder6);
        whitePieceList.Add(whiteCannonFodder7);
        whitePieceList.Add(whiteCannonFodder8);

        // Create and Instantiate Black Pieces
        ZigZagger blackZigZagger1 = Instantiate(zigZaggerPrefab);
        ZigZagger blackZigZagger2 = Instantiate(zigZaggerPrefab);
        ChainKiller blackChainKiller1 = Instantiate(chainKillerPrefab);
        ChainKiller blackChainKiller2 = Instantiate(chainKillerPrefab);
        Cannon blackCannon1 = Instantiate(cannonPrefab);
        Cannon blackCannon2 = Instantiate(cannonPrefab);
        Bomber blackBomber = Instantiate(bomberPrefab);
        Vip blackVip = Instantiate(vipPrefab);
        CannonFodder blackCannonFodder1 = Instantiate(cannonFodderPrefab);
        CannonFodder blackCannonFodder2 = Instantiate(cannonFodderPrefab);
        CannonFodder blackCannonFodder3 = Instantiate(cannonFodderPrefab);
        CannonFodder blackCannonFodder4 = Instantiate(cannonFodderPrefab);
        CannonFodder blackCannonFodder5 = Instantiate(cannonFodderPrefab);
        CannonFodder blackCannonFodder6 = Instantiate(cannonFodderPrefab);
        CannonFodder blackCannonFodder7 = Instantiate(cannonFodderPrefab);
        CannonFodder blackCannonFodder8 = Instantiate(cannonFodderPrefab);

        blackPieceList.Add(whiteZigZagger1);
        blackPieceList.Add(whiteZigZagger2);
        blackPieceList.Add(whiteChainKiller1);
        blackPieceList.Add(whiteChainKiller2);
        blackPieceList.Add(whiteCannon1);
        blackPieceList.Add(whiteCannon2);
        blackPieceList.Add(whiteBomber);
        blackPieceList.Add(whiteVip);
        blackPieceList.Add(whiteCannonFodder1);
        blackPieceList.Add(whiteCannonFodder2);
        blackPieceList.Add(whiteCannonFodder3);
        blackPieceList.Add(whiteCannonFodder4);
        blackPieceList.Add(whiteCannonFodder5);
        blackPieceList.Add(whiteCannonFodder6);
        blackPieceList.Add(whiteCannonFodder7);
        blackPieceList.Add(whiteCannonFodder8);

        selected = -1;

        // define snap points
        buttons = new Rect[grid.dims];
        for (int i = 0; i < grid.dims; i += 2)
        {
            buttons[i] = new Rect(-7.75f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
            buttons[i + 1] = new Rect(-6.25f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
        }
        

        // load purchased units from shop
        //unitIcons = new List<GameObject>();

        // temporary for test
       // Debug.Log("Object Instantiating");
       // GameObject icon = Instantiate(iconPrefab);
       // icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
       // icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
       // unitIcons.Add(icon);
       // 
       // icon = Instantiate(iconPrefab);
       // icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
       // icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
       // unitIcons.Add(icon);
       // 
       // icon = Instantiate(iconPrefab);
       // icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
       // icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
       // unitIcons.Add(icon);

        //PlaceIcons();
        PlacePieces();
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
        for (int i = 0; i < whitePieceList.Count+1; i++)
        { // uses units count as limit so it will ony select if a unit is actually there
            if (i == selected)
            {
                Debug.Log("i is selected");
                // ignore selected icon
                continue;
            }

            //SetBorderColor(i, new Color(0f, 0f, 0f, 0f));
            //Debug.Log("Border set to clear");

            if (buttons[i].Contains(worldMouse))
            {
                Debug.Log("Mouse In Button");
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Setting border to green");
                    SetBorderColor(i, grid.LightGreen);
                    selected = i;
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
                Debug.Log("checking for tiles");
                Vector2 hoveredTile = grid.WorldToTile(worldMouse);

                grid.TileSelector.transform.position = grid.TileToTransform(hoveredTile); // snap selector to tile

                // determine if unit can be placed there
                if (hoveredTile.y < grid.PlaceLimit && grid.GetEntityAt((int)hoveredTile.x, (int)hoveredTile.y) == null)
                {
                    grid.TileSelector.GetComponent<SpriteRenderer>().color = grid.LightGreen;
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
                grid.PlaceEntity(whitePieceList[selected].GetComponent<PieceStorage>().UnitPrefab, grid.WorldToTile(worldMouse));
                //GameObject deleteThis = whitePieceList[selected];
                Destroy(whitePieceList[selected]);
                whitePieceList.RemoveAt(selected);
                //Destroy(deleteThis);
                selected = -1; // nothing selected
                //PlaceIcons(); // move icons
                PlacePieces();
                grid.TileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
            }
        }
    }

    /*private void PlaceIcons()
    {
        for (int i = 0; i < unitIcons.Count; i++)
        {
            unitIcons[i].transform.position = buttons[i].center;
        }
    }*/

    private void PlacePieces()
    {
        for (int i = 0; i < whitePieceList.Count; i++)
        {
            whitePieceList[i].transform.position = buttons[i].center;
        }
    }

    // sets the border of an icon to the input color
    private void SetBorderColor(int index, Color newColor)
   {
       whitePieceList[index].transform.gameObject.GetComponent<SpriteRenderer>().color = newColor;
   }

    /// <summary>
    /// Method to remove a piece that has been captured from this players piece list. Piece will be destroyed and then removed from the list
    /// </summary>
    public void removePiece(Piece pieceToRemove)
    {
        // White Pieces
        for (int i = 0; i < whitePieceList.Count; i++)
        {
            if (whitePieceList.IndexOf(pieceToRemove) != -1)
            {
                int indexToRemove = whitePieceList.IndexOf(pieceToRemove);
                GameObject.Destroy(whitePieceList[indexToRemove]);
                whitePieceList.RemoveAt(indexToRemove);
                return;
            }
        }

        // Black Pieces
        for (int i = 0; i < whitePieceList.Count; i++)
        {
            if (blackPieceList.IndexOf(pieceToRemove) != -1)
            {
                int indexToRemove = blackPieceList.IndexOf(pieceToRemove);
                GameObject.Destroy(blackPieceList[indexToRemove]);
                blackPieceList.RemoveAt(indexToRemove);
                return;
            }
        }
        // Throw a message to the console if piece was not found
        Debug.Log("Piece was not found and can't be removed");
    }
}
