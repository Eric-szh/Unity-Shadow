using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
 
         int vertexCount = 200;
         Vector3[] vertices = new Vector3[vertexCount];
 
         int[] indices = new int[vertexCount];
 
         for(int i=0; i<vertexCount; i++){
             vertices[i] = new Vector3(Random.value, Random.value, Random.value);
             indices[i] = i;
         }
 
         Mesh m = new Mesh();
         m.vertices = vertices;
         m.SetIndices(indices, MeshTopology.Points, 0);
         m.RecalculateBounds();
 
         MeshFilter mf = GetComponent<MeshFilter>();
         mf.mesh = m;
         Debug.Log(m.bounds);
     }
}
