using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    public Transform interactionTransform;

    public Transform player;

    bool coolDown;

    CharacterAnimator anim;

    public float radius;
    private float distance;

    public virtual void Interact(Transform player)
    {
        if(transform.tag == "Tree" && coolDown == false)
        {
            player.GetComponent<Animator>().SetBool("Chop", true);     
            StartCoroutine(CoolDown());
            coolDown = true;

        }
        else
        {
            player.GetComponent<Animator>().SetBool("PickUp", true);
            anim.TimeForEndPickUp();
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.7f);
        player.GetComponent<Animator>().SetBool("Chop", false);
        GetComponent<Tree>().Health -= 5;
        GetComponent<Tree>().InteractWithTree();
        coolDown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && GetComponent<ItemEquiped>().isEquiped == false)
        {
            Transform canvas = Inventory.instance.canvas;
            GameObject obj = canvas.Find("UIPref").gameObject;
            obj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Transform canvas = Inventory.instance.canvas;
            GameObject obj = canvas.Find("UIPref").gameObject;
            obj.SetActive(false);
        }
    }


    void Update()
    {
        if(player != null)
        {
            distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact(player);
                }

                if (GetComponent<ItemEquiped>().isEquiped == false)
                    GetComponent<Outline>().enabled = true;
                else
                    GetComponent<Outline>().enabled = false;

            }
            else
            {
                if (GetComponent<ItemEquiped>().isEquiped == false)
                    GetComponent<Outline>().enabled = false;
            }

            if (player != null)
                anim = player.GetComponent<CharacterAnimator>();


        }
        if(player == null)
        {
            if (Inventory.instance != null)
                player = Inventory.instance.player;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
