using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public Sprite[] emotionSprites;
    SpriteRenderer spRend;
    public int ID; //Left = 0, Right = 1

    public enum CharacterEmotions
    {
        happy, sad, neutral, angry
    }

    CharacterEmotions myState;

	// Use this for initialization
	void Awake () {
        myState = CharacterEmotions.neutral;
        spRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeState(string emotionName)
    {
        StartCoroutine(emotionName + "State");
    }
    IEnumerator HappyState()
    {
        spRend.sprite = emotionSprites[0];
        myState = CharacterEmotions.happy;
        yield return null;
    }

    IEnumerator SadState()
    {
        spRend.sprite = emotionSprites[1];
        myState = CharacterEmotions.sad;
        yield return null;
    }

    IEnumerator NeutralState()
    {
        spRend.sprite = emotionSprites[2];
        myState = CharacterEmotions.neutral;
        yield return null;
    }

    IEnumerator AngryState()
    {
        spRend.sprite = emotionSprites[3];
        myState = CharacterEmotions.angry;
        yield return null;
    }
}
