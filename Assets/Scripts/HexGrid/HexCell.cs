using UnityEngine;

public class HexCell : MonoBehaviour
{
    
    [SerializeField]
    private HexCoordinates _coordinates;


    public HexCoordinates Coordinates { get; set; }
}
