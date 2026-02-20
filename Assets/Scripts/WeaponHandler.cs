using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
   [SerializeField] GameObject WeaponLogic;

   public void EnableWeapon(){
    WeaponLogic.SetActive(true);
   }
   public void DisableWeapond(){
    WeaponLogic.SetActive(false);
   }
}
