using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Transform firstPos;
    [SerializeField] private Transform secPos;
    [SerializeField] private Transform thirdPos;
    [SerializeField] private Transform fourPos;
    [SerializeField] private Transform fifsPos;

    private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _movementSpeed;
    readonly int m_HashMoving = Animator.StringToHash("MoveSpeed");
    

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.Log("<<<<<Zombie>>>>> NavMeshAgent isn't exist!! >>> " + gameObject.name);
        } else
        {
            SetTheDestination(thirdPos);
        }


    }

    private void SetTheDestination(Transform destinationTarget)
    {
        if (destinationTarget != null)
        {
            if (animator != null)
            {
                animator.SetFloat(m_HashMoving, _movementSpeed);
                Vector3 targetVector = destinationTarget.transform.position;
                _navMeshAgent.SetDestination(targetVector);
            }
            else
            {
                Debug.Log("<<<<<Zombie>>>>> Zombie hasn't walking animator!! >>> " + gameObject.name);
            }
        }
    }
}
