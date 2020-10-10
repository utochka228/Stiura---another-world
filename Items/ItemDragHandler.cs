using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform canvas;
    public Transform old;
    public int ownIndex;

    void Start()
    {
        ownIndex = transform.parent.parent.GetComponent<InventorySlot>().index;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        old = transform.parent;
        transform.SetParent(canvas);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == canvas)
        {
            transform.SetParent(old);
            transform.localPosition = Vector3.zero;

        }
    }

    
}
