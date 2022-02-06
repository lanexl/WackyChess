using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Piece
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
    /// Returns a list of possible tiles that this Bomber Piece can move to. Bomber pieces move 1 square 
    /// in any direction. If an enemy tile is on the square, the Bomber explodes, destroying any pieces
    /// within 1 tile surrounding the attacked enemy
    /// </summary>
    /// <returns></returns>
    public override List<float> PossibleMoves()
    {
        List<float> tempFloatList = new List<float>();
        return tempFloatList;
    }
}
