using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float moveSpeed = 7;
    Vector2 moveDirection;
    public float activeMoveSpeed;


    public float dashSpeed;
    public float dashLength = .3f, dashCooldown = 1f;
    [SerializeField]private float timeStopDurationDash = .5f;
    [ColorUsage(true, true)]
    [SerializeField]private Color dashFlashColor;
    private float dashCounter;
    private float dashCoolCounter;
    [HideInInspector]public bool isDashing = false;

    public UnlockDash UnlockDashScrpit;
    public bool Intangivel;
    private Vector3 lastPositionBeforeDash;
    [SerializeField]private LayerMask collidingSceneObjectsLayerMask;
    [SerializeField]private UIAbilitySpriteChanger uiElement;
    public AudioSource audioSource;
    public AudioClip dash, passos;
    private DamageFlash damageFlash;
    void Start()
    {
        damageFlash = GetComponent<DamageFlash>();
        activeMoveSpeed = moveSpeed;
        UnlockDashScrpit.UnlockedSprint = false;
        Intangivel = false;
    }

    void Update()
    {
        Inputs();
    }
    private void LateUpdate() {
            
        Move();


        if(UnlockDashScrpit.UnlockedSprint == true)
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                uiElement.ChangeImage(true);
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    uiElement.ChangeImage(false);
                    anim.SetBool("Dash", true);

                    Color ogColor = damageFlash.flashColor;
                    damageFlash.flashColor = dashFlashColor;
                    damageFlash.CallDamageFlasher();

                    damageFlash.flashColor = ogColor;
                    lastPositionBeforeDash = transform.position;
                    
                    HitStop.Instance.Stop(timeStopDurationDash);

                    isDashing = true;
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    Intangivel = true;

                    StartCoroutine(DashAudioPlayWaitForTimeScale());
                }
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if(dashCounter <= 0)
            {
                anim.SetBool("Dash", false);

                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;

                isDashing = false;
                Intangivel = false;

            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime; 
        }
    }
    void Inputs(){
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        SetAnimations(moveX, moveY, moveDirection);
        
    }

    private void SetAnimations(float x, float y, Vector2 mDirection)
    {
        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", y);
        anim.SetFloat("Speed", mDirection.sqrMagnitude);
        
    }

    void Move(){
        rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);
       
    }

    IEnumerator DashAudioPlayWaitForTimeScale(){
        while(Time.timeScale == 0){
            yield return null;
        }
        audioSource.clip = dash;
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water")){
            gameObject.GetComponent<IDamageable>().Damage(1, gameObject);
            transform.position = lastPositionBeforeDash;

        }
    }
    
}
