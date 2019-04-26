using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public static BossHealth BH;

    public EnemyCombat Refer;

    [SerializeField] Transform health;
    public float maxHealth = 0;

    private void Awake()
    {
        BH = this;
    }

    void Update()
    {
        if(Refer)
        {
            health.localScale = new Vector3(Refer.health / maxHealth, 1, 1);
        }
    }
}
