using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
   public EnemyBaseState(EnemyStateMachine stateMachine){
this.stateMachine=stateMachine;
    }
     protected void move(float deltaTime){
        move(Vector3.zero,deltaTime);
    }

    protected void move(Vector3 motion,float deltaTime){
    stateMachine.Controller.Move((motion+stateMachine.ForceReciever.Movement)*deltaTime);
    }
    protected bool IsInChaseRange(){
        float toPlayer = (stateMachine.Player.transform.position-stateMachine.transform.position).sqrMagnitude;
        
        return toPlayer<=stateMachine.PlayerChaseRange*stateMachine.PlayerChaseRange;
    }
}
