using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionHash=Animator.StringToHash("Locomotion");
     private readonly int SpeedHash=Animator.StringToHash("Blend");
    private const float crossfade=0.1f;
    private const float AnimatorDampTime=0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
         stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash,crossfade);
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity=Vector3.zero;
    }

    public override void Tick(float deltaTime)
    {
        if(!IsInChaseRange()){
            stateMachine.Switchstate(new EnemyIdleState(stateMachine));
            return;
        }
        MoveToPlayer(deltaTime);
        stateMachine.Animator.SetFloat(SpeedHash,1,AnimatorDampTime,deltaTime);
    }

    private void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.destination=stateMachine.Player.transform.position;
        move(stateMachine.Agent.desiredVelocity.normalized*stateMachine.MovementSpeed,deltaTime);
        stateMachine.Agent.velocity=stateMachine.Controller.velocity;
    }
}
