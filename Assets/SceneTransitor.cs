using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitor : MonoBehaviour
{
    public int SceneIndex;
    private void OnTriggerEnter(Collider other)
    {
       //if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            SceneLoaderManager.Instance.LoadSingleScene(SceneIndex);
            
        }
        
    }
}
