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

    public GameObject stationaryCollider;
    public GameObject deathPlatform;

    bool attacking = false;

    public bool intro;
    float timer = 1f;
    [SerializeField] GameObject platform;
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
                if(GetComponent<EnemyCombat>().health < 0)
                {
                    MenuControl.MC.CloseBossBar();
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
        stationaryCollider.SetActive(true);
        attacking = true;
        yield return new WaitForSeconds(4f);
        if (FlyingSpawns != null)
        {
            for (int i = 0; i < FlyingSpawns.Count; i++) //check for dead enemies in the list
            {
                if (FlyingSpawns[i] == null) FlyingSpawns.Remove(FlyingSpawns[i]);
                if (FlyingSpawns.Count == 0) break;
            }
        }
        if (CrawlingSpawns != null)
        {
            for (int i = 0; i < CrawlingSpawns.Count; i++) //check for dead enemies in the list
            {
                if (CrawlingSpawns[i] == null) CrawlingSpawns.Remove(FlyingSpawns[i]);
                if (CrawlingSpawns.Count == 0) break;
            }
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
        stationaryCollider.SetActive(false);
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
                MenuControl.MC.OpenBossBar(GetComponent<EnemyCombat>());
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
