using UnityEngine;

public class openSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject panelSettings;

    public void openSet()
    { 
        panelSettings.SetActive(!panelSettings.activeSelf);
    }
}
