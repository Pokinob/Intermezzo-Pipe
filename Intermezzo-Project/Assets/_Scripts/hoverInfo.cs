using UnityEngine;
using UnityEngine.EventSystems;

public class SkillHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject infoPanel;

    void Start()
    {
        infoPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
    }
}