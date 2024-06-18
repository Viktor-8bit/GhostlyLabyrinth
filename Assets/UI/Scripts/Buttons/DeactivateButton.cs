using System;
using JetBrains.Annotations;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DeactivateButton : MonoBehaviour
{
    public Button Button;
    public GameObject ActivateGameObject;
    public TMP_Text textMeshPro;

    void Awake()
    {
        Button.onClick.AddListener(() =>
        {
            ActivateGameObject.SetActive(false);
            textMeshPro.color = Color.black;
        });
    }
     

    public void OnMouseEnter()
    {
        textMeshPro.color = Color.white;
    }

    public void OnMouseExit()
    {
        textMeshPro.color = Color.black;
    }

}
