using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvanceMovement : MonoBehaviour
{
    [SerializeField] bool targetIsRadius = false;
    public float speed = 5;
    [SerializeField] Vector3 offSet = new Vector3(0, 2, 0);

    public bool attacking = false;
    float timer = 0;


    Transform player;
    Vector3 lastPlayerPosition = Vector3.zero;
    Vector3 target = Vector3.one * 5;

    Rigidbody2D rig;

    EnemyCombat ec;

    void Start()
    {
        player = transform;
        ec = GetComponentInParent<EnemyCombat>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!ec.dead)
        {
            timer -= Time.deltaTime;
            if (ec.seePlayer)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                GetComponentsInChildren<LookAt>()[0].FocusObject = player;
                GetComponentsInChildren<LookAt>()[1].FocusObject = player;
            }
            else
            {
                player = transform;
                GetComponentsInChildren<LookAt>()[0].FocusObject = null;
                GetComponentsInChildren<LookAt>()[1].FocusObject = null;
            }
            if (targetIsRadius)
            {
                target = (transform.position - lastPlayerPosition).normalized * offSet.x;
                target.y = Mathf.Abs(target.y);
                target += lastPlayerPosition;
                target.y += offSet.y;
            }
            else
            {
                target = lastPlayerPosition;
                target.x += offSet.x;
                target.y += offSet.y;
                target.z = offSet.z;
            }

            if (attacking && ec.contactEnemy)
            {
                ChargeAttack();
            }
            if (!attacking && ec.seePlayer)
            {
                rig.velocity = (transform.position - target).normalized * speed;
                if (timer <= 0)
                {
                    timer = 5;
                    attacking = true;
                    lastPlayerPosition = player.position;
                }
            }
        }
    }

    public virtual void ChargeAttack()
    {
        if (Vector3.Distance(target, transform.position) > 1f)
        {
            rig.velocity = (target - transform.position).normalized * speed * 2;
        }
        if (timer <= 1 || Vector3.Distance(target, transform.position) <= 1f)
        {
            rig.velocity = (transform.position - player.position).normalized * speed / 3;
        }
        if(timer <= 0)
        {
            attacking = false;
        }
    }
}
