using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interact(Transform player)
    {
        base.Interact(player);

        PickUp(player);
        
    }

    void PickUp(Transform _player)
    {
        Debug.Log("PickingUp: " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        Transform canvas = Inventory.instance.canvas;
        GameObject obj = canvas.Find("UIPref").gameObject;
        obj.SetActive(false);



        if (wasPickedUp)
        {
            //_player.GetComponent<NetCommands>().CmdDestroyNetworkObject(gameObject);
            Destroy(gameObject);
        }
    }

}
