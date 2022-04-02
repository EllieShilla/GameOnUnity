using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCharacter
{
    public Position[] GetPositions()
    {
        Position[] positions = new Position[4]
        {
            new Position(-6.079739f, 0.07999992f, -3.645579f, 89.061f),
            new Position(-6.132f, 0.07999995f, -2.779f,107.826f),
            new Position(-6.175f, 0.08000001f, -2.073f, 107.946f),
            new Position(-6.215f, 08000004f, -1.423f, 107.946f)
        };

        return positions;
    }
}


public class Position
{
    public float X;
    public float Y;
    public float Z;
    public float rotation_Y;

    public Position(float x, float y, float z, float rotationY)
    {
        X = x;
        Y = y;
        Z = z;
        rotation_Y = rotationY;
    }

}



