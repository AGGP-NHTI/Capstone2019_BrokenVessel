using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour {

    [SerializeField] Transform weapon;
    [SerializeField] GameObject projectile;
    [SerializeField] float damage = 10f;
    [SerializeField] float rangeCD = .5f;
    [SerializeField] float attackRange = 3f;

    public bool attacking = false;
    float rangeTimer = 0;

    EnemyCombat ec;

    void Start()
    {
        ec = GetComponent<EnemyCombat>();
    }

    void Update()
    {
        if (!attacking)
        {
            rangeTimer -= Time.deltaTime;
        }
        if (ec.seePlayer && rangeTimer <= 0 && ec.playerDistance < attackRange && ec.playerDistance != -1f)
        {
            attacking = true;
            Debug.Log("pew");
            rangeTimer = rangeCD;
            StartCoroutine(rangeAttack());
        }
    }

    IEnumerator rangeAttack()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(projectile, weapon.transform.position, weapon.transform.rotation);
        yield return new WaitForSeconds(.5f);
        attacking = false;
    }
}
