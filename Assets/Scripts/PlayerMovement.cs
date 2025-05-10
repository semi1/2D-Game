using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    AudioManager audioManager;

    public GameObject gameOverUI;

    public Text playerHealthAmount;

    public int playerHealth = 100;

    [Header("References")]
    public Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 7f;

    [Header("State Flags")]
    private bool isGround = true;
    private bool facingRight = true;
    public float airTimer = 0f;
    private bool isinAir = false;

    private float movement;

    [Header("Player Attack Range")]
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {

        Die();

        playerHealthAmount.text = playerHealth.ToString();

        // Get horizontal input (SimpleInput is used for mobile)
        movement = SimpleInput.GetAxis("Horizontal");

        // This is for our player moving left or right..
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if(movement > 0f && !facingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        // This is for making animation of our player for run..
        if(Mathf.Abs(movement) > 0.1f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if(Mathf.Abs(movement) < 0.1f)
        {
            animator.SetFloat("Run", 0f);
        }

        // Only allow jump if grounded
        if (isGround && Input.GetButtonDown("Jump")) // Use your own input mapping here (SimpleInput or Unity Input)
        {
            jumpButton();
        }
        if (isinAir)
        {
            airTimer += Time.deltaTime;
            if(airTimer >= 3f)
            {
                Die();
            }
        }
    }

    //This function is use for calling in animator so when our player attack on enemy object then our attack gonna work on enemy and kill..

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer); 

        if (hit != null)
        {
            Enemy enemy = hit.GetComponent<Enemy>(); // This is use because we use many enemys at a time
            if (enemy != null)
            {
                enemy.takeDamage(1);
            }
        }
    }


    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * speed;

        if (Mathf.Abs(movement) > 0.1f)  // Only play run sound if moving
        {
            if (!audioManager.Run.isPlaying)
            {
                audioManager.playRun();
            }
        }
    }

    // This function is use for giving jump of our player

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;  // Player is grounded
            isinAir = false;
            airTimer = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;  // Player is not grounded
            isinAir = true;
        }
    }

    public void jumpButton()
    {
        if (isGround) // Only allow jumping if the player is on the ground
        {
            // Perform the jump action
            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpForce;
            rb.linearVelocity = velocity;

            isGround = false; // Set isGround to false until the player touches the ground again
            isinAir = true;
        }
    }

    // This function is call when our player go and touch to trap object then our player is die;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            getDamage(15);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // This make sure our player is on ground or not
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isinAir = false;
            airTimer = 0f;
        }
    }

    public void randomAttackAnimation()  // This function is for making animation of our player attack
    {
        int random = Random.Range(0, 3);

        if(random == 0)
        {
            animator.SetTrigger("Attack 1");
        }
        else if(random == 1)
        {
            animator.SetTrigger("Attack 2");
        }
        else
        {
            animator.SetTrigger("Attack 3");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void getDamage(int damage)
    {
        // Check if the player's health is already at or below 0
        if (playerHealth > 0)
        {
            playerHealth -= damage;

            // Ensure that health doesn't go below 0
            if (playerHealth < 0)
            {
                playerHealth = 0;
            }

            audioManager.playHurt();
            animator.SetTrigger("Hurt");
        }
    }


    IEnumerator playerDeathAnimation()
    {
        
        audioManager.playPlayerDeath();
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.1f);
        gameOverUI.SetActive(true);
        Destroy(this.gameObject);
    }

    void Die()
    {
        if(airTimer >= 3f || playerHealth <= 0)
        {
            if (playerHealth > 0)
            {
                playerHealth = 0;
            }
            StartCoroutine(playerDeathAnimation());
        }
    }
}