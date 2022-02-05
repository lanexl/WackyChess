using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variable to determine which player this is
    // Player 1 = white
    // Player 2 = black
    public int playerID;
    public List<Piece> pieceList = new List<Piece>();
    public ZigZagger zigZagger1;
    public ZigZagger zigZagger2;
    public ChainKiller chainKiller1;
    public ChainKiller chainKiller2;
    public Cannon cannon1;
    public Cannon cannon2;
    public SuicideBomber suicideBomber;
    public Vip vip;
    public CannonFodder cannonFodder1;
    public CannonFodder cannonFodder2;
    public CannonFodder cannonFodder3;
    public CannonFodder cannonFodder4;
    public CannonFodder cannonFodder5;
    public CannonFodder cannonFodder6;
    public CannonFodder cannonFodder7;
    public CannonFodder cannonFodder8;

    // Start is called before the first frame update
    void Start()
    {
        pieceList.Add(zigZagger1);
        pieceList.Add(zigZagger2);
        pieceList.Add(chainKiller1);
        pieceList.Add(chainKiller2);
        pieceList.Add(cannon1);
        pieceList.Add(cannon2);
        pieceList.Add(suicideBomber);
        pieceList.Add(vip);
        pieceList.Add(cannonFodder1);
        pieceList.Add(cannonFodder2);
        pieceList.Add(cannonFodder3);
        pieceList.Add(cannonFodder4);
        pieceList.Add(cannonFodder5);
        pieceList.Add(cannonFodder6);
        pieceList.Add(cannonFodder7);
        pieceList.Add(cannonFodder8);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Method to remove a piece that has been captured from this players piece list. Piece will be destroyed and then removed from the list
    /// </summary>
    public void removePiece(Piece pieceToRemove)
    {
        for (int i = 0; i < pieceList.Count; i++)
        {
            if(pieceList.IndexOf(pieceToRemove) != -1)
            {
                int indexToRemove = pieceList.IndexOf(pieceToRemove);
                GameObject.Destroy(pieceList[indexToRemove]);
                pieceList.RemoveAt(indexToRemove);
                return;
            }
        }
        // Throw a message to the console if piece was not found
        Debug.Log("Piece was not found and can't be removed");
    }
}
