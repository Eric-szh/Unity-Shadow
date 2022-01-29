using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtility : MonoBehaviour
{
     static public void LogList<T>(List<T> list) {
        foreach(T t in list) {
            Debug.Log(t);
        }
    }
}
