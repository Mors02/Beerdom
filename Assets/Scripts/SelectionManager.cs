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
    }

    /// <summary>
    /// Deselect a new unit and removes it to the selected hashset
    /// </summary>
    /// <param name="unit"></param>
    public void Deselect(SelectableUnit unit)
    {
        SelectedUnits.Remove(unit);
    }

    public void DeselectAll()
    {
        SelectedUnits.Clear();
    }
}
