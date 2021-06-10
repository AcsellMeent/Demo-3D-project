using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [Header("Skills")]
    public float Speed_Regen_Stam;
    public float Speed_Regen_Mana;
    [Space]
    public UI_Manager UI;
    public Anim_Event_Trigger A_E_T;
    public Tel_Script Tel_Script;
    public Event_System E_S;
    public Rigidbody rb;
    private Vector3 Velocity = Vector3.zero;
    private Vector3 Jerk = Vector3.one;
    public GameObject Cam;
    public GameObject Head;
    public Animation Cam_anim;
    public Cam_Controller Cam_Controller;
    public bool IsMove;
    public bool Attack;
    [Header("Magic_Have")]
    public bool Fire_Have;
    public bool Strong_Have;
    [Header("Sword_Have")]
    public bool Sword_Have;
    [Space]
    public bool Knock;
    public bool Die;
    public GameObject[] Shoulder;
    public Animator Player_Anim;
    public GameObject Player_Obj;
    public GameObject Sword_obj;
    public GameObject Sword_Colider2;
    public GameObject[] Fire_Particle;
    public GameObject Fire_Particle2;
    public Transform Fire_Particle_Tr;
    public GameObject[] Stron_Particle;
    public Collider Sword_1_coll;
    public Collider Sword_2_coll;
    public GameObject Stamina;
    public GameObject Health;
    public Transform Mana;
    public Transform Stam_Mana;
    public float combo;
    [Header("Sword_active")]
    public bool Sword;
    [Header("Magic_active")]
    public bool Fire;
    public bool Strong;
    [Header("Item")]
    public float Item_1;
    public float Item_2;
    public float Item_3;
    public float Item_4;
    public float Item_5;
    [Space]
    public GameObject Mana_Stam;
    public bool Stam;
    private bool Walk_Side;
    public bool rest = true;
    public bool Run_Speed;
    private bool F_L;
    public bool M_F;
    public bool M_S;
    public float w;
    public bool Magic_Active;
    public bool Magic_attack;
    public bool Damage = true;
    public bool Item_Off;
    public bool M_R;
    public string ww = "Alpha1";
    public float Mana_Score = 100;
    private Transform Dalay_Image;
    private float Last_Time;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Sword_2_coll = Sword_Colider2.GetComponent<Collider>();
        ////Sword_2_coll.enabled = false;
        //Sword_1_coll = Sword_obj.GetComponent<Collider>();
        Cam_anim = Cam.GetComponent<Animation>();
        Dalay_Image = UI.Dalay.transform;

    }

    void Update()
    {
        if (Mana.localScale.x < 100 && Mana.localScale.x >= 0 && !M_R && !Magic_attack) { StartCoroutine("Mana_Regen"); M_R = true; }
        else if (Mana.localScale.x >= 100 || Magic_attack) { StopCoroutine("Mana_Regen"); M_R = false; }
        if (Stam_Mana.localScale.x <= 0) { Stam_Mana.localScale = new Vector3(0f, 0.1f, 1); Stam = true; }
        if (Stam_Mana.localScale.x >= 100) { Stam = false; }
        if (Stam_Mana.localScale.x < 100 && !Magic_attack && !UI.pause) { Stam_Mana.localScale = new Vector3(Stam_Mana.localScale.x + 5f, 0.1f, 1); }
        //
        if (Input.GetKeyDown(KeyCode.Alpha1) && !Magic_attack && Dalay_Image.localScale.x == 0 && Item_1 != 0) { Item_D(1); }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !Magic_attack && Dalay_Image.localScale.x == 0 && Item_2 != 0) { Item_D(2); }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !Magic_attack && Dalay_Image.localScale.x == 0 && Item_3 != 0) { Item_D(3); }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !Magic_attack && Dalay_Image.localScale.x == 0 && Item_4 != 0) { Item_D(4); }
        if (Input.GetKeyDown(KeyCode.Alpha5) && Dalay_Image.localScale.x == 0 && Item_5 != 0) { Item_D(5); }
        if (Input.GetKeyDown(KeyCode.F) && !UI.pause && !UI.Inv) { Magic_Active = false; Fire = false; Strong = false; Item_Off = true; }

        //
        if (Input.GetMouseButton(0) && Fire && Mana.localScale.x > 5 && !UI.pause && !UI.Inv && !Item_Off && !Stam)
        {
            //Magic_attack = true;
            UI.Regen_Mana = false;
            Player_Anim.SetBool("Fire_Spel",true);
            Player_Anim.SetBool("Idel_Magic",false);
            if (Fire_Particle_Tr.localScale.z == 0 && !F_L)
            {
                F_L = true;
                Invoke("Fire_Dalay", 1);
            }
            Fire_Particle[0].SetActive(false);
            Fire_Particle[1].SetActive(false);
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            Shoulder[0].transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            Shoulder[1].transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            if (Cam.transform.localPosition.z < -3) { Cam.transform.Translate(0, 0, 0.1f); }
        }
        else if ((Fire || Item_Off) && !UI.pause)
        {
            Player_Anim.SetBool("Fire_Spel", false);
            StopCoroutine("Mana_Fire");
            Magic_attack = false;
            StopCoroutine("Mana_Fire");
            StopCoroutine("Long_Fire");
            if (Mana.localScale.x < 0) { Mana.localScale = new Vector3(0, 0.1f, 1); }
            Fire_Particle2.SetActive(false);
            if (Fire_Particle_Tr.localScale.z > 0) { Fire_Particle_Tr.localScale = new Vector3(Fire_Particle_Tr.localScale.x, Fire_Particle_Tr.localScale.y, 0); }
            F_L = false;
            Fire_Particle2.SetActive(false);
            Fire_Particle[0].SetActive(true);
            Fire_Particle[1].SetActive(true);
            if (Cam.transform.localPosition.z > -5) { Cam.transform.Translate(0, 0, -0.1f); }
            StopCoroutine("Stam_cor");
        }

        if (Input.GetMouseButton(0) && Strong && Mana.localScale.x > 5 && !UI.pause && !UI.Inv && !Item_Off && !E_S.Strong_Attack)
        {
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            Magic_attack = true;
            Tel_Script.isActive = true;
            if (M_S)
            {
                M_S = true;
                StartCoroutine("Mana_Strong");
            }
            Player_Anim.SetBool("Strong_Sple_Up", true);
            Player_Anim.SetBool("Idel_Magic", false);
        }
        else if (Strong && !E_S.Strong_Attack && !E_S.Up_End)
        {
            Magic_attack = false;
            Tel_Script.isActive = false;
            Player_Anim.SetBool("Strong_Sple_Up", false);
            StopCoroutine("Mana_Strong");
        }
        else if (!Strong) { Tel_Script.isActive = false; }
        if (E_S.Up_End && Input.GetMouseButtonUp(0))
        {
            Player_Anim.SetBool("Strong_Sple_Up", false);
            Player_Anim.SetBool("Strong_Spel_Attack", true);
        }
        if (E_S.Strong_Attack) 
        {
            E_S.Up_End = false;
            Magic_attack = false;
            Tel_Script.isActive = false;
            Player_Anim.SetBool("Strong_Spel_Attack", false);
            Tel_Script.ready = true;
            E_S.Strong_Attack = false;
            w++;
        }
        ////Код для атаки катаной
        //if (Input.GetMouseButtonDown(0) && Sword && combo == 0 && !UI.pause && !UI.Inv && !Attack && Stamina.transform.localScale.x >= 5 && !Knock)
        //{
        //    Attack = true;
        //    StartCoroutine("Dalay");
        //    HandAnim.Play("Attack_Combo1");
        //    Cam_anim.Play("Rot_Combo1");
        //    Stamina.transform.localScale = new Vector3(Stamina.transform.localScale.x - 5, Stamina.transform.localScale.y, Stamina.transform.localScale.z);
        //    Sword_1_coll.enabled = false;
        //    Sword_2_coll.enabled = true;
        //}
        //else if (Input.GetMouseButtonDown(0) && Sword && combo == 1 && !UI.pause && !UI.Inv && !Attack && Stamina.transform.localScale.x >= 10)
        //{
        //    Attack = true;
        //    StartCoroutine("Dalay");
        //    HandAnim.Play("Attack_Combo2");
        //    Cam_anim.Play("Rot_Combo2");
        //    Stamina.transform.localScale = new Vector3(Stamina.transform.localScale.x - 10, Stamina.transform.localScale.y, Stamina.transform.localScale.z);
        //    Sword_2_coll.enabled = true;
        //    if (Input.GetKeyDown(KeyCode.W))
        //    { rb.AddForce(10000 * transform.forward * Time.deltaTime); }

        //}
        //else if (Input.GetMouseButtonDown(0) && Sword && combo == 2 && !UI.pause && !UI.Inv && !Attack && Stamina.transform.localScale.x >= 20)
        //{
        //    Attack = true;
        //    StartCoroutine("Dalay");
        //    StartCoroutine("Combo_Last");
        //    HandAnim2.SetBool("Attack", true);
        //    Cam_anim.Play("Rot_Combo3");
        //    Stamina.transform.localScale = new Vector3(Stamina.transform.localScale.x - 20, Stamina.transform.localScale.y, Stamina.transform.localScale.z);
        //    Sword_1_coll.enabled = true;
        //    Sword_2_coll.enabled = false;
        //}
        //передвежение
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        if (xMov != 0 || zMov != 0 && !UI.pause) { IsMove = true; } else if (xMov == 0 && zMov == 0) { IsMove = false; }

        if (Fire) 
        {
            Fire_Particle[1].SetActive(true); 
            Fire_Particle[0].SetActive(true);
            Mana_Stam.SetActive(true);
        } else {
            Fire_Particle[1].SetActive(false); 
            Fire_Particle[0].SetActive(false); 
            Mana_Stam.SetActive(false); }
        if (Strong) 
        {
            Stron_Particle[1].SetActive(true); 
            Stron_Particle[0].SetActive(true);
        } else {
            Stron_Particle[1].SetActive(false);
            Stron_Particle[0].SetActive(false); }
        if (Magic_attack)
        {
            Player_Anim.SetBool("Run", false);
            Player_Anim.SetBool("Walk_Back", false);
            Player_Anim.SetBool("Walk_Right", false);
            Player_Anim.SetBool("Walk_Left", false);
        }
    }
    //Item_Distribution
    void Fire_Dalay() 
    {
        StartCoroutine("Stam_cor");
        StartCoroutine("Long_Fire");
        StartCoroutine("Mana_Fire");
        Fire_Particle2.SetActive(true);
    }
    void Item_D(int i)
    {
        if (i == 1)
        {
            if (Item_1 == 1 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Magic_Active = true;
                Fire = true;
                Strong = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Item_Off = false;
            }
            if (Item_1 == 2 && Strong_Have && !Strong && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Magic_Active = true;
                Strong = true;
                Fire = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Item_Off = false;
            }
        }
        else if (i == 2)
        {
            if (Item_2 == 1 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Magic_Active = true;
                Fire = true;
                Strong = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Item_Off = false;
            }
            if (Item_2 == 2 && Strong_Have && !Strong && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Magic_Active = true;
                Strong = true;
                Fire = false;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Item_Off = false;
            }
        }
        else if (i == 3)
        {
            if (Item_3 == 1 && Sword_Have && !Sword && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Sword = true;
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_3 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
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
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_4 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
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
                Dalay_Image.localScale = new Vector3(5, 0.03f, 1);
                StartCoroutine("Dalay_Return");
                Fire = false;
            }
            else if (Item_5 == 2 && Fire_Have && !Fire && Dalay_Image.localScale.x == 0 && !Attack)
            {
                Fire = true;
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
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0 && !Magic_attack)
        {
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y + 45, 0);
            Player_Anim.SetBool("Run", true);
            Player_Anim.SetBool("Walk_Back", false);
            Player_Anim.SetBool("Walk_Right", false);
            Player_Anim.SetBool("Walk_Left", false);
            Player_Anim.SetBool("Idel_Rest", false);
            Player_Anim.SetBool("Idel_Magic", false); 
            if (!Run_Speed)
            {
                StartCoroutine("SpeedUp"); Run_Speed = true;
            }
        }
        else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0 && !Magic_attack)
        {
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y - 45, 0);
            Player_Anim.SetBool("Run", true);
            Player_Anim.SetBool("Walk_Back", false);
            Player_Anim.SetBool("Walk_Right", false);
            Player_Anim.SetBool("Walk_Left", false); 
            Player_Anim.SetBool("Idel_Rest", false);
            Player_Anim.SetBool("Idel_Magic", false);
            if (!Run_Speed)
            {
                StartCoroutine("SpeedUp"); Run_Speed = true;
            }
        }

        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0 && !Magic_attack)
        {
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            Player_Anim.SetBool("Run",true);
            Player_Anim.SetBool("Walk_Back", false);
            Player_Anim.SetBool("Walk_Right", false);
            Player_Anim.SetBool("Walk_Left", false);
            Player_Anim.SetBool("Idel_Rest", false);
            Player_Anim.SetBool("Idel_Magic", false);
            if (!Run_Speed) {
                StartCoroutine("SpeedUp"); Run_Speed = true; 
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0 && !Magic_attack) 
        {
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            Player_Anim.SetBool("Walk_Back", true);
            Player_Anim.SetBool("Run", false);
            Player_Anim.SetBool("Walk_Right", false);
            Player_Anim.SetBool("Walk_Left", false);
            Player_Anim.SetBool("Idel_Rest", false);
            Player_Anim.SetBool("Idel_Magic", false);
            StartCoroutine("SpeedUp");
            if (!Run_Speed)
            {
                StartCoroutine("SpeedUp"); Run_Speed = true;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && IsMove && !Magic_attack && Input.GetAxis("Vertical") > 0) { rb.MovePosition(rb.position + Cam.transform.forward * 1.5f * speed * Input.GetAxis("Vertical") * Time.deltaTime); Player_Anim.speed = 10; }
        else if (IsMove && !Magic_attack) { rb.MovePosition(rb.position + Cam.transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime); Player_Anim.speed = 1; }
        if (IsMove && !Magic_attack) { rb.MovePosition(rb.position + Cam.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime); Player_Anim.speed = 1;}
        if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0 && !Magic_attack)
        {
            Player_Anim.SetBool("Walk_Right", true);
            Player_Anim.SetBool("Walk_Left", false);
            Player_Anim.SetBool("Idel_Rest", false);
            Player_Anim.SetBool("Idel_Magic", false);
            Player_Anim.SetBool("Run", false);
            Player_Anim.SetBool("Walk_Back", false);
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            if (!Run_Speed)
            {
                StartCoroutine("SpeedUp"); Run_Speed = true;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0 && !Magic_attack)
        {
            Player_Anim.SetBool("Walk_Left", true);
            Player_Anim.SetBool("Walk_Right", false);
            Player_Anim.SetBool("Idel_Rest", false);
            Player_Anim.SetBool("Idel_Magic", false);
            Player_Anim.SetBool("Run", false);
            Player_Anim.SetBool("Walk_Back", false);
            Player_Obj.transform.localEulerAngles = new Vector3(0, Head.transform.localEulerAngles.y, 0);
            if (!Run_Speed)
            {
                StartCoroutine("SpeedUp"); Run_Speed = true;
            }
        }
        else if (!IsMove && !Magic_attack ) 
        {
            if (!Magic_Active)
            {
                Player_Anim.SetBool("Idel_Rest", true); Player_Anim.SetBool("Idel_Magic", false);
            }
            else { Player_Anim.SetBool("Idel_Magic",true); Player_Anim.SetBool("Idel_Rest", false); }
            Player_Anim.SetBool("Run", false);
            Player_Anim.SetBool("Walk_Back", false);
            Player_Anim.SetBool("Walk_Right", false);
            Player_Anim.SetBool("Walk_Left", false);
            Run_Speed = false;
            StopCoroutine("SpeedUp");
            speed = 0;
        }
    }
    IEnumerator Mana_Strong() 
    {
        if (Mana.localScale.x > 0) { Mana.localScale = new Vector3(Mana.localScale.x - 1f, Mana.localScale.y, Mana.localScale.z); }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Mana_Strong");
    }
    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(0.05f);
        if (speed < 5) {
            speed++;
        }
        StartCoroutine("SpeedUp");
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
        yield return new WaitForSeconds(0.8f);
        yield return new WaitForSeconds(0.85f);
        A_E_T.Combo_Last();
        A_E_T.Attack_Is_Fine();
    }
    //
    IEnumerator Mana_Fire()
    {
        if (Mana.localScale.x > 0) { Mana.localScale = new Vector3(Mana.localScale.x - 1f, Mana.localScale.y, Mana.localScale.z); }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Mana_Fire");
    }
    //Надо остонавливать следующюю куратину Mana_Can_Regen в куратине UI.Mana_Regen т.к из-за постоянного вызова 1-ой куратины она постоянно вызывала 2-ую куратину
    IEnumerator Mana_Regen()
    {
        if (!Magic_attack) {
            Mana.transform.localScale = new Vector3(Mana.localScale.x + 1f, 0.1f, 1); }
        yield return new WaitForSeconds(0.1f);
        w++;
        StartCoroutine("Mana_Regen");
    }
    IEnumerator Long_Fire()
    {
        if (Fire_Particle_Tr.localScale.z < 1)
        {
            Fire_Particle_Tr.localScale = new Vector3(Fire_Particle_Tr.localScale.x, Fire_Particle_Tr.localScale.y, Fire_Particle_Tr.localScale.z + 0.5f);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine("Long_Fire");
        }
    }
    IEnumerator Stam_cor()
    {
        if (Stam_Mana.localScale.x > 0) { 
        Stam_Mana.localScale = new Vector3(Stam_Mana.localScale.x - 5f, 0.1f, 1); }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Stam_cor");
    }
}