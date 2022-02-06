using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFodder : Piece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns a list of possible tiles that this CannonFodder Piece can move to. CannonFodder Pieces move and attack directly forward 1 square.
    /// </summary>
    /// <returns></returns>
    public override List<float> PossibleMoves()
    {
        List<float> tempFloatList = new List<float>();
        return tempFloatList;
    }
}
