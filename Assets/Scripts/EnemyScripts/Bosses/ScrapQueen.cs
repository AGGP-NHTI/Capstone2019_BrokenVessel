using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapQueen : BrokenVessel.Actor.Actor
{
    public bool start = false;

    [SerializeField] float speed = 2;
    [SerializeField] GameObject crawlSpawn;
    [SerializeField] GameObject flySpawn;
    [SerializeField] Vector3 pivot;
    public float angle = -Mathf.PI / 2;
    [SerializeField] float radius = 10;

    bool attacking = false;

    public bool intro;
    float timer = 1f;
    [SerializeField] GameObject platform;
    [SerializeField] FaceCheck fc;
    [SerializeField] Transform offSet;
    [SerializeField] Transform spawnMinionsPoint;

    List<GameObject> FlyingSpawns = new List<GameObject>();
    List<GameObject> CrawlingSpawns = new List<GameObject>();
    int flyingSpawnCap = 8;
    int crawlerSpawnCap = 20;
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
        if (paused) { return; }
        if (start)
        {
            if (intro)
            {
                //StartCoroutine(Intro());
                Intro();
            }
            else
            {
                if ((int)offSet.rotation.eulerAngles.y == 90)
                {
                    fc.gameObject.GetComponent<BoxCollider2D>().offset = Vector2.up;
                }
                if ((int)offSet.rotation.eulerAngles.y == 270)
                {
                    fc.gameObject.GetComponent<BoxCollider2D>().offset = Vector2.down;
                }

                if (fc.hit && speed < 2)
                {
                    speed += 1;
                }
                if (fc.hit == false && speed >= 2)
                {
                    speed -= 1;
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
                    if (angle >= Mathf.PI && transform.localScale.x > 0)
                    {
                        Vector3 theScale = transform.localScale;
                        theScale.x *= -1;
                        transform.localScale = theScale;
                    }
                    else if (angle >= 0 && angle < Mathf.PI && transform.localScale.x < 0)
                    {
                        Vector3 theScale = transform.localScale;
                        theScale.x *= -1;
                        transform.localScale = theScale;
                    }
                    Vector3 next = new Vector3(pivot.x + (radius * Mathf.Cos(-angle)), pivot.y + (radius * Mathf.Sin(-angle)), 4);
                    transform.position = next;
                }

            }
        }
    }

    IEnumerator SpawnMinions()
    {
        attacking = true;
        yield return new WaitForSeconds(4f);
        foreach(GameObject enemy in FlyingSpawns) //check for dead enemies in the list
        {
            if (enemy == null) FlyingSpawns.Remove(enemy);
        }
        foreach (GameObject enemy in CrawlingSpawns) //check for dead enemies in the list
        {
            if (enemy == null) CrawlingSpawns.Remove(enemy);
        }
        if (FlyingSpawns.Count < flyingSpawnCap)
        {
            FlyingSpawns.Add(Instantiate(flySpawn, spawnMinionsPoint.position, Quaternion.identity));
        }
        if (CrawlingSpawns.Count < crawlerSpawnCap)
        {
            yield return new WaitForSeconds(.5f);
            CrawlingSpawns.Add(Instantiate(crawlSpawn, spawnMinionsPoint.position, Quaternion.identity));
        }
        if (FlyingSpawns.Count < flyingSpawnCap)
        {
            yield return new WaitForSeconds(.5f);
            FlyingSpawns.Add(Instantiate(flySpawn, spawnMinionsPoint.position, Quaternion.identity));
        }
        yield return new WaitForSeconds(4f);
        float randoAngle = Random.Range(0, Mathf.PI * 2);
        spawnMinionsPoint.position = new Vector3(pivot.x + (radius * Mathf.Cos(randoAngle)), pivot.y + (radius * Mathf.Sin(randoAngle)), 4);
        attacking = false;
    }

    void Intro()
    {
        //start at top


        if ((angle - (Mathf.PI / 4)) < .025f && timer > 0)
        {
            angle += speed * Time.deltaTime;
            Vector3 next = new Vector3(pivot.x + (radius * Mathf.Cos(-angle)), pivot.y + (radius * Mathf.Sin(-angle)), 4);
            transform.position = next;
        }
        if ((angle - (Mathf.PI / 4)) >= .025f)
        {
            timer -= Time.deltaTime;
            speed = 1f;
        }
        if (timer < 0)
        {
            angle += speed * Time.deltaTime;
            Vector3 next = new Vector3(pivot.x + (radius * Mathf.Cos(-angle)), pivot.y + (radius * Mathf.Sin(-angle)), 4);
            transform.position = next;

            if ((angle - (Mathf.PI / 2)) < .025f && (angle - (Mathf.PI / 2)) > -.025f)
            {
                Destroy(platform);
                intro = false;
            }
        } 
    }

    public void removeFlyer(GameObject deadFlyer)
    {
        FlyingSpawns.Remove(deadFlyer);
        Debug.Log("Remove Flyer");
    }

    public void removeCrawler(GameObject deadCrawler)
    {
        CrawlingSpawns.Remove(deadCrawler);
        Debug.Log("Remove Crawler");
    }
}
