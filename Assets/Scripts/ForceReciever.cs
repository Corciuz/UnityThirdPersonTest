using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag=0.05f;
    private Vector3 velocity;
    public Vector3 impact;
    public Vector3 Movement =>impact+ Vector3.up*verticalVelocity;
    private float verticalVelocity;
    private void Update() {
        if(verticalVelocity<0f &&controller.isGrounded){
         verticalVelocity=Physics.gravity.y*Time.deltaTime;
        }else{
            verticalVelocity+=Physics.gravity.y*Time.deltaTime;
        }
        impact=Vector3.SmoothDamp(impact,Vector3.zero,ref velocity,drag);
    }

    public void addForce(Vector3 Force){
    impact+=Force;
    }
}
