using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss : EnemyControler
{
    [SerializeField]
    GameObject[] attacks;
    [SerializeField]
    float specialAttackCooldown;
    [SerializeField]
    int level;
    [SerializeField]
    AudioSource source2;
    [SerializeField]
    Sound hit;
    [SerializeField]
    Sound death2;
   
    protected override void Start()
    {
        base.Start();
        health.health = 50;
        //anim = GetComponent<Animator>();
    }
    protected override void Update()
    {
        if (alive)
        {
            if (hasSeenPlayer2)
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
    protected override void combatAI()
    {
        MoveToPlayer(playerObj.transform, minDistance);
        StartCoroutine(attack());
    }

    protected override IEnumerator attack()
    {

        if (canAttack)
        {
            canAttack = false;
            if (Mathf.Round(Random.Range(0, 2)) > 0)
            {
                anim.SetTrigger("attack2");

                spawnSpecial();
            } else
            {
                anim.SetTrigger("attack");
               
                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    spawnAttack();
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yield return new WaitForSeconds(attackCoolDown);
            canAttack = true;
        }
    }
   
    void spawnSpecial()
    {
        Instantiate(attacks[Random.Range(0, attacks.Length)], transform.position, Quaternion.identity);
    }

    public override void Hit(int dammage)
    {
        anim.SetTrigger("hit");
        source2.volume = hit.getVolume();
        source2.pitch = hit.getRandomPitch();
        source2.clip = hit.getClip();
        source2.Play();
        health.health -= dammage;
        if (health.health <= 0)
        {
            anim.SetTrigger("death");
            Death();
        }
    }
    public override void Death()
    {
        DeathEffects();
        source2.volume = death2.getVolume();
        source2.pitch = death2.getRandomPitch();
        source2.clip = death2.getClip();
        source2.Play();
        StartCoroutine(deathCorotine());
    }

    IEnumerator deathCorotine()
    {
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(level);
    }
}
