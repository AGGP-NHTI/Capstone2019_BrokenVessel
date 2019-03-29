using System.Collections;
using System.Collections.Generic;
using BrokenVessel.Player;
using UnityEngine;

public class TeleDoor : Interact
{
    public Transform Door;
   
    public override void Impulse()
    {
        Player.This.transform.position = Door.position;
    }
}
