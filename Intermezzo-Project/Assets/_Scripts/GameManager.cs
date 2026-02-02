using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Exposure=0;
    [SerializeField]
    private int targetExposure;

    private void Update()
    {
        if(Exposure >= targetExposure)
        {
            UIlose();
        }
    }

    private void UIlose()
    {
        Debug.Log("Data get leaked by user");
    }
}
