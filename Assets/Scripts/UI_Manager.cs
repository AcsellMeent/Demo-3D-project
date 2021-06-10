using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public bool pause = false;
    public bool Inv = false;
    public bool Regen = true;
    public bool Regen_Mana;
    public GameObject Sens_Slider;
    public GameObject Exit_Button;
    public GameObject Exit_Button2;
    public GameObject Right;
    public GameObject Left;
    public Image R;
    public Image L;
    public Text Text_menu;
    public Image UI_Curs;
    public GameObject stamina;
    public GameObject Mana;
    public GameObject Inv_Bar;
    public GameObject Dalay;
    public GameObject die;
    public Player_Controller P_C;
    public bool g;

    void Start()
    {
        R = Right.GetComponent<Image>();
        L = Left.GetComponent<Image>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && Time.timeScale == 1f) { Time.timeScale = 0.05f; } else if (Input.GetKeyDown(KeyCode.T)) { Time.timeScale = 1f; }
        if (Input.GetKeyDown(KeyCode.Escape) && !pause && !Inv)
        {
            pause = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            Text_menu.enabled = true;
            Sens_Slider.SetActive(true);
            Exit_Button.SetActive(true);
            UI_Curs.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause && !Inv)
        {
            pause = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            Text_menu.enabled = false;
            Sens_Slider.SetActive(false);
            Exit_Button.SetActive(false);
            UI_Curs.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !Inv && !pause)
        {
            Inv = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            Inv_Bar.SetActive(true);
            UI_Curs.enabled = false;
        }
        else if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && Inv)
        {
            Inv = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            Inv_Bar.SetActive(false);
            UI_Curs.enabled = true;
        }
    }
    public void Exit1(float mode)
    {
        if (mode == 1)
        {
            Exit_Button2.SetActive(true);
        }
        if (mode == 2)
        {
            Application.Quit();
        }
        if (mode == 3)
        {
            Exit_Button2.SetActive(false);
        }
    }
    IEnumerator Stam_Regen()
    {
        if (stamina.transform.localScale.x < 100 && Regen && !P_C.Attack)
        {
            yield return new WaitForSeconds(P_C.Speed_Regen_Stam);
            stamina.transform.localScale = new Vector3(stamina.transform.localScale.x + 1, stamina.transform.localScale.y, stamina.transform.localScale.z);
            StartCoroutine("Stam_Regen");
        }
    }
}
