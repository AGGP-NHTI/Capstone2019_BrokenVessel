using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVsPlayerPosition : MonoBehaviour
{
    //Sectors: 0=top, 1=right, 2=bot, 3=left

    public int bossSector = 0;
    public int playerSector = 2;

    int lapSector = 0;
    bool reset = false;

    [SerializeField] Transform Center;
    [SerializeField] ScrapQueen SQ;
    [SerializeField] Transform SQLocation;
    Transform playerLocation;

    public Vector3 p = Vector3.zero;
    public Vector3 bug = Vector3.zero;

    private void Start()
    {
        playerLocation = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        p = playerLocation.position;
        bossSector = CheckIfSec(SQLocation.position);
        playerSector = CheckIfSec(playerLocation.position);


        if(bossSector != lapSector)
        {
            reset = true;
        }
        if(bossSector == lapSector && reset)
        {
            SQ.lap = true;
        }

        if(SQ.lap && playerSector == bossSector)
        {
            SQ.ableToAttack = true;
            lapSector = bossSector;
            reset = false;
        }
    }

    int CheckIfSec(Vector3 pos)
    {
        Vector3 test = Vector3.Normalize(pos - Center.position);

        bug = test;
        if (test.y >= 0 && test.y >= Mathf.Abs(test.x))
        {
            return 0;
        }
        else if(test.y < 0 && test.y < -Mathf.Abs(test.x))
        {
            return 2;
        }
        else if(test.x >= 0 && test.x > Mathf.Abs(test.y))
        {
            return 1;
        }
        else
        {
            return 3;
        }

    }
}
