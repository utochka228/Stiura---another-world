using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : CharacterStats {

    private Image HealthPool;
    private Image StaminaPool;
    private Image HungryPool;

    //Sitost' personaja
    public bool satiety;

    public GameObject CanvasInst;

    private float timer3;

    //Poison
    public float duration;
    public int poison_damage;
    public bool playerIsPoisoned;
    public float poisonPerTime;

    // Use this for initialization
    void Start() {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        CanvasInst = GameObject.FindGameObjectWithTag("Canvas").gameObject;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        
        

    }

    void Update()
    {
        if(CanvasInst == null)
        {
            CanvasInst = GameObject.FindGameObjectWithTag("Canvas").gameObject;
        }

        //HealthPool UI reference
        HealthPool = CanvasInst.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>();
        HealthPool.fillAmount = (float)currentHealth / 100;

        //HungryPool UI reference
        HungryPool = CanvasInst.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<Image>();
        HungryPool.fillAmount = (float)currentHungry / 100;

        //StaminaPool UI reference
        StaminaPool = CanvasInst.transform.GetChild(0).GetChild(12).GetChild(1).GetComponent<Image>();
        StaminaPool.fillAmount = (float)currentStamina / 100;

        //Baf na sitost' (20sec)
        if (satiety)
        {
            timer3 += Time.deltaTime;
            if(timer3 >= 20f)
            {
                timer3 = 0;
                satiety = false;
            }
        }

        //Esli personaj ne sitiy, to 
        if(!satiety)
            TakeHungry(1, 8f, 1);

        if(playerIsPoisoned)
            TakePoison(duration, poison_damage, poisonPerTime);

        if (GetComponent<CharacterAnimator>().run == false)
            StaminaRegeneration(2, 1f);
        else
            TakeStamina(2, 0.5f);
    }

    
}
