using UnityEngine;
using UnityEngine.UI;

public class ActivateByButton : MonoBehaviour
{
    public Button Button;
    public GameObject ActivateGameObject;

    void Awake()
    {
        Button.onClick.AddListener(() =>
        {
            ActivateGameObject.SetActive(true);
        });
    }

}
