using UnityEngine;


/// <summary>
/// Hexagons are defined by a circle of triangles.
/// OuterRadius is the length of the side of the triangle.
/// InnerRadius is the height of the triangle.
/// </summary>
public static class HexMetrics
{
    /// <summary>
    /// distance from the center to the corner
    /// </summary>
    public const float OuterRadius = 10f;
    /// <summary>
    /// distance from the center to the side
    /// </summary>
    public const float InnerRadius = OuterRadius * 0.866025404f;

    /// <summary>
    /// positions of the 6 points, starting from the top.
    /// </summary>
    public static Vector3[] corners = {
        new Vector3(0f, 0f, OuterRadius),
        new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(0f, 0f, -OuterRadius),
        new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(0f, 0f, OuterRadius)
    };


}
