using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public readonly int FreeLookSpeedHash=Animator.StringToHash("FreeLookSpeed");
    public readonly int FreeLookBlendTree=Animator.StringToHash("FreeLookBlendTree");
    private const float AnimatorDampTime=0.1f;
    private const float crossfade=0.3f;
    

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTree,crossfade);
        stateMachine.InputReader.TargetEvent += OnTarget;
        
    }

    public override void Exit()
    {
    stateMachine.InputReader.TargetEvent -= OnTarget;
            
    }
    public void OnTarget(){
       if(!stateMachine.Targeter.SelectTarget()){return;}
            stateMachine.Switchstate(new PlayerTargetingState(stateMachine));
            

    }

    public override void Tick(float deltaTime)
    {
        
        if(stateMachine.InputReader.Attack){
            
            stateMachine.Switchstate(new PlayerAttackingState(stateMachine,0));
            return;
        }
        Vector3 Movement = CalculateMovement();


        move(Movement* stateMachine.FreeLookMoveSpeed,deltaTime);
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);

            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(Movement,deltaTime);

    }

    private void FaceMovementDirection(Vector3 Movement,float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation
        ,Quaternion.LookRotation(Movement),deltaTime*stateMachine.RotationSmoothValue);
    }

    private Vector3 CalculateMovement(){
       Vector3 forward= stateMachine.MainCameraTransform.forward;
       Vector3 right= stateMachine.MainCameraTransform.right;
       forward.y=0f;
       right.y=0f;
       forward.Normalize();
       right.Normalize();
       return forward*stateMachine.InputReader.MovementValue.y+right*stateMachine.InputReader.MovementValue.x;
    }
    
   
}
