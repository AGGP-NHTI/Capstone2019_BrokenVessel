using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : BrokenVessel.Actor.Actor
{

    [SerializeField] Transform weapon;
    [SerializeField] float meleeCD = 1f;
    [SerializeField] float attackRange = 3f;

    public bool attacking = false;
    float meleeTimer = 0;

    EnemyCombat ec;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(paused) { return; }
        if (!attacking)
        {
            meleeTimer -= Time.deltaTime;
        }
        if(ec.seePlayer && meleeTimer <= 0 && ec.playerDistance < attackRange && ec.playerDistance != -1f)
        {
            attacking = true;

            meleeTimer = meleeCD;
            StartCoroutine(meleeAttack());
        }

    }

    IEnumerator meleeAttack()
    {
        weapon.transform.rotation = Quaternion.Euler(0, 0, 45);
        yield return new WaitForSeconds(2);
        weapon.transform.rotation = Quaternion.Euler(0, 0, -90);
        Debug.Log("melee attack");
        //damage trigger = true
        yield return new WaitForSeconds(2);
        weapon.transform.rotation = Quaternion.identity;
        attacking = false;
    }
}
