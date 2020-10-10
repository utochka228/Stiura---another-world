using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreviewObject : MonoBehaviour {

    public objectsorts sorts;
    public List<Collider> col = new List<Collider>();

    public Material green;
    public Material red;

    public bool IsBuildable;
    public bool second;

    public PreviewObject childcol;

    public Transform graphics;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            col.Add(other);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            col.Remove(other);
        }
    }

    void Update()
    {
        if(!second)
            ChangeColor();
    }

    public void ChangeColor()
    {

        if(sorts == objectsorts.foundation)
        {
            if (col.Count == 0)
                IsBuildable = true;
            else
                IsBuildable = false;
        }
        else
        {
            if (col.Count == 0 && childcol.col.Count > 0)
                IsBuildable = true;
            else
                IsBuildable = false;
        }

        if (IsBuildable)
        {
            foreach (Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = green;
            }
        }
        else
        {
            foreach (Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = red;
            }
        }
    }
}

public enum objectsorts { normal, foundation, floor}