using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredPieces : MonoBehaviour
{
    public int iconWidth = 1;
    public GameGrid grid;
    bool blueTurnToPlace;
    bool redTurnToPlace;
    int bluePiecesPlaced;
    int redPiecesPlaced;

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
    private Rect[] blueButtons; // locations on screen that unit icons snap to
    private Rect[] redButtons; 
    private int selected; // index of currently selected unit to be placed

    public List<Piece> bluePieceList = new List<Piece>();
    public List<Piece> redPieceList = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
        // Blue places before red at the start of the game
        blueTurnToPlace = true;
        redTurnToPlace = false;
        bluePiecesPlaced = 0;
        redPiecesPlaced = 0;

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
        ZigZagger redZigZagger1 = Instantiate(zigZaggerPrefab);
        ZigZagger redZigZagger2 = Instantiate(zigZaggerPrefab);
        ChainKiller redChainKiller1 = Instantiate(chainKillerPrefab);
        ChainKiller redChainKiller2 = Instantiate(chainKillerPrefab);
        Cannon redCannon1 = Instantiate(cannonPrefab);
        Cannon redCannon2 = Instantiate(cannonPrefab);
        Bomber redBomber = Instantiate(bomberPrefab);
        Vip redVip = Instantiate(vipPrefab);
        //CannonFodder redCannonFodder1 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder2 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder3 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder4 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder5 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder6 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder7 = Instantiate(cannonFodderPrefab);
        //CannonFodder redCannonFodder8 = Instantiate(cannonFodderPrefab);

        redPieceList.Add(redZigZagger1);
        redPieceList.Add(redZigZagger2);
        redPieceList.Add(redChainKiller1);
        redPieceList.Add(redChainKiller2);
        redPieceList.Add(redCannon1);
        redPieceList.Add(redCannon2);
        redPieceList.Add(redBomber);
        redPieceList.Add(redVip);
       //redPieceList.Add(redCannonFodder1);
       //redPieceList.Add(redCannonFodder2);
       //redPieceList.Add(redCannonFodder3);
       //redPieceList.Add(redCannonFodder4);
       //redPieceList.Add(redCannonFodder5);
       //redPieceList.Add(redCannonFodder6);
       //redPieceList.Add(redCannonFodder7);
       //redPieceList.Add(redCannonFodder8);

        selected = -1;

        // define snap points
        blueButtons = new Rect[grid.dims];
        redButtons = new Rect[grid.dims];
        for (int i = 0; i < grid.dims; i += 2)
        {
            blueButtons[i] = new Rect(-7.75f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
            blueButtons[i + 1] = new Rect(-6.25f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
        }

        for (int i = 0; i < grid.dims; i += 2)
        {
            redButtons[i] = new Rect(7.75f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
            redButtons[i + 1] = new Rect(6.25f - iconWidth / 2f, 3.5f - iconWidth / 2f - i * 0.75f, iconWidth, iconWidth);
        }

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
                // ignore selected icon
                continue;
            }

            //SetBorderColor(i, new Color(0f, 0f, 0f, 0f));
            //Debug.Log("Border set to clear");

            if (blueButtons[i].Contains(worldMouse))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //SetBorderColor(i, grid.LightGreen);
                    selected = i;
                }
                break;
            }

            if (redButtons[i].Contains(worldMouse))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //SetBorderColor(i, grid.LightGreen);
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
                /*if (hoveredTile.y < grid.PlaceLimit && grid.GetPieceAt((int)hoveredTile.x, (int)hoveredTile.y) == null)
                {
                    grid.TileSelector.GetComponent<SpriteRenderer>().color = grid.LightGreen;
                    canPlace = true;
                }
                else
                {
                    grid.TileSelector.GetComponent<SpriteRenderer>().color = grid.Red;
                }*/

                if((blueTurnToPlace == true && hoveredTile.y < 4) || (redTurnToPlace == true && hoveredTile.y > 5))
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
                    grid.PlacePiece(bluePieceList[selected].GetComponent<Piece>(), grid.WorldToTile(worldMouse), "blue");
                    removeButton(blueButtons, selected);
                    bluePiecesPlaced++;
                    if(bluePiecesPlaced == 8)
                    {
                        blueTurnToPlace = false;
                        redTurnToPlace = true;
                        Debug.Log("It is now reds turn to place");
                    }
                }
                else if(redTurnToPlace == true)
                {
                    grid.PlacePiece(redPieceList[selected], grid.WorldToTile(worldMouse), "red");
                    removeButton(redButtons, selected);
                    redPiecesPlaced++;
                    if(redPiecesPlaced == 8)
                    {
                        redTurnToPlace = false;
                        grid.SetupPhase = false;
                        Debug.Log("Setup phase complete");
                    }
                }

                //GameObject deleteThis = bluePieceList[selected];
                //Destroy(bluePieceList[selected]);
                //bluePieceList.RemoveAt(selected);
                
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
            bluePieceList[i].transform.position = blueButtons[i].center;
        }

        for (int i = 0; i < redPieceList.Count; i++)
        {
            redPieceList[i].transform.position = redButtons[i].center;
        }
    }

    // sets the border of an icon to the input color
    //private void SetBorderColor(int index, Color newColor)
    //{
    //   bluePieceList[index].transform.gameObject.GetComponent<SpriteRenderer>().color = newColor;
    //}

    /// <summary>
    /// Method to remove a piece that has been captured from this player's piece list. Piece will be destroyed and then removed from the list
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

    public void removeButton(Rect[] buttonList, int index)
    {
        if (buttonList.Length != -1)
        {
            Vector2 blank = new Vector2(1, 1);
            buttonList[index] = new Rect(iconWidth*100, iconWidth * 100, iconWidth * 100, iconWidth * 100);
        }
    }
}
