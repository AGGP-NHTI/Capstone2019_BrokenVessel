using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    PlayerData data;

    bool dashUnlocked = false;
    bool wallGrabUnlocked = false;
    bool grappleHookUnlocked = false;
    bool phaseUnlocked = false;
    bool phaseDashUnlocked = false;
    bool chargeJumpUnlocked = false;
    int shipControlUnlocked = 0;

    Rigidbody2D rig;

    private void Start()
    {
        //data = GameObject.Find("DATA").GetComponent<PlayerData>();
        data = GetComponent<PlayerData>();

        wallGrabUnlocked = data.wallGrab;
        grappleHookUnlocked = data.grappleHook;
        phaseUnlocked = data.phase;
        phaseDashUnlocked = data.phaseDash;
        chargeJumpUnlocked = data.chargeJump;
        shipControlUnlocked = data.shipControl;

        rig = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {

    }
}
