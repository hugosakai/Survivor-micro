using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Config/Attack Config")]
public class AttackConfig : ScriptableObject
{
    public float interval;
    public float window;
    public float jitter;
    public float initialDelay;
}
