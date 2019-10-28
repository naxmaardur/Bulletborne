using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
public class EnemyControler : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject playerObj;
    [SerializeField]
    protected bool hasSeenPlayer;
    protected bool hasSeenPlayer2;
    [SerializeField]
    protected Health health;
    [SerializeField]
    protected float walkRadius;
    protected Vector3 startPos;
    protected bool isPickingPos;
    [SerializeField]
    protected float detectionDist;
    [SerializeField]
    protected LayerMask layerMask;
    [SerializeField]
    protected GameObject Bullet;
    [SerializeField]
    protected bool canAttack;
    [SerializeField]
    protected float attackCoolDown;
    [SerializeField]
    protected bool normalAttack = true;
    [SerializeField]
    protected float minDistance;
    [SerializeField]
    GameObject[] blood;
    [SerializeField]
    protected Animator anim;
    bool left;
    [SerializeField]
    Transform attackPoint;
    [SerializeField]
    protected Sound death;
    [SerializeField]
    protected AudioSource source;
    protected bool alive = true;
    Transform sprite;
    float scaleX;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        startPos = transform.position;
        if (!playerObj)
            playerObj = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        sprite = transform.GetChild(0);
        scaleX = sprite.localScale.x;
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (alive)
        {
            if (hasSeenPlayer)
            {
                combatAI();

            }
            else
            {
                idelAI();
            }
            facing();
        }
    }

    protected void facing()
    {
        
        
            sprite.localScale = new Vector3(
            Vector3.Distance(
            playerObj.transform.position,
            transform.position + new Vector3(2, 0, 0)
            )
            >
            Vector3.Distance(
            playerObj.transform.position,
            transform.position + new Vector3(-2, 0, 0)
            ) ? scaleX : -scaleX,
            sprite.localScale.y,
            sprite.localScale.z
            );
    }
    protected void idelAI()
    {
        
        if (agent.remainingDistance <= 0)
            StartCoroutine(pickIdelPos());
        checkForPlayer();
    }
    protected virtual void combatAI()
    {
        MoveToPlayer(playerObj.transform, minDistance);
        StartCoroutine(attack());
    }

    protected void MoveToPlayer(Transform player, float minDist)
    {
       
        agent.SetDestination(player.position);
        if (Vector3.Distance(player.position , transform.position) < minDist)
            agent.SetDestination(transform.position);
    }

    protected virtual IEnumerator attack()
    {
        if (canAttack && normalAttack)
        {
            if (anim)
                anim.SetTrigger("attack");
            canAttack = false;
            //spawnAttack();
            yield return new WaitForSeconds(attackCoolDown);
            canAttack = true;
        }
    }
    protected void spawnAttack()
    {
        GameObject bullet = Instantiate(Bullet, attackPoint.position, Quaternion.identity);
        bullet.transform.LookAt(playerObj.transform.position);
        Quaternion q = bullet.transform.rotation;
        q.eulerAngles = new Vector3(0, q.eulerAngles.y, q.eulerAngles.z);
        bullet.transform.rotation = q;
    }

    IEnumerator pickIdelPos()
    {
        if (!isPickingPos)
        {
            isPickingPos = true;
            float time = Random.Range(0, 3);

            yield return new WaitForSeconds(time);
            randompos();
            isPickingPos = false;
        }
    }

    protected void randompos()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += startPos;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    protected void checkForPlayer()
    {
        if (!hasSeenPlayer2)
        {
            if (Vector3.Distance(playerObj.transform.position, transform.position) <= detectionDist)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (playerObj.transform.position - transform.position), out hit, detectionDist, layerMask))
                {
                    if (hit.transform.tag == "Player")
                    {
                        anim.SetTrigger("player");
                        hasSeenPlayer2 = true;
                    }
                }
            }
        }
        
    }
    public void setSeen()
    {
        hasSeenPlayer = true;
    }

    public virtual void Hit(int dammage)
    {
        health.health -= dammage;
        if(health.health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {

        DeathEffects();
        agent.SetDestination(transform.position);
        anim.SetTrigger("death");
        
    }

    protected virtual void DeathEffects()
    {
        alive = false;
        source.volume = death.getVolume();
        source.pitch = death.getRandomPitch();
        source.clip = death.getClip();
        source.Play();
        
    }

    public virtual void Delete()
    {
        GameObject b = Instantiate(blood[Random.Range(0, blood.Length)], transform.position, Quaternion.identity);
        b.transform.localScale = new Vector3(Random.Range(0.5f, 1.3f), 1, Random.Range(0.5f, 1.3f));
        Destroy(gameObject);
    }
}
