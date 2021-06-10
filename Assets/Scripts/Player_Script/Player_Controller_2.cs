using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller_2 : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [Header("Skills")]
    public float Speed_Regen_Stam;
    public float Speed_Regen_Mana;
    [Space]
    public UI_Manager UI;
    public Anim_Event_Trigger A_E_T;
    public Rigidbody rb;
    private Vector3 Velocity = Vector3.zero;
    private Vector3 Jerk = Vector3.one;
    public GameObject Cam;
    public Animation Cam_anim;
    public Cam_Controller Cam_Controller;
    public bool IsMove;
    public bool Attack;
    [Header("Magic_Have")]
    public bool Fire_Have;
    [Header("Hands")]
    public GameObject Magic_Hand;
    public GameObject Sword_Hand;
    public GameObject Right_Hand;
    [Header("Sword_Have")]
    public bool Sword_Have;
    [Space]
    public Animator Right_Hand_Anim;
    public Animator Left_Hand;
    public bool Knock;
    public bool Die;
    public GameObject Hand;
    public Animation HandAnim;
    public Animator HandAnim2;
    public GameObject Sword_obj;
    public GameObject Sword_Colider2;
    public GameObject Fire_Particle;
    public GameObject Fire_Particle2;
    public Transform Fire_Particle_Tr;
    public Collider Sword_1_coll;
    public Collider Sword_2_coll;
    public GameObject Stamina;
    public GameObject Health;
    public Transform Mana;
    public float combo;
    [Header("Sword_active")]
    public bool Sword;
    [Header("Magic_active")]
    public bool Fire;
    [Header("Item")]
    public float Item_1;
    public float Item_2;
    public float Item_3;
    public float Item_4;
    public float Item_5;
    [Space]
    private bool F_L;
    public bool M_F;
    public bool w;
    public bool Magic_attack;
    public bool Damage = true;
    public bool M_H;
    public string ww = "Alpha1";
    public float Mana_Score = 100;
    private Transform Dalay_Image;
    private float Last_Time;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        HandAnim = Hand.GetComponent<Animation>();
        Sword_2_coll = Sword_Colider2.GetComponent<Collider>();
        Sword_2_coll.enabled = false;
        Sword_1_coll = Sword_obj.GetComponent<Collider>();
        Cam_anim = Cam.GetComponent<Animation>();
        Dalay_Image = UI.Dalay.transform;
    }

    void Update()
    {
        if (F_L) { StartCoroutine("Long_Fire"); F_L = false; }
        //
        if (Input.GetKeyDown(KeyCode.Alpha1) && Dalay_Image.localScale.x == 0 && Item_1 != 0) { Item_D(1); }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Dalay_Image.localScale.x == 0 && Item_2 != 0) { Item_D(2); }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Dalay_Image.localScale.x == 0 && Item_3 != 0) { Item_D(3); }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Dalay_Image.localScale.x == 0 && Item_4 != 0) { Item_D(4); }
        if (Input.GetKeyDown(KeyCode.Alpha5) && Dalay_Image.localScale.x == 0 && Item_5 != 0) { Item_D(5); }

        //
        if (Input.GetMouseButton(1) && Fire && Mana.localScale.x > 0 && Left_Hand.enabled && !UI.pause && !UI.Inv)
        {
            Magic_attack = true;
            UI.Regen_Mana = false;
            Left_Hand.SetBool("Fire", true);
            Left_Hand.SetBool("Idel", false);
            StartCoroutine("Mana_Can_Regen");
            Fire_Particle.SetActive(true);
            if (Fire_Particle_Tr.localScale.z == 0) { F_L = true; }
            if (M_F == false)
            {
                M_F = true;
                StartCoroutine("Mana_Fire");
            }
            Fire_Particle2.SetActive(false);
        }
        else if (Left_Hand.enabled)
        {
            StopCoroutine("Mana_Fire");
            Magic_attack = false;
            Left_Hand.SetBool("Fire", false);
            Left_Hand.SetBool("Idel", true);
            StopCoroutine("Mana_Fire");
            StopCoroutine("Long_Fire");
            if (Mana.localScale.x < 0) { Mana.localScale = new Vector3(0, 0.1f, 1); }
            Fire_Particle.SetActive(false);
            if (Fire_Particle_Tr.localScale.z > 0) { Fire_Particle_Tr.localScale = new Vector3(Fire_Particle_Tr.localScale.x, Fire_Particle_Tr.localScale.y, 0); }
            F_L = false;
            M_F = false;
            Fire_Particle2.SetActive(true);
        }


        //Код для атаки катаной
        if (Input.GetMouseButtonDown(0) && Sword && combo == 0 && !UI.pause && !UI.Inv && !Attack && Stamina.transform.localScale.x >= 5 && !Knock)
        {
            Attack = true;
            StartCoroutine("Dalay");
            HandAnim.Play("Attack_Combo1");
            Cam_anim.Play("Rot_Combo1");
            Stamina.transform.localScale = new Vector3(Stamina.transform.localScale.x - 5, Stamina.transform.localScale.y, Stamina.transform.localScale.z);
            Sword_1_coll.enabled = false;
            Sword_2_coll.enabled = true;
        }
        else if (Input.GetMouseButtonDown(0) && Sword && combo == 1 && !UI.pause && !UI.Inv && !Attack && Stamina.transform.localScale.x >= 10)
        {
            Attack = true;
            StartCoroutine("Dalay");
            HandAnim.Play("Attack_Combo2");
            Cam_anim.Play("Rot_Combo2");
            Stamina.transform.localScale = new Vector3(Stamina.transform.localScale.x - 10, Stamina.transform.localScale.y, Stamina.transform.localScale.z);
            Sword_2_coll.enabled = true;
            if (Input.GetKeyDown(KeyCode.W))
            { rb.AddForce(10000 * transform.forward * Time.deltaTime); }

        }
        else if (Input.GetMouseButtonDown(0) && Sword && combo == 2 && !UI.pause && !UI.Inv && !Attack && Stamina.transform.localScale.x >= 20)
        {
            Attack = true;
            StartCoroutine("Dalay");
            StartCoroutine("Combo_Last");
            HandAnim2.SetBool("Attack", true);
            Cam_anim.Play("Rot_Combo3");
            Stamina.transform.localScale = new Vector3(Stamina.transform.localScale.x - 20, Stamina.transform.localScale.y, Stamina.transform.localScale.z);
            Sword_1_coll.enabled = true;
            Sword_2_coll.enabled = false;
        }
        if (Input.GetMouseButtonDown(0) && !Sword && !UI.pause && !UI.Inv && !Attack && Stamina.transform.localScale.x >= 20)
        {
            if ((Time.time - Last_Time) <= 2f) { Mana_Score++; }
            Last_Time = Time.time;

        }
        if (Sword && !IsMove && !Attack && !Knock) { HandAnim.Play("Idel_Hand"); }
        else if (Sword && !Attack && !Knock) { HandAnim.Play("Run_Hand"); }
        if (!Sword)
        {
            HandAnim.Stop("Idel_Hand");
            Sword_obj.SetActive(false);
            Sword_Hand.SetActive(false);
            Right_Hand.SetActive(true);
        }
        else
        {
            Sword_obj.SetActive(true);
            Sword_Hand.SetActive(true);
            Right_Hand.SetActive(false);
        }
        if (!M_H)
        {
            Left_Hand.enabled = false;
            Magic_Hand.SetActive(false);
        }
        else
        {
            Left_Hand.enabled = true;
            Magic_Hand.SetActive(true);
        }
        //передвежение
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        if (xMov != 0 || zMov != 0 && !UI.pause) { IsMove = true; } else if (xMov == 0 && zMov == 0) { IsMove = false; }
        Vector3 Move_Hro = transform.right * xMov;
        Vector3 Move_Ver = transform.forward * zMov;
        Velocity = (Move_Hro + Move_Ver).normalized * speed;
    }
    //Item_Distribution
    void Item_D(int i)
    {
        if (i == 1)
        {
            if (Item_1 == 1 && Sword_Have && !Sword && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Sword = true;
                M_H = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_1 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
                M_H = true;
                Sword = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
            }
        }
        else if (i == 2)
        {
            if (Item_2 == 1 && Sword_Have && !Sword && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Sword = true;
                M_H = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_2 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
                M_H = true;
                Sword = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
            }
        }
        else if (i == 3)
        {
            if (Item_3 == 1 && Sword_Have && !Sword && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Sword = true;
                M_H = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_3 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
                M_H = true;
                Sword = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
            }
        }
        else if (i == 4)
        {
            if (Item_4 == 1 && Sword_Have && !Sword && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Sword = true;
                M_H = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_4 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
                M_H = true;
                Sword = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
            }
        }
        else if (i == 5) 
        {
            if (Item_5 == 1 && Sword_Have && !Sword && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Sword = true;
                M_H = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_5 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
                M_H = true;
                Sword = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
            }
        }
    }
    //
    void FixedUpdate()
    {
        Move();
        if (Dalay_Image.localScale.x < 0) { Dalay_Image.localScale = new Vector3(0, 0.03f, 1); }
    }

    public void Move()
    {
        if (Velocity != Vector3.zero && Input.GetKeyDown(KeyCode.LeftShift)) { rb.MovePosition(rb.position + Velocity * 2f * Time.fixedDeltaTime); }
        else if (Velocity != Vector3.zero) { rb.MovePosition(rb.position + Velocity * Time.fixedDeltaTime); }
    }
    IEnumerator Dalay()
    {
        UI.Regen = false;
        yield return new WaitForSeconds(2f);
        UI.Regen = true;
        UI.StartCoroutine("Stam_Regen");
    }
    IEnumerator Dalay_Return()
    {
        if (Dalay_Image.localScale.x > 0)
        {
            yield return new WaitForSeconds(0.01f);
            Dalay_Image.localScale = new Vector3(Dalay_Image.localScale.x - 0.2f, 0.03f, 1);
            StartCoroutine("Dalay_Return");
        }
    }
    IEnumerator Combo_Last()
    {
        yield return new WaitForSeconds(0.2f);
        HandAnim2.SetBool("Attack", false);
        yield return new WaitForSeconds(0.8f);
        yield return new WaitForSeconds(0.85f);
        A_E_T.Combo_Last();
        A_E_T.Attack_Is_Fine();
    }
    //
    IEnumerator Mana_Fire() 
    {
        if (Mana.localScale.x > 0) { Mana.localScale = new Vector3(Mana.localScale.x - 0.5f, Mana.localScale.y, Mana.localScale.z); }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Mana_Fire");
    }
    //Надо остонавливать следующюю куратину Mana_Can_Regen в куратине UI.Mana_Regen т.к из-за постоянного вызова 1-ой куратины она постоянно вызывала 2-ую куратину
    IEnumerator Mana_Can_Regen()
    {
        UI.Regen_Mana = false;
        yield return new WaitForSeconds(2f);
        UI.Regen_Mana = true;
        UI.StartCoroutine("Mana_Regen");
    }
    IEnumerator Long_Fire() 
    {
        if (Fire_Particle_Tr.localScale.z < 1)
        {
            Fire_Particle_Tr.localScale = new Vector3(Fire_Particle_Tr.localScale.x, Fire_Particle_Tr.localScale.y, Fire_Particle_Tr.localScale.z + 0.1f);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine("Long_Fire");
        }
    }
}