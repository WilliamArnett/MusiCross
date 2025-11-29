using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    public string scene;
    
    public void Enter()
    {
        SceneManager.LoadScene(scene);
    }
}
