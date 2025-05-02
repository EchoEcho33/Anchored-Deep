using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Unity.VisualStudio.Editor;
using Novasloth;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Healthbar
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private float playerHealth = 100;

    [SerializeField] private GameObject hitEffect;

    private float currentHealth;

    private float playerCurrentHealth;

    [SerializeField] private Healthbar healthbar;

    [SerializeField] private Healthbar playerHealthbar;

    //Explosion Soundeffect
    private AudioSource audioSource;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks = 3.0f;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animation anim;

    private void Start()
    {
        currentHealth = maxHealth;
        playerCurrentHealth = playerHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
        audioSource = GetComponent<AudioSource>();   
    }

    private void Awake()
    {
        player = GameObject.Find("Boat").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer(); 
    }

    private void Patroling() {
        anim = GetComponent<Animation>();
        
        if (!anim.IsPlaying("dive")) {
            anim.Play("fastswim");
        }

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint() {

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void ChasePlayer() {
        agent.SetDestination(player.position);
        anim = GetComponent<Animation>();
        if (!anim.IsPlaying("dive")) {
            anim.Play("fastswim");
        }
    }

    private void AttackPlayer() {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked) {
            ///Attack code here
            TakeDamage(10.0f);
            
            anim = GetComponent<Animation>();
            anim["dive"].speed = 3.0f;
            anim.Play("dive");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage) {
        playerCurrentHealth -= damage;

        if (playerCurrentHealth <= 0) {
            SceneManager.LoadScene("Death");
        } else {
            playerHealthbar.UpdateHealthBar(playerHealth, playerCurrentHealth);
        }
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private bool cooldown = false;

    private void ResetCooldown(){
        cooldown = false;
    }

    
    private void OnMouseDown()
    {
        if (!healthbar.gameObject.activeSelf) {
            healthbar.gameObject.SetActive(true);
        }

        if ( cooldown == false ) {
            audioSource.Play();
            currentHealth -= Random.Range(5f, 20f);

            if (currentHealth <= 0) {
                Invoke(nameof(DestroyEnemy), 0f);
            } else {
                healthbar.UpdateHealthBar(maxHealth, currentHealth);
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            Invoke("ResetCooldown",1.0f);
            cooldown = true;
        }
    }
}
