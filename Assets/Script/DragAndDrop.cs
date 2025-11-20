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

    public Transform executionGrid;

    public static List<GameObject> sequenciaExecucao = new List<GameObject>();

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



        // Checar se o objeto está em cima da grid de execução
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var r in results)
        {
            // coloca como filho da grid
            transform.SetParent(r.gameObject.transform, false);

            // ADICIONA O BLOCO À SEQUÊNCIA
            if (!sequenciaExecucao.Contains(gameObject))
                sequenciaExecucao.Add(gameObject);

            return;
        }


        // Se não estiver, volta para o lugar original
        transform.SetParent(parentDepoisDoDrag);

    }

       
    
}
