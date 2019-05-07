using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{

    public GameObject textBox;
    public Transform spawnPosition;
    public float triggerDistance = 3.0f;
    GameObject Player;
    GameObject textBoxInstance;
    bool textBoxVisible;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(Player.transform.position, gameObject.transform.position) < triggerDistance)
        {
            if (textBoxVisible == false)
            {
                textBoxInstance = Instantiate(textBox, spawnPosition);
                textBoxVisible = true;
            }
        }
        else
        {
            if (textBoxVisible == true)
            {
                Destroy(textBoxInstance);
                textBoxVisible = false;
            }
        }
    }
}
