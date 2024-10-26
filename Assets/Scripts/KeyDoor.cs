using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public int DoorNumber;
    public Key KeyScript;
    // Start is called before the first frame update
    void Start()
    {
        KeyScript.KeyOne = false;
        KeyScript.KeyTwo = false;
        KeyScript.KeyThree = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(DoorNumber == 1 && KeyScript.KeyOne == true)
            {
                Destroy(gameObject);
            }
            if (DoorNumber == 2 && KeyScript.KeyTwo == true)
            {
                Destroy(gameObject);
            }
            if (DoorNumber == 3 && KeyScript.KeyThree == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
