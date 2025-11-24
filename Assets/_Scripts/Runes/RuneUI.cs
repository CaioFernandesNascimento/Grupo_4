using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RuneUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ... (nenhuma mudança nas variáveis aqui) ...
    private RuneData runeData;
    [SerializeField] private Image runeImage;
    [SerializeField] private TextMeshProUGUI nameText;
    public static GameObject DraggedInstance;
    private GameObject masterPrefab;
    private CanvasGroup canvasGroup;
    private bool isFromPalette;
    private Color originalColor;


    public void SetMasterPrefab(GameObject prefab) { masterPrefab = prefab; }

    public void Setup(RuneData data)
    {
        this.runeData = data;
        if (runeData != null)
        {
            if (nameText != null) nameText.text = runeData.runeType.ToString() + "()";
            if (runeImage != null)
            {
                runeImage.sprite = runeData.runeSprite;
                runeImage.color = Color.white;
                originalColor = runeImage.color;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isFromPalette = transform.parent.GetComponent<ProgrammingPanelController>() == null && transform.parent.GetComponentInParent<ProgrammingPanelController>() == null;

        if (isFromPalette)
        {
            DraggedInstance = Instantiate(masterPrefab, GetComponentInParent<Canvas>().transform);
            RuneUI newRuneScript = DraggedInstance.GetComponent<RuneUI>();
            newRuneScript.Setup(this.runeData);
            newRuneScript.SetMasterPrefab(this.masterPrefab); 
        }
        else
        {
            DraggedInstance = this.gameObject;
            transform.SetParent(GetComponentInParent<Canvas>().transform);
        }

        DraggedInstance.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        canvasGroup = DraggedInstance.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = DraggedInstance.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DraggedInstance != null)
        {
            DraggedInstance.transform.position = eventData.position;
            
            ProgrammingPanelController panelBelow = null;
            if (eventData.pointerEnter != null)
            {
                 panelBelow = eventData.pointerEnter.GetComponentInParent<ProgrammingPanelController>();
            }
            
            if (panelBelow != null)
            {
                panelBelow.UpdatePlaceholderPosition(eventData.position);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject dropTarget = eventData.pointerEnter;
        ProgrammingPanelController panelBelow = null;
        if (dropTarget != null)
        {
            panelBelow = dropTarget.GetComponentInParent<ProgrammingPanelController>();
        }

        if (panelBelow != null)
        {
            int placeholderIndex = panelBelow.GetPlaceholderIndex();
            panelBelow.HidePlaceholder();
            
            DraggedInstance.transform.SetParent(panelBelow.ContentParent);
            DraggedInstance.transform.SetSiblingIndex(placeholderIndex);

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            DraggedInstance.transform.localScale = Vector3.one;
        }
        else
        {
            Destroy(DraggedInstance);
        }

        DraggedInstance = null;
    }
    
    public void Highlight(Color highlightColor) { if(runeImage != null) runeImage.color = highlightColor; }
    public void Unhighlight() { if(runeImage != null) runeImage.color = originalColor; }

    public RuneType GetRuneType() { return runeData.runeType; }
}