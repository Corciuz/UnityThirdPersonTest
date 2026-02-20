using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    private List<Target>Targets=new List<Target>();
    Camera maincamera;
    [SerializeField] private CinemachineTargetGroup CineTargetGroup;
    public Target CurrentTarget{get;private set;}
    private void Start() {
      maincamera=Camera.main;
    }
    private void OnTriggerEnter(Collider other) {
        if(!other.TryGetComponent<Target>(out Target target)){return;}
        Targets.Add(target);
        target.OnDestroyed+=RemoveTarget;
        
        }
    private void OnTriggerExit(Collider other) {
        if(!other.TryGetComponent<Target>(out Target target)){return;}
        RemoveTarget(target);
        
     }
     public bool SelectTarget(){
        if(Targets.Count==0){return false;}
       Target ClosestTarget=null;
       float ClosestTargetDis=Mathf.Infinity;



        foreach(Target target in Targets){
        Vector2 ViewPos=maincamera.WorldToViewportPoint(target.transform.position);
        if(ViewPos.x>1 || ViewPos.x<0 || ViewPos.y>1 || ViewPos.y<0){
        continue;
        }
        
        Vector2 ToCenter=ViewPos-new Vector2(0.5f,0.5f);
        if(ToCenter.sqrMagnitude<ClosestTargetDis){
         ClosestTarget=target;
         ClosestTargetDis=ToCenter.sqrMagnitude;
        }
        }
        if(ClosestTarget == null){return false;}

        CurrentTarget=ClosestTarget;
        CineTargetGroup.AddMember(CurrentTarget.transform,1f,2f);
        return true;
     }
     public void cancel(){
        if(CurrentTarget==null){return;}
        CineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget=null;
     }
     public void RemoveTarget(Target target){
      if(CurrentTarget==target){
        CineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget=null;
      }
      target.OnDestroyed -=RemoveTarget;
      Targets.Remove(target);
     }

}
