using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 각 나무.
/// </summary>
public partial class Wave : LODActive
{
    [SerializeField]
    float randomBendStrength;
    [SerializeField]
    float randomSpeed;
    public float bendStrengthMin;
    public float bendStrengthMax;
    public float speedMin;
    public float speedMax;
    float interval;
    public float intervalMin;
    public float intervalMax;
    [SerializeField]
    Material windMaterial;
    Vector3 windDirection;
    Vector3 newRandomDirection;
    Vector3 currentDirection;
    float time;
    float randomDirection;
    bool isChange = true;
   

}
