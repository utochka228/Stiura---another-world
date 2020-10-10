using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public int maxHungry = 100;
    public int currentHungry;

    public int maxStamina = 100;
    public int currentStamina;

    public Stat damage;
    public Stat armor;

    //Timers for delays
    private float timer;
    private float timer2;
    private float timer3;
    private float timer4;
    private float timer5 = 0f;
    private float timer6 = 0f;

    void Awake()
    {
        currentHealth = maxHealth;
        currentHungry = maxHungry;
        currentStamina = maxStamina;
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damages");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeHungry(int hungryCount, float timeToHungry, int multiplier)
    {
        timer += Time.deltaTime;

        if(timer >= timeToHungry)
        {
            currentHungry -= hungryCount * multiplier;
            timer = 0;
        }

        if (currentHungry <= 0)
        {
            currentHungry = 0;

            timer2 += Time.deltaTime;
            if(timer2 >= 3f)
            {
                TakeDamage(2);
                timer2 = 0;
            }
        }
    }

    public void SetHungry(int amount)
    {
        currentHungry += amount;
    }

    public void TakePoison(float duration, int damage, float perVar)
    {
        timer3 += Time.deltaTime;
        timer4 += Time.deltaTime;

        if (timer4 >= perVar)
        {
            TakeDamage(damage);
            timer4 = 0;
        }

        if (timer3 >= duration)
        {
            GetComponent<PlayerStats>().playerIsPoisoned = false;
            timer3 = 0;
            timer4 = 0;
        }
    }

    public void StaminaRegeneration(int countStamina, float delayTime)
    {
        timer5 += Time.deltaTime;
        if(timer5 >= delayTime)
        {
            currentStamina += countStamina;
            timer5 = 0f;
        }
    }
    public void TakeStamina(int countStamina, float delayTime)
    {
        timer6 += Time.deltaTime;
        if (timer6 >= delayTime)
        {
            currentStamina -= countStamina;
            timer6 = 0f;
        }
    }

    public virtual void Die()
    {
        GameObject canvas = Inventory.instance.canvas.gameObject;
        GameObject camera = CameraHandler.instance.gameObject;
        Destroy(canvas);
        Destroy(camera);
        SceneManager.LoadScene("DieScene");
        Destroy(gameObject);
    }

    
}
