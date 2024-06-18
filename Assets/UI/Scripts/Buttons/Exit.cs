using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Button Button;

    void Awake()
    {
        Button.onClick.AddListener(Application.Quit);
    }
}
