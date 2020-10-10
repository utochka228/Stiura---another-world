using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    Inventory inventory;

    public int dropIndex;

    public InventoryUI invUI;

    void Start()
    {
        dropIndex = GetComponent<InventorySlot>().index;
    }

    void Update()
    {
        inventory = Inventory.instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler drag = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if(drag != null)
        {
            if(transform.GetChild(0).childCount > 0)
            {
                Transform s = transform.GetChild(0).GetChild(0);
                s.SetParent(drag.old);
                s.localPosition = Vector3.zero;
                var buf = inventory.items[drag.ownIndex];
                inventory.items[drag.ownIndex] = inventory.items[dropIndex];
                inventory.items[dropIndex] = buf;
                invUI.UpdateUI();
            }
            drag.transform.SetParent(transform.GetChild(0));
            drag.transform.localPosition = Vector3.zero;
            

            
            inventory.UpdateCallback();
        }
    }
}
