using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Trigger : MonoBehaviour
{
    public string i;
    public UI_Manager UI;
    public Player_Controller P_C;
    private void OnTriggerEnter(Collider other)
    {
        i = other.gameObject.tag;
        if (other.gameObject.tag == "Enemy_Sword" && other.gameObject.GetComponent<Sword_Enemy>().Attack && P_C.Damage)
        {
            P_C.Damage = false;
            P_C.Health.transform.localScale = new Vector3(P_C.Health.transform.localScale.x - Random.Range(20, 30), P_C.Health.transform.localScale.y, P_C.Health.transform.localScale.z);
            other.gameObject.GetComponent<Sword_Enemy>().Attack = false;
            if (P_C.Health.transform.localScale.x <= 0) { P_C.Health.SetActive(false); UI.die.SetActive(true); P_C.Die = true; }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy_Sword") { P_C.Damage = true; }
    }
}
