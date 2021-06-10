using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Manage : MonoBehaviour
{
    public bool Can_Move;
    public int cost;
    private Rigidbody rb;
    public GameObject Particle;
    private Transform transform_Tel;
    private Animator anim;
    public Tel_Script Tel_Script;
    public bool Push;
    public bool Can_Dash;
    public float Damage;
    public float Charge;
    public float h;
    public bool ready;
    public float Type;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Damage = 100;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Magic_Tel")
        {
            h = Mathf.Abs(transform.position.y - Tel_Script.transform.position.y);
            if (rb.mass < 25)
            {
                Charge = Mathf.Abs(transform.position.y - transform_Tel.position.y);
            }
            else if (rb.mass > 25 && rb.mass < 75) 
            {
                Charge = Mathf.Abs(transform.position.y - transform_Tel.position.y) * 10;
            }
            if (Can_Move && Tel_Script.ready)
            {
                if (Can_Dash)
                {
                    StartCoroutine("Dash");
                    Can_Dash = false;
                    Tel_Script.Reload = true;
                }
                rb.drag = 0;
            }

            Damage = Mathf.Round(80 * Charge * cost);
            if (Tel_Script.isActive && transform.position.y < transform_Tel.position.y + 3 && Can_Move)
            {
                Push = true;
                if (!Input.GetMouseButtonUp(0))
                {
                    rb.drag = 50;
                }
                rb.MovePosition(rb.position + transform_Tel.up * 0.04f);
                Particle.SetActive(true);
            }
            else if (Tel_Script.isActive && Can_Move && Vector3.Distance(Tel_Script.Transform_Cam.position, transform.position) > 2 && (Mathf.Abs(transform.position.y - Tel_Script.transform.position.y) > 3f))
            {
                transform.position = Vector3.Lerp(transform.position, Tel_Script.Transform_Cam.position, Time.deltaTime * 3);
            }
            else if (Tel_Script.isActive && Can_Move && Vector3.Distance(Tel_Script.Transform_Cam.position, transform.position) > 3 && (Mathf.Abs(transform.position.y - Tel_Script.transform.position.y) > 3f))
            {
                transform.position = Vector3.Lerp(transform.position, Tel_Script.Transform_Cam.position, Time.deltaTime * 20);
            }
            if (!Tel_Script.isActive) 
            {
                Push = true; 
                if (Push) 
                { 
                    rb.drag = 0; 
                    rb.MovePosition(rb.position + transform_Tel.up * -0.01f); 
                    Push = false;
                    Particle.SetActive(false);
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Magic_Tel")
        {
            transform_Tel = other.GetComponent<Transform>();
            Tel_Script = other.gameObject.GetComponent<Tel_Script>();
            if (Tel_Script.Cell > (cost - 1))
            {
                Tel_Script.Cell -= cost;
                Can_Move = true;
                Can_Dash = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Magic_Tel" && Can_Move) 
        {
            if (!Input.GetKeyDown(KeyCode.E)) 
            {
                Tel_Script.Reload = true;
            }
            if (Tel_Script.Cell < Tel_Script.Max_Cell) 
            {
                Tel_Script.Cell += cost;
            }
            Can_Move = false;
            rb.drag = 0;
            StopCoroutine("Dash");
            Tel_Script.ready = false;
            Particle.SetActive(false);
        }
    }
    IEnumerator Dash() 
    {
        rb.AddForce(Tel_Script.Transform_Cam.forward * 100 * Type * rb.mass * Charge);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Dash");
    }
}