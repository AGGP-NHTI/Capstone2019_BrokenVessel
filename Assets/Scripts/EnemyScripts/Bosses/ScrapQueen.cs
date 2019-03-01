using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapQueen : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] GameObject crawlSpawn;
    [SerializeField] GameObject flySpawn;
    [SerializeField] Vector3 pivot;
    public float angle = Mathf.PI;
    [SerializeField] float radius = 10;

    bool seePlayer = false;
    bool attacking = false;

    public bool intro;
    [SerializeField] GameObject platform;

    Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        intro = true;
        pivot = transform.position;
        pivot.y -= radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (intro)
        {
            Intro();
        }
        else
        {
            if (seePlayer && speed == 5)
            {
                speed += 3;
            }
            else if (speed == 8)
            {
                speed -= 3;
            }

            if(transform.position.y - pivot.y < .0005f && speed != 0)
            {

                    StartCoroutine(SpawnMinions());

            }

                angle += speed * Time.deltaTime;
                if (angle >= Mathf.PI * 2)
                {
                    angle -= Mathf.PI * 2;
                }
                Vector3 next = new Vector3(pivot.x + (radius * Mathf.Cos(angle)), pivot.y + (radius * Mathf.Sin(angle)), 0);
                transform.position = next;

        }
    }

    IEnumerator SpawnMinions()
    {
        speed = 0;
        yield return new WaitForSeconds(2f);
        Instantiate(flySpawn, pivot, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(crawlSpawn, pivot, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(flySpawn, pivot, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        angle += .01f;
        speed = 2;
    }

    void Intro()
    {
        //start at top
        //rotate till 5:00ish; 360/12 * 5;
        //x = cx + r * cos(a)
        //y = cy + r * sin(a)

    }
}
