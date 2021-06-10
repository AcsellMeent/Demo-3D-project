using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_System : MonoBehaviour
{
    public bool Up_End;
    public bool Strong_Attack;
    void Up_End_Func()
    {
        Up_End = true;
        //if (Up_End) { Up_End = false; }
    }
    void S_A ()
    {
        Strong_Attack = true;
    }
}
