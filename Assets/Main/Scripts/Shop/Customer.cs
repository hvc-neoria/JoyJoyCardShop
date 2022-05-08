using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public partial class Customer : MonoBehaviour
{
    CustomerStateBase _currentState = goingGoods;
    CustomerStateBase currentState
    {
        get { return _currentState; }
        set
        {
            _currentState.OnExit(this, value);
            value.OnEnter(this, _currentState);
            _currentState = value;
            stateName = value.ToString();
        }
    }
    // debug
    [SerializeField] string stateName;
    static readonly GoingGoods goingGoods = new GoingGoods();
    static readonly GoingRegister goingRegister = new GoingRegister();
    static readonly GoingOutside goingOutside = new GoingOutside();
    static readonly Vector3 RegisterPos = Vector3.zero;

    Transform myTrans;
    NavMeshAgent nav;
    PackInstantiater packInstantiater;

    Vector3 trueDestination;
    ReadPack[] readPacks;

    Animator animator;

    void Start()
    {
        myTrans = transform;
        nav = GetComponent<NavMeshAgent>();
        packInstantiater = GetComponent<PackInstantiater>();
        animator = GetComponentInChildren<Animator>();
        currentState.OnEnter(this, null);
    }

    void Update()
    {
        currentState.OnUpdate(this);
        //TODO:腕振りをwalkSpeedに連動させる方法が分からない
        // float walkSpeed = nav.velocity.magnitude;
        // animator.SetFloat("WalkSpeed", walkSpeed);
        // if (walkSpeed > 0.1f)
        // {
        //     animator.SetFloat("WalkSpeed", walkSpeed);
        //     animator.SetInteger("arms", 1);
        //     animator.SetInteger("legs", 1);
        // }
        // else
        // {
        //     animator.SetInteger("legs", 10);
        //     animator.SetInteger("arms", 10);
        // }
    }

    void SetDestination(Vector3 position)
    {
        trueDestination = position;
        nav.SetDestination(position);
    }

    public abstract class CustomerStateBase
    {
        public virtual void OnEnter(Customer owner, CustomerStateBase prevState) { }
        public virtual void OnUpdate(Customer owner) { }
        public virtual void OnExit(Customer owner, CustomerStateBase nextState) { }
    }
}