using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //a imagem que estamos usando
    public Image image;

    //variavel para receber se é filho ou pai (para evitar de ficar atras da grid)
    public Transform parentDepoisDoDrag;


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("pegando item");

        //para alocar o item no slot precisa colocar ele como ultimo elemento da tela
        parentDepoisDoDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        //para evitar do mouse nao achar a grid atras do item
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("levando item");

        //loca o item no mouse position
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("soltando item");

        //no final coloca o item de volta no parente q ele eh (pai ou filho)
        transform.SetParent(parentDepoisDoDrag);

        //volta a ficar visivel pro mouse
        image.raycastTarget = true;
    }
}
