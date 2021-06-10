using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Event_Trigger : MonoBehaviour
{
    public bool BoolPluse;
    public Player_Controller Player;
    public bool Attack2;
    public bool Check;
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && Player.combo == 0 && BoolPluse) { Player.combo++; }
        if (Input.GetMouseButtonDown(0) && Player.combo == 1 && BoolPluse && Attack2) { Player.combo++; }
    }
    public void Combo_Pluse()
    {
        StartCoroutine("Pluse");
    }
    public void Combo_Last()
    {
        Player.combo = 0;
        Check = true;
        Attack2 = false;
    }

    public void Attack_Is_Fine()
    {
        Player.Attack = false;
    }
    public void Attack_2()
    {
        if (Player.combo != 2) { Player.combo = 0; Attack2 = true; }

    }
    public void Attack_2_f()
    {
        Attack2 = false;
    }
    public void DeEnCol()
    {
        Player.Sword_2_coll.enabled = false;
    }
    public void EnCol()
    {
        Player.Sword_2_coll.enabled = true;
    }
    public void Knock()
    {
        Player.Knock = false;
    }
    IEnumerator Pluse()
    {
        BoolPluse = true;
        yield return new WaitForSeconds(1f);
        BoolPluse = false;
    }
}
