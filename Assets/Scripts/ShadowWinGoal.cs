using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowWinGoal : MonoBehaviour
{

    bool win = false;
    public string _sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!win && other.gameObject.tag == "Shadow" && other.gameObject.GetComponent<TriangleFollowing>().isActive()) {
            SceneManager.LoadScene(_sceneName);

            win = true;
        }
        
    }

}
