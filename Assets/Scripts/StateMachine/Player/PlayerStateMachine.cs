using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field:SerializeField] public InputReader InputReader{get; private set;}
    [field:SerializeField] public CharacterController Controller{get; private set;}
    [field:SerializeField] public Animator Animator{get; private set;}
    [field:SerializeField] public Targeter Targeter{get; private set;}
    [field:SerializeField] public ForceReciever ForceReciever{get; private set;}
    [field:SerializeField] public WeaponDamage weapon{get; private set;}
    [field:SerializeField] public float RotationSmoothValue{get; private set;}

    
    [field:SerializeField] public float FreeLookMoveSpeed{get; private set;}
    [field:SerializeField] public float TargettingMoveSpeed{get; private set;}
    [field:SerializeField] public Attack [] Attacks{get; private set;}
    public Transform MainCameraTransform {get; private set;}
    
    private void Start()
    {
        MainCameraTransform=Camera.main.transform;
        Switchstate(new PlayerFreeLookState(this));
    }

    
}
