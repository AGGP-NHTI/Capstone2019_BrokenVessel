using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : BrokenVessel.Actor.Actor
{

    [SerializeField] Transform weapon;
    [SerializeField] GameObject projectile;
    [SerializeField] float damage = 10f;
    [SerializeField] float rangeCD = .5f;
    [SerializeField] float attackRange = 3f;

    [SerializeField] float speed = 5;
    [SerializeField] Vector2 movement = Vector2.one;
    [SerializeField] bool shootByX = false;


    public bool attacking = false;
    float rangeTimer = 0;

    EnemyCombat ec;
    GameObject refer;

    void Start()
    {
        ec = GetComponent<EnemyCombat>();
        UpdateProjectile();
    }

    void UpdateProjectile()
    {
        refer = projectile;
        refer.GetComponent<Projectile>().speed = speed;
        refer.GetComponentInChildren<DamageTrigger>().OwnerLayer = gameObject.layer;
        refer.GetComponentInChildren<DamageTrigger>().damage = damage;
    }

    void Update()
    {
        if (paused) { return; }
        if (!attacking)
        {
            rangeTimer -= Time.deltaTime;
        }
        if (ec.seePlayer && rangeTimer <= 0 && ec.playerDistance < attackRange && ec.playerDistance != -1f)
        {
            attacking = true;
            rangeTimer = rangeCD;
            StartCoroutine(rangeAttack());
        }
    }

    IEnumerator rangeAttack()
    {
        yield return new WaitForSeconds(.5f);
        refer = Instantiate(projectile, weapon.transform.position, weapon.transform.rotation) as GameObject;
        refer.GetComponent<Projectile>().movement = movement * transform.localScale;
        yield return new WaitForSeconds(.5f);
        attacking = false;
    }
}
