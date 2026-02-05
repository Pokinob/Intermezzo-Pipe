using UnityEngine;

public class globalPause : MonoBehaviour
{
    public static globalPause instance;
    
    public bool _globalPause = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
