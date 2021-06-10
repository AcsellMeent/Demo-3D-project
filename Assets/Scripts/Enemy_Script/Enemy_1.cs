using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_1 : MonoBehaviour
{
    public Enemy_Trigger E_T;
    public Player_Controller P_C;
    public GameObject Player;
    public GameObject Fire_Particle;
    public Animator Anim;
    public Transform H_B;
    public Sword_Enemy S_E;
    public float HP = 100f;
    public float dist;
    public float radius = 15;
    public bool Attack;
    public bool CanAttack = true;
    private NavMeshAgent nav;
    public GameObject Ragdoll;
    public Enemy_1_Ragdoll Enemy_1_Ragdoll;
    public int combo;
    public bool stan;
    public bool Fire_Damage;
    public float Can_Run;
    public float w;
    public float Can_Burn;
    public bool can_Stop;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        P_C = Player.GetComponent<Player_Controller>();
        HP = 100f;
        w = 0;
    }

    void Update()
    {
        dist = Vector3.Distance(Player.transform.position, transform.position);
        if (dist > radius && HP > 0 && !stan && !Fire_Damage)
        {
            Can_Run = 0;
            //StopCoroutine("Run");
            nav.enabled = false;
            //if (!can_Stop) { StartCoroutine("Stop"); can_Stop = true; }
        }
        if (dist < radius && dist > 2f && !P_C.Die && !Attack && HP > 0 && !stan && !Fire_Damage)
        {
            nav.enabled = true;
            nav.SetDestination(Player.transform.position);
            if (Can_Run == 0)
            {
                Can_Run = 1;
                if (Can_Run == 1)
                {
                    Can_Run = 2;
                    if (dist < radius)
                    {
                        Anim.SetTrigger("Run");
                    }
                    else { Anim.SetTrigger("Stop"); }
                }
            }

        }
        if (dist < 2f && !P_C.Die && HP > 0 && !stan && !Fire_Damage)
        {
            Can_Run = 0;
            StopCoroutine("Run");
            nav.enabled = false;
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            if (CanAttack && !Attack)
            {
                CanAttack = false;
                combo = Random.Range(1, 5);
                if (combo >= 3 && !Attack)
                {
                    w++;
                    Attack = true;
                    Anim.SetTrigger("Attack");
                    StartCoroutine("CoolDown1");
                }
                else if (!Attack)
                {
                    w++;
                    Attack = true;
                    Anim.SetTrigger("Attack2");
                    StartCoroutine("CoolDown2");
                }
            }
        }
    }
    //
    IEnumerator Fire_Enter()
    {
        nav.enabled = false;
        Anim.SetTrigger("Burn");
        Fire_Damage = true;
        Fire_Particle.SetActive(true);
        nav.speed = 0;
        StartCoroutine("Fire_DM");
        yield return new WaitForSeconds(0.5f);
        Can_Burn = 0;
        yield return new WaitForSeconds(0.5f);
        Can_Burn = 2;
        yield return new WaitForSeconds(1.5f);
        Fire_Damage = false;
        Fire_Particle.SetActive(false);
        nav.speed = 5;
        Anim.SetTrigger("Idel");
        Can_Run = 0;
        StopCoroutine("Run");
        Can_Burn = 0;
    }
    IEnumerator Fire_DM()
    {
        if (Fire_Damage)
        {
            HP -= 10f;
            if (HP <= 0) { Instantiate(Ragdoll, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation); Destroy(gameObject); }
            yield return new WaitForSeconds(1f);
            StartCoroutine("Fire_DM");
        }

    }
    //
    IEnumerator CoolDown1()
    {
        yield return new WaitForSeconds(0.1f);
        Attack = true;
        S_E.Attack = Attack;
        yield return new WaitForSeconds(0.5f);
        Anim.SetTrigger("Idel A");
        yield return new WaitForSeconds(0.5f);
        Attack = false;
        S_E.Attack = Attack;
        CanAttack = true;
        Can_Run = 0;
        StopCoroutine("Run");
    }
    IEnumerator CoolDown2()
    {
        yield return new WaitForSeconds(0.6f);
        S_E.Attack = Attack;
        yield return new WaitForSeconds(1.1f);
        Attack = false;
        S_E.Attack = Attack;
        CanAttack = true;
        Can_Run = 0;
        StopCoroutine("Run");
    }
    IEnumerator Stan()
    {
        yield return new WaitForSeconds(0.3f);
        stan = false;
    }
    IEnumerator Stop() 
    {
        //Anim.SetTrigger("Idel");
        yield return new WaitForSeconds(0.8f);
    }
}
