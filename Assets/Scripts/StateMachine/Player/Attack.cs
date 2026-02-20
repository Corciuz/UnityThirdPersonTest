using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Attack
{
    [field:SerializeField]public string Animation{get; private set;}
    [field:SerializeField]public float Transition{get; private set;}
    [field:SerializeField]public int comboStateIndex{get; private set;}=-1;
    [field:SerializeField]public float ComboAttactTime{get; private set;}
    [field:SerializeField]public float ForceTime{get; private set;}
    [field:SerializeField]public float Force{get; private set;}
    [field:SerializeField]public int Damage{get; private set;}
}
