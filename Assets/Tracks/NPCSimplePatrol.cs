using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSimplePatrol : MonoBehaviour
{
    [SerializeField] private bool _patrolWating;

    [SerializeField] private float _totalWaitingTime;

    [SerializeField] private float _switchProbability = 0.2f;

    [SerializeField] List<Waypoint> _tracker;

    [SerializeField] private Animator animator;
    readonly int m_HashMoving = Animator.StringToHash("MoveSpeed");
    [SerializeField] private float _movementSpeed = 0.5f;



    //Privates
    private NavMeshAgent _navMeshAgent;
    private int _currentPatrolIndex;
    private float _waitingTime;

    //Flags
    private bool _travelling;
    private bool _traveling;
    private bool _waiting;
    private bool _following;
    private bool _patrolForward;
    private GameObject _attackedObject;
    private Vector3 _targetVector;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(_navMeshAgent == null)
        {
            Debug.Log("<<<<<Warning!!>>>>> NavMeshAgent isn't exist on.. >>> " + gameObject.name);
        }
        else
        {
            if(_tracker != null && _tracker.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDistanetion();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(_traveling && _navMeshAgent.remainingDistance <= 1.0F)
        {
            _traveling = false;
            if (_patrolWating)
            {
                _waiting = true;
                _waitingTime = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDistanetion();
            }
        }
        if (_waiting)
        {
            _waitingTime += Time.deltaTime;
            if(_waitingTime >= _totalWaitingTime)
            {
                _waiting = false;
                ChangePatrolPoint();
                SetDistanetion();
            }
        }
    }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }
        if (_patrolForward)
        {

            _currentPatrolIndex++;
            if (_currentPatrolIndex >= _tracker.Count)
            {
                _currentPatrolIndex = 0;
            }

            // _currentPatrolIndex = (_currentPatrolIndex + 1) % _tracker.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _tracker.Count - 1;
            }
        }
    }

    private void SetDistanetion()
    {
        Debug.Log("<<<<<SetDistanation()!!>>>>> _currentPatrolIndex >>> " + _currentPatrolIndex +
            " ; _tracker.Count >>> " + _tracker.Count +
            " ; _patrolForward >>> " + _patrolForward);

        if (_tracker != null)
        {
            if (animator != null)
            {
                animator.SetFloat(m_HashMoving, _movementSpeed);
                if (_attackedObject == null)
                {
                    _targetVector = _tracker[_currentPatrolIndex].transform.position;
                }
                else
                {
                    _targetVector = _attackedObject.transform.position;
                }
                _navMeshAgent.SetDestination(_targetVector);
                _traveling = true;
            }
            else
            {
                Debug.Log("<<<<<Zombie>>>>> Zombie hasn't walking animator!! >>> " + gameObject.name);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("<<<<<Zombie>>>>> Zombie ATACK!! >>> " + gameObject.name);

        _attackedObject = other.gameObject;
        SetDistanetion();
    }
    private void OnTriggerStay(Collider other)
    {
        SetDistanetion();
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("<<<<<Zombie>>>>> Zombie OOOOOWWW!! >>> " + gameObject.name);

        _attackedObject = null;
        SetDistanetion();
    }

}
