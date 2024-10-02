using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyMask;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    void Update()
    {
        if(Time.time >= nextAttackTime){

            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){

                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                
            }
        }  
    }

    void Attack(){

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);

        foreach(Collider2D enemy in hitEnemies){
            //Apply damage to enemies
        }

    }
    void OnDrawGizmosSelected(){
        if(attackPoint == null){ return; }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}