using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float PreviousFrameTime;
    private bool AllReaddyApp;
    private Attack attack;
    
    public PlayerAttackingState(PlayerStateMachine stateMachine,int attackId) : base(stateMachine)
    {
       attack= stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        stateMachine.weapon.SetAttack(attack.Damage);
        stateMachine.Animator.CrossFadeInFixedTime(attack.Animation,attack.Transition);
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        move(deltaTime);
        FaceTarget();
        
        float normalizedTime=GetNormalizeTime();
        
        if(normalizedTime>=PreviousFrameTime&&normalizedTime<1f){

         if(normalizedTime>=attack.ForceTime){
            TryApplyForce();
        }

            if(stateMachine.InputReader.Attack){
              tryComboAttack(normalizedTime);  
            }
            
            
        }else{
            if(stateMachine.Targeter.CurrentTarget!=null){
stateMachine.Switchstate(new PlayerTargetingState(stateMachine));
            }else{
                stateMachine.Switchstate(new PlayerFreeLookState(stateMachine));

            }
        }
        PreviousFrameTime=normalizedTime;
        
    }

    private void tryComboAttack(float normalizedTime)
    {
       if(attack.comboStateIndex==-1){
        return;
       }

       if(normalizedTime<attack.ComboAttactTime){
        return;
       }
       stateMachine.Switchstate(new PlayerAttackingState(stateMachine,attack.comboStateIndex));
    }

    private float GetNormalizeTime(){
        AnimatorStateInfo currentInfo=stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo=stateMachine.Animator.GetNextAnimatorStateInfo(0);
        if(stateMachine.Animator.IsInTransition(0)&&nextInfo.IsTag("Attack")){
         return nextInfo.normalizedTime;
        
        }else if(!stateMachine.Animator.IsInTransition(0)&&currentInfo.IsTag("Attack")){
return currentInfo.normalizedTime;
        }else{
            return 0f;
        }
    }

    private void TryApplyForce(){
        if(AllReaddyApp){return;}
stateMachine.ForceReciever.addForce(stateMachine.transform.forward*attack.Force);
        AllReaddyApp=true;

    }
}
