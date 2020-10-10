using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    public PostProcessingProfile profile;
    public GameObject Crafting;

    public Text weightUI;
    public Text coinsUI;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        inventoryUI.SetActive(false);

        Cursor.visible = false;

    }
	
	// Update is called once per frame
	void Update () {
        float curWeight = inventory.currentWeight;
        float maxWeight = inventory.maxWeight;

        weightUI.text = "Weight: " + curWeight + "/" + maxWeight;


        float coins = inventory.currentCoins;
        coinsUI.text = coins + " Val'ds";

        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Cursor.visible = !Cursor.visible;
            if (inventoryUI.activeSelf)
                UpdateUI();
        }

        

        if (inventoryUI.activeSelf || Crafting.activeSelf)
            profile.depthOfField.enabled = true;
        else if(!(inventoryUI.activeSelf && Crafting.activeSelf))
            profile.depthOfField.enabled = false;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);


            } else
            {
                slots[i].ClearSlot();
            }

            slots[i].index = i;
        }
    }
    
    public void CloseSecretLetter()
    {
        GameObject obj = transform.Find("SecretTip").gameObject;
        obj.SetActive(false);
    }
}
