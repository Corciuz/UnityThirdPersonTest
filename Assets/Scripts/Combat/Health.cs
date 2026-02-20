using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]  private int MaxHealth=100;
    private int health;  
    
   private void Start()
    {
        health=MaxHealth;

        
    }
    public void dealDamage(int Damage){
        if(health==0){return; }
        health=Mathf.Max(health-Damage,0);
        Debug.Log(health);
    } 

    
    
}
