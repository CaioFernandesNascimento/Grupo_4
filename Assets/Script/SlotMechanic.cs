using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotMechanic : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //isso impede de colocar item sob item no grid do slot
        if (transform.childCount == 0)
        {
            //dropar item recebe o evento poiter drag
            GameObject droparItem = eventData.pointerDrag;

            //recebe oq ta dropando
            DragAndDrop dragAndDrop = droparItem.GetComponent<DragAndDrop>();

            //transforma em parent (pai/filho)
            dragAndDrop.parentDepoisDoDrag = transform;
        }
    }
}
