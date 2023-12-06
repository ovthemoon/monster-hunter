using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Mode", menuName = "Scriptable Object/Gun Data")]
public class GunMode : ScriptableObject
{
    public float coolTime;
    public AttackMode mode;
    public float damage;
}
