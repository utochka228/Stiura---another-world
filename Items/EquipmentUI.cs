using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {
    public static EquipmentUI instance;

    EquipmentManager manager;

    Equipment[] equipment = new Equipment[6];

    public Image[] icons;

    private int countOfSlots;

    public GameObject equipObj;

    void Awake()
    {
        instance = this;
        manager = EquipmentManager.instance;
    }

    // Use this for initialization
    void Start () {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        countOfSlots = numSlots;

        equipObj.SetActive(false);

        UpdateEquipment();
    }
	
	// Update is called once per frame
	void Update () {
        if(manager == null)
        {
            manager = EquipmentManager.instance;
        }

        if (Input.GetButtonDown("Equipment"))
        {
            UpdateEquipment();
            equipObj.SetActive(!equipObj.activeSelf);
        }
    }

    public void UpdateEquipment()
    {
        for (int i = 0; i < countOfSlots; i++)
        {
            equipment[i] = manager.currentEquipment[i];

            if (equipment[i] != null)
            {
                icons[i].sprite = equipment[i].icon;
                icons[i].enabled = true;
            }
            else
                icons[i].enabled = false;
        }

    }
}
