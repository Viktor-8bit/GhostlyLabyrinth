using UnityEngine;
using TMPro;

public class TMPmanager : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public void Deactivate() { textMeshPro.enabled = false; }
    public void Activate() { textMeshPro.enabled = true; }
}
