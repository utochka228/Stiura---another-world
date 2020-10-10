using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyStats : CharacterStats {

    public Image HealthPool;

    void Update()
    {
        HealthPool.fillAmount = (float)currentHealth / 100;
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
