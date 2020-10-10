using UnityEngine;
using System.Collections;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public enum MeshBlendShape { Torso, Arms, Legs };
    public Equipment[] defaultEquipment;

    public static EquipmentManager instance;
    public SkinnedMeshRenderer targetMesh;

    [Header("Player slots of equipment")]
    public Transform leftHand;
    public Transform rightHand;
    public Transform Chest;
    public Transform Pants;
    public Transform Shoes;
    public Transform Head;

    Transform player;
    EquipmentUI equipmentUI;

    SkinnedMeshRenderer[] currentMeshes;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] currentEquipment;   // Items we currently have equipped

    // Callback for when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    Inventory inventory;    // Reference to our inventory

    void Start()
    {
        equipmentUI = EquipmentUI.instance;
        inventory = Inventory.instance;     // Get a reference to our inventory

        // Initialize currentEquipment based on number of equipment slots
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaults();
    }

    // Equip a new item

    public void Equip(Equipment newItem)
    {
        // Find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);

        inventory.items.Remove(newItem);

        //Updating equipmentUI
        equipmentUI.UpdateEquipment();

        if (inventory.onItemChangedCallback != null)
            inventory.onItemChangedCallback.Invoke();

    }

    // Unequip an item with a particular index
    public Equipment Unequip(int slotIndex)
    {
        Equipment oldItem = null;
        // Only do this if an item is there
        if (currentEquipment[slotIndex] != null)
        {
            // Add the item to the inventory
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            SetBlendShapeWeight(oldItem, 0);
            // Destroy the mesh
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            // Remove the item from the equipment array
            currentEquipment[slotIndex] = null;

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        //Updating equipmentUI
        equipmentUI.UpdateEquipment();

        return oldItem;
    }

    // Unequip all items
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        //Updating equipmentUI
        equipmentUI.UpdateEquipment();

        EquipDefaults();
    }

    void AttachToMesh(Equipment item, int slotIndex)
    {
        
        GameObject newMesh = Instantiate(item.prefab);

        if ((int)item.equipSlot == 0)//Head, Chest, Pants, Shoes, LeftHand, RightHand
        {
            newMesh.transform.parent = Head;
            
            //if (item.name == "Torch")
             //   newMesh.transform.GetComponent<Light>().enabled = true;
        }
        if ((int)item.equipSlot == 1)//Head, Chest, Pants, Shoes, LeftHand, RightHand
        {
            newMesh.transform.parent = Chest;
        }

        if ((int)item.equipSlot == 2) //Head, Chest, Pants, Shoes, LeftHand, RightHand
        {
            newMesh.transform.parent = Pants;
        }

        if ((int)item.equipSlot == 3)//Head, Chest, Pants, Shoes, LeftHand, RightHand
        {
            newMesh.transform.parent = Shoes;
        }

        if ((int)item.equipSlot == 4)//Head, Chest, Pants, Shoes, LeftHand, RightHand
        {
            newMesh.transform.parent = leftHand;

            //if (item.itemTag == "Torch")
           // {
           //     newMesh.GetComponent<LightOrigin>().mustBurn = true;
            //}
        }

        if ((int)item.equipSlot == 5)//Head, Chest, Pants, Shoes, LeftHand, RightHand
        {
            newMesh.transform.parent = rightHand;
        }

        newMesh.transform.localPosition = item.LocalPos;
        newMesh.transform.localRotation = Quaternion.Euler(item.LocalRot);
        newMesh.transform.GetComponent<Rigidbody>().isKinematic = true;
        newMesh.transform.GetComponent<Collider>().isTrigger = true;

        newMesh.transform.GetComponent<ItemEquiped>().isEquiped = true;

        if (newMesh.transform.GetComponent<ItemPickup>() != null)
            newMesh.transform.GetComponent<ItemPickup>().enabled = false;
    }

    void SetBlendShapeWeight(Equipment item, int weight)
    {
        foreach (MeshBlendShape blendshape in item.coveredMeshRegions)
        {
            int shapeIndex = (int)blendshape;
            targetMesh.SetBlendShapeWeight(shapeIndex, weight);
        }
    }

    void EquipDefaults()
    {
        foreach (Equipment e in defaultEquipment)
        {
            Equip(e);
        }
    }

    void Update()
    {
        player = Inventory.instance.player;

        EquipmentInfo info = player.GetComponent<EquipmentInfo>();
        //For equipment slots

        leftHand = info.LeftHandSlot;
        rightHand = info.RightHandSlot;
        Chest = info.Chest;
        Pants = info.Pants;
        Shoes = info.Shoes;
        Head = info.Head;


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (player.GetComponent<CharacterAnimator>().LeftHandItemIs == "Torch")
            {
                if (leftHand.GetChild(0).GetComponent<LightOrigin>().mustBurn == false)
                {
                    player.GetComponent<CharacterAnimator>().SetBoolFireTorchTrue(leftHand.GetChild(0).GetComponent<LightOrigin>());
                }
            }
        }
    }

}
