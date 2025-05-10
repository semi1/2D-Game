using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    AudioManager audioManager;

    public int maxHealth = 3;

    public Animator animator;

    public Transform player;

    [Header("EnemyRange")]
    public float attackRange = 3f;
    public LayerMask attackLayer;

    public float enemyWalkSpeed = 2f;

    bool Facingleft = true;

    [Header("FacingControl")]
    public Transform detectPoint;
    public float distance;//It tell about our enemy's detect point range from where is need to rotate
    public LayerMask targetLayer;


    public Transform attackPoint;
    public float attackDistance = 1f;
    public LayerMask layerMask;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {

        Die();

        Collider2D attackInfo = Physics2D.OverlapCircle(transform.position, attackRange, attackLayer);//It say our player Enter in range of enemy;

        if(attackInfo == true)//If our player is in range of our enemy then ->
        {
            animator.SetBool("Attack", true);
            if(player.position.x > transform.position.x && Facingleft == true)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                Facingleft = false;
            }
            else if(player.position.x < transform.position.x && Facingleft == false)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                Facingleft = true;
            }
        }
        
        else//If our player is not in range of our enemy ->
        {
            animator.SetBool("Attack", false);
            transform.Translate(Vector2.left * Time.deltaTime * enemyWalkSpeed);
            RaycastHit2D callInfo = Physics2D.Raycast(detectPoint.position, new Vector2(0f, -1f), distance, targetLayer);
            if(callInfo == false)
            {
                if(Facingleft == true)
                {
                    transform.eulerAngles = new Vector3(0f, -180f, 0f);
                    Facingleft = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    Facingleft = true;
                }
            }
        }
    }

    public void takeDamage(int damage) //This function is for enemy health and this use in different script so we make it public
    {
        if(maxHealth >= 0)
        {
            maxHealth -= damage;
            animator.SetTrigger("Hurt");
        }
    }

    private void OnDrawGizmosSelected() //It make a circle arround the enemy so we can make an attack area or see the attacking area..
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    IEnumerator EnemyDieAnimation() // It make our enemy to die
    {
        audioManager.playEnemyDeath();
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.20f);
        //FindAnyObjectByType<GameManager>().isGameActive = false;
        Destroy(this.gameObject);
    }

    void Die() 
    {
        if(maxHealth <= 0)
        {
            StartCoroutine(EnemyDieAnimation());
        }
    }

    public void AttacktoPlayer()
    {
        Collider2D attackInfo = Physics2D.OverlapCircle(attackPoint.position, attackDistance, layerMask);//It say our player Enter in range of enemy;

        if(attackInfo == true)
        {
            
            if (attackInfo.GetComponent<PlayerMovement>() != null)
            {
                attackInfo.GetComponent<PlayerMovement>().getDamage(25);
            }
        }
    }
}
