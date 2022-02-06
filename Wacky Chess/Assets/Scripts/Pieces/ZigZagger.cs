using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagger : Piece
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
    /// Returns a list of possible tiles that this Zig Zagger Piece can move to. Zig Zagger Pieces move in a zig zag (2 diagonal one way, 1 diagonal another way)
    /// </summary>
    /// <returns></returns>
    public override List<float> PossibleMoves()
    {
        List<float> possibleMovesCoordinates = new List<float>();
        float currentX = this.gameObject.transform.position.x;
        float currentY = this.gameObject.transform.position.y;

        /*
         * Possible Moves: 
         * Up-Left: x-1.5, y+4.5
         * Up-Right: x+1.5, y+4.5
         * Down-Left: x-1.5, y-4.5
         * Down-Right: x+1.5, y-4.5
         * Left: x-4.5
         * Right: x+4.5
        */

        // Move 1 is two Up-Left, one Up-Right
        float x1 = currentX - 1.5f;
        float y1 = currentY + 4.5f;
        possibleMovesCoordinates.Add(x1);
        possibleMovesCoordinates.Add(y1);

        // Move 2 is two Up-Left, one Down-Left
        float x2 = currentX - 4.5f;
        float y2 = currentY + 1.5f;
        possibleMovesCoordinates.Add(x2);
        possibleMovesCoordinates.Add(y2);

        // Move 3 is two Up-Right, one Up-Left
        float x3 = currentX + 1.5f;
        float y3 = currentY + 4.5f;
        possibleMovesCoordinates.Add(x3);
        possibleMovesCoordinates.Add(y3);

        // Move 4 is two Up-Right, one Down-Right
        float x4 = currentX + 4.5f;
        float y4 = currentY + 1.5f;
        possibleMovesCoordinates.Add(x4);
        possibleMovesCoordinates.Add(y4);

        // Move 5 is two Down-Right, one Up-Right
        float x5 = currentX + 4.5f;
        float y5 = currentY - 1.5f;
        possibleMovesCoordinates.Add(x5);
        possibleMovesCoordinates.Add(y5);

        // Move 6 is two Down-Right, one Down-Left
        float x6 = currentX + 1.5f;
        float y6 = currentY - 4.5f;
        possibleMovesCoordinates.Add(x6);
        possibleMovesCoordinates.Add(y6);

        // Move 7 is two Down-Left, one Up-Left
        float x7 = currentX - 4.5f;
        float y7 = currentY - 1.5f;
        possibleMovesCoordinates.Add(x7);
        possibleMovesCoordinates.Add(y7);

        // Move 8 is two Down-Left, one Down-Right
        float x8 = currentX - 1.5f;
        float y8 = currentY - 4.5f;
        possibleMovesCoordinates.Add(x8);
        possibleMovesCoordinates.Add(y8);


        return possibleMovesCoordinates;
    }
}
