
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private SpriteRenderer _selectionSprite;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        SelectionManager.Instance.AvailableUnits.Add(this);
        _agent = GetComponent<NavMeshAgent>();

        _agent.enabled = false;
        
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            Debug.Log(hit.position);
            transform.position = hit.position;
            _agent.enabled = true;
        }
    }

    public void MoveTo(Vector3 position)
    {
        _agent.SetDestination(position);   
    }

    /// <summary>
    /// Called when selected by the player
    /// </summary>
    public void OnSelected()
    {
        _selectionSprite.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when deselected by the player
    /// </summary>
    public void OnDeselected()
    {
        _selectionSprite.gameObject.SetActive(false);
    }
}
