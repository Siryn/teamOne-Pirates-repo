using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    public NavMeshAgent Agent;
    public float distanceRemainingTreshold;
    private bool m_destinationReached;
    public bool DestinationReached
    {
        get { return m_destinationReached; }
        set
        {
            m_destinationReached = value;
            if (m_destinationReached != false)
            {
                OnDestinationReached();
            }
        }
    }

    public event System.Action OnDestinationReached;


    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DestinationReached || !Agent.hasPath)
            return;

        if (Agent.remainingDistance < distanceRemainingTreshold)
            DestinationReached = true;

    }

    public void SetTarget(Vector3 target)
    {
        DestinationReached = false;
        Agent.SetDestination(target);
    }
}
