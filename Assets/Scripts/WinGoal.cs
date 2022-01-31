using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGoal : MonoBehaviour
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
        Debug.Log("win");
        if (!win && other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerMoving>().isActive() ) {
            
            SceneManager.LoadScene(_sceneName);

            win = true;
        }
        
    }
}
