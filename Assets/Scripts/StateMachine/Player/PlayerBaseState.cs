using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine){
this.stateMachine=stateMachine;
    }
    protected void move(float deltaTime){
        move(Vector3.zero,deltaTime);
    }

    protected void move(Vector3 motion,float deltaTime){
    stateMachine.Controller.Move((motion+stateMachine.ForceReciever.Movement)*deltaTime);
    }
    protected void FaceTarget(){
        if(stateMachine.Targeter.CurrentTarget==null){return;}
        Vector3 LookPos=(stateMachine.Targeter.CurrentTarget.transform.position-stateMachine.transform.position);
        LookPos.y=0;
        stateMachine.transform.rotation=Quaternion.LookRotation(LookPos);
    }
}
