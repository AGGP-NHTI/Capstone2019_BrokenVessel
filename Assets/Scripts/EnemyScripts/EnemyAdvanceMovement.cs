using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvanceMovement : MonoBehaviour
{
    [SerializeField] bool targetIsRadius = false;
    public float speed = 5;
    public float offSetX = 0;
    public float offSetY = 2;
    public float offSetZ = 0;

    public bool attacking = false;
    int timer = 0;
    [SerializeField] int timerMax = 10;

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
        if(ec.seePlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            GetComponentInChildren<LookAt>().FocusObject = player;
        }
        else
        {
            player = transform;
            GetComponentInChildren<LookAt>().FocusObject = null;
        }
        if (targetIsRadius)
        {
            target = (transform.position - lastPlayerPosition).normalized * offSetX;
            target.y = Mathf.Abs(target.y);
            target += lastPlayerPosition;
            target.y += offSetY;
        }
        else
        {
            target = lastPlayerPosition;
            target.x += offSetX;
            target.y += offSetY;
            target.z = offSetZ;
        }

        if (attacking)
        {
            Attack();
        }
        if (!attacking)
        {
            timer++;
            rig.velocity = -(transform.position - target).normalized * speed / 2 * Time.fixedDeltaTime;
            if (timer >= timerMax && ec.seePlayer)
            {
                timer = 0;
                attacking = true;
                lastPlayerPosition = player.position;
            }
        }
    }

    public virtual void Attack()
    {
        timer++;
        if (Vector3.Distance(target, transform.position) > 1)
        {
            rig.velocity = (target - transform.position).normalized * speed * 100 * Time.fixedDeltaTime;
        }
        if (timer >= timerMax / 2)
        {
            timer = 0;
            attacking = false;
        }
    }
}
