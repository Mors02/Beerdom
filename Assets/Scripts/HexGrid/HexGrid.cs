using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    [SerializeField]
    [Range(1, 20)]
    private int _width = 6;

    [SerializeField]
    [Range(1, 20)]
    private int _height = 6;

    [SerializeField]
    private HexCell _cellPrefab;

    private HexCell[] _cells;

    private Canvas _coordinatesCanvas;

    private HexMesh _mesh;

    [SerializeField]
    private TMP_Text _coordinatesPrefab;


    void Awake()
    {
        _coordinatesCanvas = GetComponentInChildren<Canvas>();
        _mesh = GetComponentInChildren<HexMesh>();

        _cells = new HexCell[_height * _width];
        //Create the grid of cells
        for (int width = 0, counter = 0; width < _width; width++)
        {
            for (int height = 0; height < _height; height++)
            {
                CreateCell(height, width, counter++);
            }
        }
    }

    /// <summary>
    /// Create a single cell
    /// </summary>
    /// <param name="height">the position of the cell in the grid on the x axis</param>
    /// <param name="width">the position of the cell in the grid on the z axis</param>
    /// <param name="counter">the id of this cell</param>
    private void CreateCell(int height, int width, int counter)
    {
        //create the position of the center based on the radius and position in the grid
        Vector3 position;
        position.x = (height + width * 0.5f - width / 2) * HexMetrics.InnerRadius * 2f;
        position.y = 0f;
        position.z = width * HexMetrics.OuterRadius * 1.5f;

        //instantiate the cell with the position relative to the parent
        HexCell cell = _cells[counter] = Instantiate<HexCell>(_cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(height, width);

        DebugCoordinates(cell, width, height);
    }


    private void DebugCoordinates(HexCell cell, int width, int height)
    {
        TMP_Text label = Instantiate<TMP_Text>(_coordinatesPrefab);

        label.rectTransform.SetParent(this._coordinatesCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(cell.transform.localPosition .x, cell.transform.localPosition.z);
        label.text = cell.Coordinates.ToStringMultilines();
    }

    private void Start()
    {
        _mesh.Triangulate(_cells);
    }

    private void Update()
    {   
        //on click do something
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    /// <summary>
    /// Handle the click input on the mesh
    /// </summary>
    private void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(inputRay, out RaycastHit hit))
        {
            TouchCell(hit.point);
        }
    }

    /// <summary>
    /// Retrieves the clicked cell from the click position and runs a function
    /// </summary>
    /// <param name="position">position of the click</param>
    private void TouchCell(Vector3 position)
    {   
        //transform global position to local to retrieve the corresponding cell
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        Debug.Log("touched at " + coordinates.ToString());


    }
}
