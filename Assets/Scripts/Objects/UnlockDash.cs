using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockDash : MonoBehaviour
{
    public bool UnlockedSprint;
    public GameObject telaDash;
    public Button DashBtn;

    // Start is called before the first frame update
    void Start()
    {
        UnlockedSprint = false;
        telaDash.active = false;
        DashBtn.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            UnlockedSprint = true;
            telaDash.active = true;
            DashBtn.enabled = true;
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }
}
