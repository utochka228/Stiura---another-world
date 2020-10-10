using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buildingsystem : MonoBehaviour {
    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects currentObject;

    private Vector3 currentPos;
    private Vector3 currentRot;

    public Transform currentPreview;
    public Camera cam;

    public RaycastHit hit;
    public LayerMask layer;

    public float offset = 1f;
    public float gridSize = 1f;

    public bool IsBuilding;

    public MCFace dir;

    public GameObject objectMenuobj;

    private bool chooseobj;

    void Start()
    {
        cam = Camera.main;
        currentObject = objects[0];
        ChangeCurrentBuilding(0);
        IsBuilding = false;
    }

    void Update()
    {
        if(IsBuilding)
        {
            if (IsBuilding && !chooseobj)
                StartPreview();
            if (Input.GetMouseButtonDown(1) && !chooseobj)
                Build();
            
            if (Input.GetKeyDown(KeyCode.Q) && IsBuilding)
            {
                Destroy(currentPreview.gameObject);
                IsBuilding = false;
            }

        }
        if (Input.GetButton("showMenu") && !objectMenuobj.activeSelf)
        {
            objectMenuobj.SetActive(true);
            chooseobj = true;
        }
        if (!Input.GetButton("showMenu") && objectMenuobj.activeSelf)
            turnOffTheMenu();
    }

    public void turnOffTheMenu()
    {
        objectMenuobj.SetActive(false);
        chooseobj = false;
    }

    public void ChangeCurrentBuilding(int cur)
    {
        currentObject = objects[cur];

        if (currentPreview != null)
            Destroy(currentPreview.gameObject);

        GameObject curprev = Instantiate(currentObject.preview, currentPos, Quaternion.Euler(currentRot)) as GameObject;
        currentPreview = curprev.transform;
    }

    public void StartPreview()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 20f, layer))
        {
            if(hit.transform != this.transform)
            {
                showPreview(hit);
            }
        }
    }

    public void showPreview(RaycastHit hit2)
    {
        if(currentObject.sort == objectsorts.floor)
        {
            dir = GetHitFace(hit2);

            if (dir == MCFace.Up || dir == MCFace.Down)
            {
                currentPos = hit2.point;
            } else
            {
                if(dir == MCFace.North)
                    currentPos = hit2.point + new Vector3(0, 0, 2);

                if (dir == MCFace.South)
                    currentPos = hit2.point + new Vector3(0, 0, -2);

                if (dir == MCFace.East)
                    currentPos = hit2.point + new Vector3(2, 0, 0);

                if (dir == MCFace.West)
                    currentPos = hit2.point + new Vector3(-2, 0, 0);
            }
        }
        else
            currentPos = hit2.point;
        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentPreview.position = currentPos;

        if(Input.GetKeyDown(KeyCode.R))
            currentRot += new Vector3(0, 90, 0);
        currentPreview.localEulerAngles = currentRot;
    }

    public void Build()
    {
        PreviewObject PO = currentPreview.GetComponent<PreviewObject>();
        if(PO.IsBuildable)
        {
            Instantiate(currentObject.prefab, currentPos, Quaternion.Euler(currentRot));
            IsBuilding = false;
            Destroy(currentPreview.gameObject);
        }
    }

    public static MCFace GetHitFace(RaycastHit hit)
    {
        Vector3 incomingVec = hit.normal - Vector3.up;
        if (incomingVec == new Vector3(0, -1, -1))
            return MCFace.South;
        if (incomingVec == new Vector3(0, -1, 1))
            return MCFace.North;
        if (incomingVec == new Vector3(0, 0, 0))
            return MCFace.Up;
        if (incomingVec == new Vector3(1, 1, 1))
            return MCFace.Down;
        if (incomingVec == new Vector3(-1, 1, 0))
            return MCFace.West;
        if (incomingVec == new Vector3(1, -1, 0))
            return MCFace.East;
        return MCFace.None;
    }
}

[System.Serializable]
public class buildObjects {
    public string name;
    public GameObject prefab;
    public GameObject preview;
    public objectsorts sort;
    public int gold;
}

public enum MCFace { None, Up, Down, East, West, North, South}
