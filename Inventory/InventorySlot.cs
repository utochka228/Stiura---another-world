using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;          // Reference to the Icon image
    public Button removeButton; // Reference to the remove button

    Item item;  // Current item in the slot

    public int stackAmount;
    private float currentWeight;

    public GameObject toolTip;
    public Text stack;

    public int index;

    public GameObject infoPanel;
    void Update()
    {
        stack.text = stackAmount.ToString();

        if (item != null)
        {
            if (!item.canHaveStack)
                ClearStack();
            else
            {
                stack.enabled = true;
            }
        }
    }

    // Add item to the slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        
    }
    public void ClearStack()
    {
        stack.enabled = false;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        stack.enabled = false;

        Inventory.instance.currentWeight -= currentWeight;
    }

    // Called when the remove button is pressed
    public void OnRemoveButton()
    {
        Inventory.instance.RemoveToWorld(item);
    }

    // Called when the item is pressed
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
    public void EatItem()
    {
        if (item != null)
        {
            item.EatItem();
        }
    }
    public void DestroyItem()
    {
        if(item != null)
        {
            item.DestroyItem();
            Inventory.instance.items.Remove(item);

            Inventory.instance.currentWeight -= item.weight;

            if (Inventory.instance.onItemChangedCallback != null)
                Inventory.instance.onItemChangedCallback.Invoke();

        }
    }


    public void ShowToolTip()
    {
        if(item != null)
        {
            toolTip.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(UseItem);
            toolTip.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(EatItem);
            if (item.isFood)
            {
                toolTip.transform.GetChild(5).GetComponent<Button>().interactable = true;
            }
            else
            {
                toolTip.transform.GetChild(5).GetComponent<Button>().interactable = false;
            }

            toolTip.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(DestroyItem);
            toolTip.transform.GetChild(1).GetComponent<Image>().sprite = item.icon;
            toolTip.transform.GetChild(1).GetComponent<Image>().enabled = true;

            toolTip.transform.GetChild(2).GetComponent<Text>().text = item.description;
            toolTip.transform.GetChild(3).GetComponent<Text>().text = item.name;
        }
    }
    
}
