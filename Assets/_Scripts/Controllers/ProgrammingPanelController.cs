using System.Collections.Generic;
using UnityEngine;

public class ProgrammingPanelController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private GameObject placeholderPrefab;
    [SerializeField] private RectTransform contentParent;

    public RectTransform ContentParent => contentParent;

    private GameObject placeholderInstance;

    void Awake()
    {
        if (placeholderPrefab != null && contentParent != null)
        {
            placeholderInstance = Instantiate(placeholderPrefab, contentParent);
            placeholderInstance.SetActive(false);
        }
    }

    public int GetPlaceholderIndex()
    {
        return placeholderInstance.transform.GetSiblingIndex();
    }

    public void HidePlaceholder()
    {
        if (placeholderInstance != null)
        {
            placeholderInstance.SetActive(false);
        }
    }

    public void UpdatePlaceholderPosition(Vector3 mousePosition)
    {
        if (placeholderInstance == null) return;
        placeholderInstance.SetActive(true);

        int newSiblingIndex = contentParent.childCount;
        for (int i = 0; i < contentParent.childCount; i++)
        {
            Transform child = contentParent.GetChild(i);
            if (child.gameObject.activeSelf && child != RuneUI.DraggedInstance.transform)
            {
                if (mousePosition.y > child.position.y)
                {
                    newSiblingIndex = i;
                    break;
                }
            }
        }
        placeholderInstance.transform.SetSiblingIndex(newSiblingIndex);
    }

    public List<RuneUI> GetProgramRunes()
    {
        List<RuneUI> runes = new List<RuneUI>();
        foreach (Transform child in contentParent)
        {
            if (child.gameObject.activeSelf && child.GetComponent<RuneUI>() != null)
            {
                runes.Add(child.GetComponent<RuneUI>());
            }
        }
        return runes;
    }

    public void ClearProgram()
    {
        foreach (Transform child in contentParent)
        {
            if (child.GetComponent<RuneUI>() != null)
                Destroy(child.gameObject);
        }
    }
}