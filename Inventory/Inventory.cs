using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    public GameObject itemUIPrefab;
    public Transform canvas;
    public Transform tookItemSlot;

    void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    public Transform player;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public float maxWeight = 20f;

    public float currentWeight = 0f;

    public float currentCoins = 0f;

    public List<Item> items = new List<Item>();

    void Start()
    {
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    void Update()
    {
        if(canvas == null)
            canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough of space!");
                return false;
            }
            if(currentWeight >= maxWeight)
            {
                Debug.Log("You are overwhelmed!");
                return false;
            }

            items.Add(item);
            Debug.Log(item + "added");
            GameObject ip = Instantiate(itemUIPrefab);
            ip.transform.GetChild(0).GetComponent<Text>().text = item.name;
            ip.transform.parent = tookItemSlot;     

            currentWeight += item.weight;

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void RemoveToWorld(Item item)
    {
        player.GetComponent<NetCommands>().CmdSpawnInvObject(item.prefab, player.gameObject);

        items.Remove(item);
        currentWeight -= item.weight;

        if (currentWeight <= 0)
            currentWeight = 0;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        currentWeight -= item.weight;

        if (currentWeight <= 0)
            currentWeight = 0;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void UpdateCallback()
    {
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
