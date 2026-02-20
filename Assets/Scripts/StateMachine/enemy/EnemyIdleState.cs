using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionHash=Animator.StringToHash("Locomotion");
     private readonly int SpeedHash=Animator.StringToHash("Blend");
    private const float crossfade=0.1f;
    private const float AnimatorDampTime=0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash,crossfade);
    }

    public override void Exit(){ }

    public override void Tick(float deltaTime)
    {
        move(deltaTime);

        if(IsInChaseRange()){
            stateMachine.Switchstate(new EnemyChasingState(stateMachine));
        }
        stateMachine.Animator.SetFloat(SpeedHash,0,AnimatorDampTime,deltaTime);
        
    }
}
