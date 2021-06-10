using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trigger : MonoBehaviour
{
    public Enemy_1 Enemy_1;
    public GameObject Enemy_1_obj;
    public Player_Controller P_C;
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
        P_C = Player.GetComponent<Player_Controller>();
    }

    public void OnTriggerEnter(Collider Damage)
    {
        if (Damage.gameObject.name == "default" && P_C.Attack && P_C.combo == 0)
        {
            Enemy_1.HP -= 10f;
            if (Enemy_1.HP <= 0) { Instantiate(Enemy_1.Ragdoll, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation); Destroy(Enemy_1_obj); }
        }
        else if (Damage.gameObject.name == "default" && P_C.Attack && P_C.combo == 1)
        {
            Enemy_1.HP -= 15f;
            if (Enemy_1.HP <= 0) { Instantiate(Enemy_1.Ragdoll, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation); Destroy(Enemy_1_obj); }
            Enemy_1.stan = true;
            Enemy_1.StartCoroutine("Stan");
            Enemy_1.Anim.SetTrigger("Stan");
        }
        else if (Damage.gameObject.name == "Sword" && P_C.Attack && P_C.combo == 2)
        {
            Enemy_1.HP -= 20f;
            if (Enemy_1.HP <= 0) { Instantiate(Enemy_1.Ragdoll, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation); Destroy(Enemy_1_obj); }
            Enemy_1.StartCoroutine("Stan");
            if (!Enemy_1.stan) { Enemy_1.Anim.SetTrigger("Stan"); }
            Enemy_1.stan = true;
        }
        if (Damage.gameObject.tag == "Rock_Dash") 
        {
            Rigidbody Rock_RB = Damage.gameObject.GetComponent<Rigidbody>();
            if (Mathf.Abs(Rock_RB.velocity.x) > 2 || Mathf.Abs(Rock_RB.velocity.y) > 2 || Mathf.Abs(Rock_RB.velocity.z) > 2) 
            {
                Enemy_1.HP -= Damage.gameObject.GetComponent<Rock_Manage>().Damage;
                if (Enemy_1.HP <= 0) 
                {
                    Destroy(Enemy_1_obj);
                    Instantiate(Enemy_1.Ragdoll, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation);
                    Enemy_1.Enemy_1_Ragdoll.dir = Player.transform.forward;
                }
                Enemy_1.StartCoroutine("Stan");
                if (!Enemy_1.stan) { Enemy_1.Anim.SetTrigger("Stan"); }
                Enemy_1.stan = true;
            }
        }
    }
    private void OnTriggerStay(Collider Damage_Stay)
    {
        if (Damage_Stay.gameObject.tag == "Magic_Fire")
        {
            if (Enemy_1.Can_Burn == 0)
            {
                Enemy_1.Can_Burn = 1;
                if (Enemy_1.Can_Burn == 1)
                {
                    Enemy_1.StartCoroutine("Fire_Enter"); Enemy_1.Can_Burn = 2;
                }
            }
        }
    }
}
