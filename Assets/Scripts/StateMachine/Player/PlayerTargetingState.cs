using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {}
    public readonly int TargetBlendTree=Animator.StringToHash("TargetingBlendTree");
    public readonly int TargetForward=Animator.StringToHash("TargettingForward");
    public readonly int TargetRight=Animator.StringToHash("TargetingRight");
    private const float AnimatorDampTime=0.3f;

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetBlendTree,AnimatorDampTime);
        stateMachine.InputReader.TargetEvent += OffTarget;
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OffTarget;
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.Attack){
            stateMachine.Switchstate(new PlayerAttackingState(stateMachine,0));
            return;
        }
        if(stateMachine.Targeter.CurrentTarget==null){
         stateMachine.Switchstate(new PlayerFreeLookState(stateMachine));
         return;
        }
        Vector3 movement=CalculateMovement();
        move(movement*stateMachine.TargettingMoveSpeed,deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
    }
    public void OffTarget(){
        stateMachine.Targeter.cancel();
        stateMachine.Switchstate(new PlayerFreeLookState(stateMachine));
    }
    private Vector3 CalculateMovement(){
        Vector3 movemet= new Vector3();
        movemet+=stateMachine.transform.right*stateMachine.InputReader.MovementValue.x;
        movemet+=stateMachine.transform.forward*stateMachine.InputReader.MovementValue.y;
        
        return movemet;
    }
    private void UpdateAnimator(float deltaTime){
if (stateMachine.InputReader.MovementValue.y ==0)
        {
            stateMachine.Animator.SetFloat(TargetForward, 0f,0.1f,deltaTime);
        }else{
            float value=stateMachine.InputReader.MovementValue.y>0?1f:-1f;
            stateMachine.Animator.SetFloat(TargetForward, value,0.1f,deltaTime);
        }
        if (stateMachine.InputReader.MovementValue.x ==0)
        {
            stateMachine.Animator.SetFloat(TargetRight, 0f,0.1f,deltaTime);
        }else{
            float value=stateMachine.InputReader.MovementValue.x>0?1f:-1f;
            stateMachine.Animator.SetFloat(TargetRight, value,0.1f,deltaTime);
        }
        
    }
        
    }
    

