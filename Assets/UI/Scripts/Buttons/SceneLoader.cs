using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
  
    public string SceneName;
    public Button Button;

    void Awake()
    {
        Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneName);
        });
    }

}
