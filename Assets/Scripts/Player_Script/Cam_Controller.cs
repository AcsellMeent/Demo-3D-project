using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cam_Controller : MonoBehaviour
{
    public UI_Manager UI;
    public float xSpeed;
    public float ySpeed;
    public float Sens_Y;
    public float Sens_X;
    private float Hand_Speed_x;
    public GameObject Player;
    public Player_Controller Player_Controller;
    public GameObject Head;
    public GameObject Head2;
    public GameObject Hand;
    public GameObject Right_Hand;
    public GameObject Left_Hand;
    public GameObject S_hand;
    public bool Cam2Bool;
    public float Y_Engel;
    public Animation Animation;
    public Transform Head_Tr;
    public LayerMask maskObstacl;
    public Vector3 StarPostition;
    public Transform BackPoinnt;

    void Update()
    {
        RaycastHit Hit;
        if (Physics.Raycast(Head_Tr.position, BackPoinnt.position - Head_Tr.position, out Hit, Vector3.Distance(BackPoinnt.position, Head_Tr.position), maskObstacl) && Hit.transform.gameObject.isStatic && Vector3.Distance(BackPoinnt.position,Head_Tr.position) < 5)
        {
            if (Vector3.Distance(Hit.point,Head_Tr.position) < 3) {
                transform.position = new Vector3(Hit.point.x, Hit.point.y, Hit.point.z); }
            print(Vector3.Distance(Hit.point, Head_Tr.position));
        }
        else if(!Player_Controller.Magic_attack) { transform.localPosition = StarPostition; }
        Sens_X = UI.Sens_Slider.GetComponent<Slider>().value * 5;
        Sens_Y = UI.Sens_Slider.GetComponent<Slider>().value * 20;
        if (!UI.pause && !UI.Inv) {
            //Координаты мыши по осям x и y
            ySpeed = Input.GetAxis("Mouse X") * Sens_Y;
            xSpeed -= Input.GetAxis("Mouse Y") * Sens_X;
            Hand_Speed_x -= Input.GetAxis("Mouse Y") * Sens_X;
            //Ограничение врашения по оси x
            xSpeed = Mathf.Clamp(xSpeed, -55, 45);

            //Присвоения координат мыши к координатам врашения объектов player и Head 
            Head.transform.Rotate(0, ySpeed, 0);
            Head2.transform.localEulerAngles = new Vector3(xSpeed,Head2.transform.localEulerAngles.y,Head2.transform.localEulerAngles.z);
            if (!Player_Controller.Sword) {
                Hand_Speed_x = Mathf.Clamp(Hand_Speed_x, -55, 45);
                Right_Hand.transform.localEulerAngles = new Vector3(xSpeed, 0, 0);
                Left_Hand.transform.localEulerAngles = new Vector3(xSpeed, Left_Hand.transform.rotation.y, Left_Hand.transform.rotation.z);
            }
            else { S_hand.transform.localEulerAngles = new Vector3(xSpeed, Left_Hand.transform.rotation.y, Left_Hand.transform.rotation.z); }
        }
    }
}
