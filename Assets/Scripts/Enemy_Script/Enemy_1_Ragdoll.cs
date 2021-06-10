using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Ragdoll : MonoBehaviour
{
    public float r;
    public Rigidbody[] Ragdoll;
    public Rigidbody Hips;
    public Animator anim;
    public Vector3 dir;
    public int i;
    public bool Knoc_Start;
    private void Awake()
    {
        r = Random.Range(0,3);
        if (r >= 2) { anim.SetTrigger("DIE1"); } else { anim.SetTrigger("DIE2"); }
        for (i = 0; i < 11;i ++)
        {
            Ragdoll[i].isKinematic = true;
        }
        StartCoroutine("Rag");
    }
    public void Update()
    {
        if (i == 11 && Knoc_Start) { Invoke("knoc",0.2f); Knoc_Start = false; }
    }
    IEnumerator Rag() 
    {
        yield return new WaitForSeconds(1f);
        anim.enabled = false;
        for (int i = 0; i < 11; i++)
        {
            Ragdoll[i].isKinematic = false;
        }
    }
    public void knoc() 
    {
         Hips.AddForce(dir * 1000);
    }
}
