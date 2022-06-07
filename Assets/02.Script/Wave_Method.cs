using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 각 나무.
/// </summary>
public partial class Wave : LODActive
{
    public bool strongWind;
    public float strongBendStrength;
    private void Awake()
    {
        strongWind = false;
        interval = Random.Range(intervalMin, intervalMax);
        randomBendStrength = Random.Range(bendStrengthMin, bendStrengthMax);
        randomSpeed = Random.Range(speedMin, speedMax);

        windMaterial = transform.GetChild(lodid).GetComponent<Renderer>().material;
        windMaterial.SetFloat("_BendStrength", randomBendStrength);
        windMaterial.SetFloat("_Speed", randomSpeed);

    }

    private void Update()
    {
        //일정 시간마다 바람의 방향이 바뀐다.
        //일정 시간마다 바람의 세기가 달라진다.
        //노이즈가 랜덤한 시간마다 바뀐다.

        time += Time.deltaTime;
        if (time > interval&& !strongWind)
        {
            if (isChange)
            {
                RandomNum();         
            }
        
            //이전 위치에서 다음 위치로 자연스럽게 이동시켜야 함.

            currentDirection = windMaterial.GetVector("_Direction");

            if (Vector3.Distance(currentDirection, newRandomDirection) > 0)
            {
                windDirection = Vector3.MoveTowards(currentDirection, newRandomDirection, Time.deltaTime);

                windMaterial.SetVector("_Direction", windDirection);
            }
            else
            {
                time = 0f;
                isChange = true;
            }
        }
        else if(strongWind)
        {
            StrongWind();
            currentDirection = windMaterial.GetVector("_Direction");

            if (Vector3.Distance(currentDirection, newRandomDirection) > 0)
            {
                windDirection = Vector3.MoveTowards(currentDirection, newRandomDirection, Time.deltaTime);

                windMaterial.SetVector("_Direction", windDirection);
            }
            else
            {
                time = 0f;
               
            }
        }

    }

    void RandomNum()
    {
        randomDirection = Random.Range(0, 1f);
        newRandomDirection = new Vector3(randomDirection, randomDirection, 0);
        isChange = false;
    }

    void StrongWind()
    {
      
        windMaterial.SetFloat("_BendStrength", strongBendStrength);
        float strong = Random.Range(3, 5f);
        newRandomDirection = new Vector3(strong,0, 0);
        isChange = false;
    }

    void SoftWind()
    {

    }
}
