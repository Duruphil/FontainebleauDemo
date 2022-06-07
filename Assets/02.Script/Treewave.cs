//using UnityEngine;
//using System.Collections;

//public class Treewave : MonoBehaviour
//{
//    public GameObject[] trees;
//    public float[] randomNum;
//    public float[] randomSpeed;
//    public float min;
//    public float max;
//    public float speedMin;
//    public float speedMax;
//    public GameObject[] children;
//    public Material[] windMats;
//    float time;
//    Vector3 windDir;
//    Vector3 newDir;
//    Vector3 currentDir;
//    float random;
//    bool isChange = true;

    
//    void Awake()
//    {
        
//        trees = GameObject.FindGameObjectsWithTag("Tree");
//        randomNum = new float[trees.Length];
//        randomSpeed = new float[trees.Length];
//        children = new GameObject[trees.Length];
//        windMats = new Material[trees.Length];
//        newDir = new Vector3(1, 1, 0);
//        for (int i = 0; i < randomNum.Length; i++)
//        {
//            randomNum[i] = Random.Range(min, max);
//            randomSpeed[i] = Random.Range(speedMin, speedMax);
//        }
//        GetLODObjofTrees();

//    }

//    void GetLODObjofTrees()
//    {

//        Transform[] lodchild = new Transform[3];
//        for (int i = 0; i < trees.Length; i++)
//        {
//            children[i] = trees[i].transform.GetChild(0).gameObject;
          
//            LODGroup group = children[i].GetComponent<LODGroup>();
//           int lodid = GetVisibleLOD(group, Camera.main);
//            //Debug.Log("children" + i + ": " + lodid);
//            windMats[i] = children[i].transform.GetChild(lodid).GetComponent<Renderer>().material;
//            Debug.Log("gameobject." + trees[i].name + "/" + "material:" + windMats[i]);
//            windMats[i].SetFloat("_BendStrength", randomNum[i]);
//            windMats[i].SetFloat("_Speed", randomSpeed[i]);
//           // Debug.Log("material"+i +": "+windMats[i].GetFloat("_BendStrength"));

//        }

//    }
//    //returns the currently visible LOD level of a specific LODGroup, from a specific camera. If no camera is define, uses the Camera.current.

//    //플젝 전체에 걸쳐 하나만 존재해야 하는 것. 
//    public static int GetVisibleLOD(LODGroup lodGroup, Camera camera = null)
//    {
//        var lods = lodGroup.GetLODs();
//        var relativeHeight = GetRelativeHeight(lodGroup, camera ?? Camera.current);


//        int lodIndex = GetMaxLOD(lodGroup);
//        for (var i = 0; i < lods.Length; i++)
//        {
//            var lod = lods[i];

//            if (relativeHeight >= lod.screenRelativeTransitionHeight)
//            {
//                lodIndex = i;
//                break;
//            }
//        }


//        return lodIndex;
//    }
//    public static int GetMaxLOD(LODGroup lodGroup)
//    {
//        return lodGroup.lodCount - 1;
//    }
//    static float GetRelativeHeight(LODGroup lodGroup, Camera camera)
//    {
//        var distance = (lodGroup.transform.TransformPoint(lodGroup.localReferencePoint) - camera.transform.position).magnitude;
//        return DistanceToRelativeHeight(camera, (distance / QualitySettings.lodBias), GetWorldSpaceSize(lodGroup));
//    }
//    static float DistanceToRelativeHeight(Camera camera, float distance, float size)
//    {
//        if (camera.orthographic)
//            return size * 0.5F / camera.orthographicSize;

//        var halfAngle = Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView * 0.5F);
//        var relativeHeight = size * 0.5F / (distance * halfAngle);
//        return relativeHeight;
//    }
//    public static float GetWorldSpaceSize(LODGroup lodGroup)
//    {
//        return GetWorldSpaceScale(lodGroup.transform) * lodGroup.size;
//    }
//    static float GetWorldSpaceScale(Transform t)
//    {
//        var scale = t.lossyScale;
//        float largestAxis = Mathf.Abs(scale.x);
//        largestAxis = Mathf.Max(largestAxis, Mathf.Abs(scale.y));
//        largestAxis = Mathf.Max(largestAxis, Mathf.Abs(scale.z));
//        return largestAxis;
//    }

//    private void Update()
//    {
//        //일정 시간마다 바람의 방향이 바뀐다.
//        //일정 시간마다 바람의 세기가 달라진다.
//        //노이즈가 랜덤한 시간마다 바뀐다.


//        time += Time.deltaTime;

//        float limitTime = 5f;

//        if (time > limitTime)
//        {
//            if(isChange)
//            {
//                RandomNum();
//            }
          
          
//            //이전 위치에서 다음 위치로 자연스럽게 이동시켜야 함.

//            currentDir = windMats[0].GetVector("_Direction");
         
//            if (Vector3.Distance(currentDir, newDir) > 0)
//            {


//                windDir = Vector3.MoveTowards(currentDir, newDir, Time.deltaTime);
//                for (int i = 0; i < trees.Length; i++)
//                {
//                    windMats[i].SetVector("_Direction", windDir);
//                    Debug.Log("wind direction change" + windDir);
//                }
             

//            }
//            else
//            {
//                time = 0f;
//                Debug.Log("time zero");
//                isChange = true;
//            }

//        }

//    }

//    void RandomNum()
//    {
//        Debug.Log("newDIr");
//        random = Random.Range(0, 1f);
//        newDir = new Vector3(random, random, 0);
       
//        isChange = false;
//    }
   
//}
