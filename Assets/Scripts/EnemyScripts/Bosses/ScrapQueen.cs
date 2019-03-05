using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapQueen : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] GameObject crawlSpawn;
    [SerializeField] GameObject flySpawn;
    [SerializeField] Vector3 pivot;
    public float angle = Mathf.PI / 2;
    [SerializeField] float radius = 10;

    bool attacking = false;

    public bool intro;
    [SerializeField] GameObject platform;
    [SerializeField] FaceCheck fc;
    [SerializeField] Transform offSet;
    [SerializeField] Transform spawnMinionsPoint;

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
            if((int)offSet.rotation.eulerAngles.y == 90)
            {
                fc.gameObject.GetComponent<BoxCollider2D>().offset = Vector2.up;
            }
            if ((int)offSet.rotation.eulerAngles.y == 270)
            {
                fc.gameObject.GetComponent<BoxCollider2D>().offset = Vector2.down;
            }

            if (fc.hit && speed < 4)
            {
                speed += 3;
            }
            if (fc.hit == false && speed >= 4)
            {
                speed -= 3;
            }

            if (!attacking && (transform.position - spawnMinionsPoint.position).magnitude < 1f)
            {
                StartCoroutine(SpawnMinions());
            }
            if (attacking == false)
            {
                angle += speed * Time.deltaTime;
                if (angle >= Mathf.PI * 2)
                {
                    angle -= Mathf.PI * 2;
                }
                Vector3 next = new Vector3(pivot.x + (radius * Mathf.Cos(-angle)), pivot.y + (radius * Mathf.Sin(-angle)), 0);
                transform.position = next;
            }

        }
    }

    IEnumerator SpawnMinions()
    {
        attacking = true;
        yield return new WaitForSeconds(2f);
        Instantiate(flySpawn, pivot, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(crawlSpawn, pivot, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(flySpawn, pivot, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        float randoAngle = Random.Range(0, Mathf.PI * 2);
        spawnMinionsPoint.position = new Vector3(pivot.x + (radius * Mathf.Cos(randoAngle)), pivot.y + (radius * Mathf.Sin(randoAngle)), 0);
        attacking = false;
    }

    void Intro()
    {
        //start at top
        //rotate till 5:00ish; 360/12 * 5;
        //x = cx + r * cos(a)
        //y = cy + r * sin(a)

    }
}
