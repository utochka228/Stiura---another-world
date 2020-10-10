using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public float weight = 0;

    public Sprite icon = null;

    public bool isFood;
    public bool canHaveStack;
    public bool isDefaultItem = false;
    public bool isBerry;

    public GameObject prefab;

    public float hungryLevel;
    public bool isPoisoned;
    public float poisonDuration;
    public int poisonDamage;
    public float poisonPerTime;
    [Range(0,1f)]
    public float freshenness;

    public float cost;

    public string description;

    public string itemTag;

    void Start()
    {
    }

    public virtual void Use()
    {
        Debug.Log("Use " + name);
        if(name == "Письмо-подсказка")
        {
            Transform canvas = Inventory.instance.canvas;
            GameObject obj = canvas.Find("SecretTip").gameObject;
            obj.SetActive(true);
        }
        
    }
    public virtual void EatItem()
    {
        Transform player = Inventory.instance.player;
        PlayerStats stats = player.GetComponent<PlayerStats>();

        stats.SetHungry((int)(hungryLevel * freshenness));

        if (isPoisoned)
        {
            stats.playerIsPoisoned = true;
            stats.duration = poisonDuration;
            stats.poison_damage = poisonDamage;
            stats.poisonPerTime = poisonPerTime;
        }

        Inventory.instance.Remove(this);
    }
    public virtual void DestroyItem()
    {
        Debug.Log("Destroy " + name);
    }
    public virtual void GetItemInfo()
    {

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
