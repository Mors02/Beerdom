using UnityEngine;

/// <summary>
/// Used to handle the coordinates from a set of square coordinates to a set of hexagon coordinates
/// </summary>
[System.Serializable]
public class HexCoordinates
{
    [SerializeField]
    private int _x, _z;
    public int X { get { return _x; } }
    public int Z { get { return _z; } }

    public int Y { get { return -X - Z; }}


    public HexCoordinates(int x, int z)
    {
        _x = x;
        _z = z;
    }

    /// <summary>
    /// Returns a new set of coordinates relative to the hex grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {   //shifts the coordinates on the x axis to be linear on the diagonal
        return new HexCoordinates(x - z/2, z);
    }

    public override string ToString()
    {
        return "(" + this.X + "; " + this.Y + "; " + this.Z + ")";
    }
    /// <summary>
    /// Prints coordinates on multiple lines
    /// </summary>
    /// <returns></returns>
    public string ToStringMultilines()
    {
        return this.X + "\n" + this.Y + "\n" + this.Z;
    }

    /// <summary>
    /// Returns the cell from the world position
    /// </summary>
    /// <param name="position">world position</param>
    /// <returns>hex coordinates of the corresponding cell</returns>
    public static HexCoordinates FromPosition(Vector3 position)
    {
        //divide the position from the length of the hexagon to retrieve the offset
        float x = position.x / (HexMetrics.InnerRadius / 2);
        float y = -x;

        //since the grid is shifted we need to correct the coordinates every 2 rows
        float offset = position.z / (HexMetrics.OuterRadius * 3f);
        x -= offset;
        y -= offset;

        //we round to get the integer coordinate number
        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        //retrieve Z based on the two other coordinates
        int iZ = Mathf.RoundToInt(-x -y);
        if (iX + iY + iZ != 0)
        {
            Debug.LogWarning("Rounding error!");
        }
        return new HexCoordinates(iX, iZ);
    }
}
