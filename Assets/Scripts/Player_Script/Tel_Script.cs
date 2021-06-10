using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tel_Script : MonoBehaviour
{
    public int Max_Cell;
    public int Cell = 5;
    public bool isActive;
    public bool ready;
    public Transform Transform_Cam;
    public bool Reload;
    private void Update()
    {
        if (Cell >= 1 && Reload) { StartCoroutine("Dalay"); Reload = false; }
    }
    IEnumerator Dalay() 
    {
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        transform.localScale = new Vector3(1, 1, 1);
    }
}