using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;
    public int damageModifier;

    public Vector3 LocalPos;
    public Vector3 LocalRot;

    public override void Use()
    {
        EquipmentManager.instance.Equip(this);
        //RemoveFromInventory();
    }

}

public enum EquipmentSlot { Head, Chest, Pants, Shoes, LeftHand, RightHand }
public enum EquipmentMeshRegion { Legs, Arms, Torso}
