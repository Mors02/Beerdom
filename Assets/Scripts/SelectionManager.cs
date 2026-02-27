using UnityEngine;
using System.Collections.Generic;
public class SelectionManager
{
    private static SelectionManager _instance;
    public static SelectionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SelectionManager();
                return _instance;
        }   
        private set
        {
            _instance = value;
        }
    }    
    
    public HashSet<SelectableUnit> SelectedUnits = new HashSet<SelectableUnit>();
    public List<SelectableUnit> AvailableUnits = new List<SelectableUnit>();
    private SelectionManager()
    {
        
    }

    /// <summary>
    /// Select a new unit and adds it to the selected hashset
    /// </summary>
    /// <param name="unit"></param>
    public void Select(SelectableUnit unit)
    {
        SelectedUnits.Add(unit);
        unit.OnSelected();
    }

    /// <summary>
    /// Deselect a new unit and removes it to the selected hashset
    /// </summary>
    /// <param name="unit"></param>
    public void Deselect(SelectableUnit unit)
    {
        unit.OnDeselected();
        SelectedUnits.Remove(unit);
    }

    public void DeselectAll()
    {
        
        foreach (SelectableUnit unit in SelectedUnits)
        {
            unit.OnDeselected();
        }
        SelectedUnits.Clear();
    }

    /// <summary>
    /// Checks whether a unit is selected or not
    /// </summary>
    /// <param name="unit">unit to check</param>
    /// <returns>true if it's selected, false otherwise</returns>
    public bool IsSelected(SelectableUnit unit)
    {
       return SelectedUnits.Contains(unit);
    }
}
