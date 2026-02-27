using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private RectTransform _selectionBox;

    [SerializeField]
    private LayerMask _unitLayers;
    [SerializeField]
    private LayerMask _flooLayers;

    [SerializeField]
    private float _dragDelay = 0.1f;

    private float _mouseDownTime;

    private Vector2 _startMousePosition;


    public void Update()
    {
        HandleSelectionInput();
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        //if there are selected units and i click on right button
        if (Input.GetKeyUp(KeyCode.Mouse1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            //raycast, if its on the floor
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _flooLayers))
            {
                //move all to position
                foreach(SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.MoveTo(hit.point);
                }
            } 
        }
    }

    /// <summary>
    /// Handles click down, clicking, click up for the selection box
    /// </summary>
    private void HandleSelectionInput()
    {
        //if the player clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //reset the selectionbox
            _selectionBox.sizeDelta = Vector2.zero;
            _selectionBox.gameObject.SetActive(true);
            //reset the mouseposition
            _startMousePosition = Input.mousePosition;
            //save the moment it clicks
            _mouseDownTime = Time.time;
        }
        //check if the input is still clicking and it has been clicked enough to consider a drag
        else if (Input.GetKey(KeyCode.Mouse0) && _mouseDownTime + _dragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        //the mouse button is being unclicked
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _selectionBox.sizeDelta = Vector2.zero;
            _selectionBox.gameObject.SetActive(false);

            //if i'm here i check if I clicked without dragging
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _unitLayers) && hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
            {   
                //check if shift is down then add it to the list
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    //add to the list 
                    if (SelectionManager.Instance.IsSelected(unit))
                    {
                        SelectionManager.Instance.Deselect(unit);
                    } else
                    //or remove only it and maintain the list
                    {
                        SelectionManager.Instance.Select(unit);
                    }
                } else
                {
                    //else select only this
                    SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(unit);
                }
                //instead if we just clicked on nothing then deselect all (only if we are not dragging)
            } else if (_mouseDownTime + _dragDelay > Time.time)
            {
                SelectionManager.Instance.DeselectAll();
            }
            _mouseDownTime = 0;
        }
    }

    /// <summary>
    /// Calculates and resizes the selection box
    /// </summary>
    private void ResizeSelectionBox()
    {
        //calculate the size of the box
        float width = Input.mousePosition.x - _startMousePosition.x;
        float height = Input.mousePosition.y - _startMousePosition.y;

        //keep the anchored position on mouse
        _selectionBox.anchoredPosition = _startMousePosition + new Vector2(width / 2, height / 2);
        //resize
        _selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        //create the bounds to compare the unit on screen
        Bounds bounds = new Bounds(_selectionBox.anchoredPosition, _selectionBox.sizeDelta);
        //cycle the units
        for (int i = 0; i < SelectionManager.Instance.AvailableUnits.Count; i++)
        {
            //check if it's inside
            if (UnitIsInSelectionBox(_camera.WorldToScreenPoint(SelectionManager.Instance.AvailableUnits[i].transform.position), bounds))
            {
                //if it is select
                SelectionManager.Instance.Select(SelectionManager.Instance.AvailableUnits[i]);
            } else
            {
                //else deselect
                SelectionManager.Instance.Deselect(SelectionManager.Instance.AvailableUnits[i]);
            }
        }
    }

    /// <summary>
    /// Checks if the unit is inside the selection box
    /// </summary>
    /// <param name="position">WorldToScreenPoint of the unit</param>
    /// <param name="bounds">Bounds to be compared</param>
    /// <returns>true if it's inside, false otherwise</returns>
    private bool UnitIsInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x && position.y > bounds.min.y && position.y < bounds.max.y;
    }
}
