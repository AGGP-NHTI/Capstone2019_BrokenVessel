using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvanceMovement : MonoBehaviour
{

    public bool seePlayer = false;

    [SerializeField] bool targetIsRadius = false;
    public float speed = 5;
    public float offSetX = 0;
    public float offSetY = 2;
    public float offSetZ = 0;

    public bool attacking = false;
    int timer = 0;
    [SerializeField] int timerMax = 10;

    public Transform homePlate;
    Transform player;
    Vector3 lastPlayerPosition = Vector3.zero;
    Vector3 target = Vector3.one * 5;
    [SerializeField] Transform pivot;
    [SerializeField] Transform orbitPoint;

    Rigidbody2D rig;

    void Start()
    {
        player = transform;
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(seePlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
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
        pivot.Rotate(Vector3.forward, 180 * Time.fixedDeltaTime);

        if (!attacking)
        {
            timer++;
            rig.velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, orbitPoint.position, speed * Time.fixedDeltaTime);
            if (timer >= timerMax && seePlayer)
            {
                timer = 0;
                attacking = true;
                lastPlayerPosition = player.position;
                
                rig.velocity = Vector2.zero;
            }
        }
        if (attacking)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        timer++;
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        rig.AddForce((target - transform.position).normalized * speed * 100 * Time.fixedDeltaTime);
        if (timer >= timerMax / 2)
        {
            timer = 0;
            attacking = false;
            rig.velocity = Vector2.zero;
            if (Vector3.Distance(player.position, orbitPoint.position) < Vector3.Distance(transform.position, orbitPoint.position))
            {
                homePlate.position = transform.position;
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
