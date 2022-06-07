using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODActive : MonoBehaviour
{
    protected int lodid;
    private void Start()
    {
        GetLODObjofTrees();
    }
    void GetLODObjofTrees()
    {
        Transform[] lodchild = new Transform[3];

        LODGroup group = GetComponent<LODGroup>();
        lodid = GetVisibleLOD(group, Camera.main);
    }
   
    //플젝 전체에 걸쳐 하나만 존재해야 하는 것. 
    public static int GetVisibleLOD(LODGroup lodGroup, Camera camera = null)
    {
        var lods = lodGroup.GetLODs();
        var relativeHeight = GetRelativeHeight(lodGroup, camera ?? Camera.current);

        int lodIndex = GetMaxLOD(lodGroup);
        for (var i = 0; i < lods.Length; i++)
        {
            var lod = lods[i];

            if (relativeHeight >= lod.screenRelativeTransitionHeight)
            {
                lodIndex = i;
                break;
            }
        }


        return lodIndex;
    }
    public static int GetMaxLOD(LODGroup lodGroup)
    {
        return lodGroup.lodCount - 1;
    }
    static float GetRelativeHeight(LODGroup lodGroup, Camera camera)
    {
        var distance = (lodGroup.transform.TransformPoint(lodGroup.localReferencePoint) - camera.transform.position).magnitude;
        return DistanceToRelativeHeight(camera, (distance / QualitySettings.lodBias), GetWorldSpaceSize(lodGroup));
    }
    static float DistanceToRelativeHeight(Camera camera, float distance, float size)
    {
        if (camera.orthographic)
            return size * 0.5F / camera.orthographicSize;

        var halfAngle = Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView * 0.5F);
        var relativeHeight = size * 0.5F / (distance * halfAngle);
        return relativeHeight;
    }

    public static float GetWorldSpaceSize(LODGroup lodGroup)
    {
        return GetWorldSpaceScale(lodGroup.transform) * lodGroup.size;
    }
    static float GetWorldSpaceScale(Transform t)
    {
        var scale = t.lossyScale;
        float largestAxis = Mathf.Abs(scale.x);
        largestAxis = Mathf.Max(largestAxis, Mathf.Abs(scale.y));
        largestAxis = Mathf.Max(largestAxis, Mathf.Abs(scale.z));
        return largestAxis;
    }
}
