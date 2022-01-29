using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using System;

public class GenerateShadow : MonoBehaviour
{
    public GameObject light;
    private Vector3 dir;
    private Mesh selfMesh;
    GameObject shadowObj;
    Vector3[] vertices;
    Vector3[] noDupeVert;
    Vector3 scale;
    List<Vector3> topPoints;
    List<Vector3> bottomPoints;    

    private void Awake() {
        dir = light.GetComponent<Transform>().forward;
        selfMesh = GetComponent<MeshFilter>().mesh;
        vertices = selfMesh.vertices;
        noDupeVert = vertices.Distinct().ToArray();
        scale = transform.localScale;
        topPoints = new List<Vector3>();
        bottomPoints = new List<Vector3>();
        SeperatePoints();
        // LogList(topPoints);
        // Debug.Log("----------");
        // LogList(bottomPoints);
    }

    private void SeperatePoints() {
        float Lowest = 9999.0f;
        foreach (Vector3 vect in noDupeVert) {
            if (vect[1] < Lowest) {
                Lowest = vect[1];
            }
        }
        foreach (Vector3 vect in noDupeVert) {
            if (vect[1] == Lowest) {
                bottomPoints.Add(vect);
            } else {
                topPoints.Add(vect);
            }
        }
    }

    private Vector3 GlobalPoint(Vector3 local) {
        return transform.rotation * SimpleMult(local, scale) + transform.position;
    }

    private Vector3 SimpleMult(Vector3 v1, Vector3 v2) {
        return new Vector3(v1[0] * v2[0], v1[1] * v2[1], v1[2] * v2[2]); 
    }

    private void LogList<T>(List<T> list) {
        foreach(T t in list) {
            Debug.Log(t);
        }
    }

    private List<Vector3> RemoveColinear(List<Vector3> list) {
        var newList = list.ConvertAll(v3 => new Vector3(v3[0],v3[1],v3[2]));
        for(int i = newList.Count - 1; i >= 0; i--) {


            var p1 = newList[(i + 1 + newList.Count) % newList.Count];
            var p2 = newList[(i + newList.Count) % newList.Count];
            var p3 = newList[(i - 1 + newList.Count) % newList.Count];

            var coValue = (p2[0] - p1[0])*(p3[2] - p2[2]) - (p2[2] - p1[2])*(p3[0] - p2[0]);
            var isCo = Math.Abs(coValue - 0) < 0.0001;
            if (isCo) {
                newList.RemoveAt(i);
            }
        }
        return newList;
    }

    // Start is called before the first frame update
    void Start()
    {
       GenShadow(false);
    }

    List<Vector3> convexHull;

    void GenShadow(bool haveBase) {
        List<Vector3> hitPoints = new List<Vector3>();
        foreach (Vector3 vect in topPoints) {
            RaycastHit hit;
            Ray ray = new Ray(transform.rotation * SimpleMult(vect, scale) + transform.position, light.transform.forward);
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Land"))) {
                hitPoints.Add(hit.point);
            }
            
        }
        foreach (Vector3 vect in bottomPoints) {
            hitPoints.Add(GlobalPoint(vect));
        }
        convexHull = ConvexHull.compute(hitPoints);
        
        //LogList(convexHull);

        if (!haveBase) {

            var gBottomPoints = new List<Vector3>();
            foreach (Vector3 vert in bottomPoints){
                gBottomPoints.Add(GlobalPoint(vert));
            }
            gBottomPoints = ConvexHull.compute(gBottomPoints);

            convexHull = RemoveColinear(convexHull);
            List<Vector3> pointsToFit =  gBottomPoints.Except(convexHull).ToList();
            List<Vector3> pointsToReplace = new List<Vector3>();

            foreach (Vector3 vert in pointsToFit) {
                var myIndex = gBottomPoints.FindIndex(a => a == vert);
                var otherIndex = (myIndex + 2) % 4;
                pointsToReplace.Add(gBottomPoints[otherIndex]);
            }

            pointsToFit.Reverse();

            for (int i = 0; i < pointsToFit.Count; i++) {
               var indexToReplace = convexHull.FindIndex(a => a == pointsToReplace[i]);
                convexHull[indexToReplace] = pointsToFit[i];
            }
        }
        var yOffset = 0.07f;
        var convexHullPlus = convexHull.ConvertAll(v => new Vector3(v[0], v[1] + yOffset, v[2]));
        convexHull.AddRange(convexHullPlus);


        shadowObj = new GameObject("shadow");
        shadowObj.transform.parent = this.gameObject.transform;
        shadowObj.AddComponent<Rigidbody>();
        shadowObj.AddComponent<MeshFilter>();
        shadowObj.AddComponent<MeshRenderer>();
        shadowObj.GetComponent<Rigidbody>().isKinematic = true;
        shadowObj.GetComponent<MeshFilter>().mesh.vertices = convexHull.ToArray();

        

        if (convexHull.Count == 8) {
            shadowObj.GetComponent<MeshFilter>().mesh.triangles = new int[]{2,1,0, 2,0,3, 6,5,4, 6,4,7};
        } else {
            shadowObj.GetComponent<MeshFilter>().mesh.triangles = new int[]{2,1,0, 4,3,2, 4,0,5, 0,4,2, 8,7,6, 10,9,8, 10,6,11, 6,10,8};
        }

        shadowObj.AddComponent<MeshCollider>();
        shadowObj.GetComponent<MeshCollider>().convex = true;
        
        //Debug.Log("---------");

        //LogList(shadowObj.GetComponent<MeshFilter>().mesh.vertices.ToList());




    }

    // Update is called once per frame
    void Update()
    {
        // foreach (Vector3 vect in topPoints) {
        //     RaycastHit hit;
        //     Ray ray = new Ray(transform.rotation * SimpleMult(vect, scale) + transform.position, light.transform.forward);
        //     if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Land"))) {
        //         Debug.DrawLine(ray.origin, hit.point, Color.red);
        //     }
        // }
            


        for(var i = 1; i < convexHull.Count; i++){
           Debug.DrawLine(convexHull[i-1], convexHull[i], Color.red);
        }
    } 

    
}
