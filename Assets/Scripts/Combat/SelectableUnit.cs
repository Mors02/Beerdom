
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Modules.Rendering.Outline;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private SpriteRenderer _selectionSprite;

    [SerializeField]
    private OutlineComponent _outlineComponent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        SelectionManager.Instance.AvailableUnits.Add(this);
        _agent = GetComponent<NavMeshAgent>();

        _agent.enabled = false;
        
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            
            transform.position = hit.position;
            _agent.enabled = true;
        }
    }

    public void MoveTo(Vector3 position)
    {
        Debug.Log(gameObject.name + " going to " + position);
        _agent.SetDestination(position);   
    }

    /// <summary>
    /// Called when selected by the player
    /// </summary>
    public void OnSelected()
    {
        _selectionSprite.gameObject.SetActive(true);
        _outlineComponent.enabled = true;
    }

    /// <summary>
    /// Called when deselected by the player
    /// </summary>
    public void OnDeselected()
    {
        _selectionSprite.gameObject.SetActive(false);
        _outlineComponent.enabled = false;
    }
}
