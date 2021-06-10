using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword_Script : MonoBehaviour
{
    public Player_Controller P_C;
    public Anim_Event_Trigger A_E_T;
    public GameObject S;
    public Image Sprite;
    public Color color;
    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.isStatic && P_C.Attack) 
        //{
        //    P_C.Knock = true;
        //    P_C.HandAnim.Stop(); 
        //    P_C.HandAnim2.Play("Stay"); 
        //    P_C.HandAnim.Play("KnockBack"); 
        //    P_C.Cam_anim.Play("Cam_KnockBack");
        //    A_E_T.Attack_2_f();
        //    A_E_T.Attack_Is_Fine();
        //    A_E_T.Combo_Last();
        //    color.a = 1;
        //    Sprite.color = color;
        //    StartCoroutine("visibal");
        //}
    }
    IEnumerator visibal() 
    {
        if (color.a > 0) {
            color.a -= 0.025f;
            Sprite.color = color;
            yield return new WaitForSeconds(0.01f);
            StartCoroutine("visibal");
        }
    }
}
