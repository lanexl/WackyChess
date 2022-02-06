using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredPieces : MonoBehaviour
{
    public int iconWidth = 1;
    public GameGrid grid;
    bool blueTurnToPlace;
    bool redTurnToPlace;

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

    public List<Piece> bluePieceList = new List<Piece>();
    public List<Piece> redPieceList = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
        // Blue places before red at the start of the game
        blueTurnToPlace = true;
        redTurnToPlace = false;

        // Create and Instantiate blue Pieces
        ZigZagger blueZigZagger1 = Instantiate(zigZaggerPrefab);
        ZigZagger blueZigZagger2 = Instantiate(zigZaggerPrefab);
        ChainKiller blueChainKiller1 = Instantiate(chainKillerPrefab);
        ChainKiller blueChainKiller2 = Instantiate(chainKillerPrefab);
        Cannon blueCannon1 = Instantiate(cannonPrefab);
        Cannon blueCannon2 = Instantiate(cannonPrefab);
        Bomber blueBomber = Instantiate(bomberPrefab);
        Vip blueVip = Instantiate(vipPrefab);
        //CannonFodder blueCannonFodder1 = Instantiate(cannonFodderPrefab);
        //CannonFodder blueCannonFodder2 = Instantiate(cannonFodderPrefab);
        //CannonFodder blueCannonFodder3 = Instantiate(cannonFodderPrefab);
        //CannonFodder blueCannonFodder4 = Instantiate(cannonFodderPrefab);
        //CannonFodder blueCannonFodder5 = Instantiate(cannonFodderPrefab);
        //CannonFodder blueCannonFodder6 = Instantiate(cannonFodderPrefab);
        //CannonFodder blueCannonFodder7 = Instantiate(cannonFodderPrefab);
        //CannonFodder blueCannonFodder8 = Instantiate(cannonFodderPrefab);

        bluePieceList.Add(blueZigZagger1);
        bluePieceList.Add(blueZigZagger2);
        bluePieceList.Add(blueChainKiller1);
        bluePieceList.Add(blueChainKiller2);
        bluePieceList.Add(blueCannon1);
        bluePieceList.Add(blueCannon2);
        bluePieceList.Add(blueBomber);
        bluePieceList.Add(blueVip);
        //bluePieceList.Add(blueCannonFodder1);
        //bluePieceList.Add(blueCannonFodder2);
        //bluePieceList.Add(blueCannonFodder3);
        //bluePieceList.Add(blueCannonFodder4);
        //bluePieceList.Add(blueCannonFodder5);
        //bluePieceList.Add(blueCannonFodder6);
        //bluePieceList.Add(blueCannonFodder7);
        //bluePieceList.Add(blueCannonFodder8);

        // Create and Instantiate red Pieces
        //ZigZagger redZigZagger1 = Instantiate(zigZaggerPrefab);
        //ZigZagger redZigZagger2 = Instantiate(zigZaggerPrefab);
        //ChainKiller redChainKiller1 = Instantiate(chainKillerPrefab);
        //ChainKiller redChainKiller2 = Instantiate(chainKillerPrefab);
        //Cannon redCannon1 = Instantiate(cannonPrefab);
        //Cannon redCannon2 = Instantiate(cannonPrefab);
        //Bomber redBomber = Instantiate(bomberPrefab);
        //Vip redVip = Instantiate(vipPrefab);
        //CannonFodder redCannonFodder1 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder2 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder3 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder4 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder5 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder6 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder7 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder8 = Instantiate(cannonFodderPrefab);

        //redPieceList.Add(blueZigZagger1);
        //redPieceList.Add(blueZigZagger2);
        //redPieceList.Add(blueChainKiller1);
        //redPieceList.Add(blueChainKiller2);
        //redPieceList.Add(blueCannon1);
        //redPieceList.Add(blueCannon2);
        //redPieceList.Add(blueBomber);
        //redPieceList.Add(blueVip);
        //redPieceList.Add(blueCannonFodder1);
        //redPieceList.Add(blueCannonFodder2);
        //redPieceList.Add(blueCannonFodder3);
        //redPieceList.Add(blueCannonFodder4);
        //redPieceList.Add(blueCannonFodder5);
        //redPieceList.Add(blueCannonFodder6);
        //redPieceList.Add(blueCannonFodder7);
        //redPieceList.Add(blueCannonFodder8);

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
        //Debug.Log("Object Instantiating");
        //GameObject icon = Instantiate(iconPrefab);
        //player1.pieceList[0] = Instantiate(zigZaggerPrefab);
        //icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
        //icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
        //unitIcons.Add(icon);
        //
        //icon = Instantiate(iconPrefab);
        //icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
        //icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
        //unitIcons.Add(icon);
        //
        //icon = Instantiate(iconPrefab);
        //icon.transform.localScale = new Vector3(iconWidth, iconWidth, 0);
        //icon.GetComponent<PieceStorage>().UnitPrefab = tempPrefab;
        //unitIcons.Add(icon);

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
        for (int i = 0; i < bluePieceList.Count+1; i++)
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
                if (hoveredTile.y < grid.PlaceLimit && grid.GetPieceAt((int)hoveredTile.x, (int)hoveredTile.y) == null)
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
            {
                if(blueTurnToPlace == true)
                {
                    grid.PlacePiece(bluePieceList[selected], grid.WorldToTile(worldMouse), "blue");
                }
                else if(redTurnToPlace == true)
                {
                    grid.PlacePiece(redPieceList[selected], grid.WorldToTile(worldMouse), "red");
                }

                //GameObject deleteThis = bluePieceList[selected];
                //Destroy(bluePieceList[selected]);
                //bluePieceList.RemoveAt(selected);
                removePiece(bluePieceList[selected]);
                //Destroy(deleteThis);
                selected = -1; // nothing selected
                //PlaceIcons(); // move icons
                PlacePieces();
                grid.TileSelector.transform.position = new Vector3(-9999f, -9999f, 0f); // hidden offscreen
            }
        }
    }

    private void PlacePieces()
    {
        for (int i = 0; i < bluePieceList.Count; i++)
        {
            bluePieceList[i].transform.position = buttons[i].center;
        }
    }

    // sets the border of an icon to the input color
    private void SetBorderColor(int index, Color newColor)
    {
       bluePieceList[index].transform.gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    /// <summary>
    /// Method to remove a piece that has been captured from this players piece list. Piece will be destroyed and then removed from the list
    /// </summary>
    public void removePiece(Piece pieceToRemove)
    {
        // blue Pieces
        for (int i = 0; i < bluePieceList.Count; i++)
        {
            if (bluePieceList.IndexOf(pieceToRemove) != -1)
            {
                int indexToRemove = bluePieceList.IndexOf(pieceToRemove);
                GameObject.Destroy(bluePieceList[indexToRemove]);
                bluePieceList.RemoveAt(indexToRemove);
                return;
            }
        }

        // red Pieces
        for (int i = 0; i < bluePieceList.Count; i++)
        {
            if (redPieceList.IndexOf(pieceToRemove) != -1)
            {
                int indexToRemove = redPieceList.IndexOf(pieceToRemove);
                GameObject.Destroy(redPieceList[indexToRemove]);
                redPieceList.RemoveAt(indexToRemove);
                return;
            }
        }
        // Throw a message to the console if piece was not found
        Debug.Log("Piece was not found and can't be removed");
    }
}
