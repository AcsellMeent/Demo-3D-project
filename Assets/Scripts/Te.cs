using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Te : MonoBehaviour
{
    public NavMeshAgent Nav;
    void Update()
    {
        Nav.SetDestination(new Vector3(-50,transform.position.y,transform.position.z));
    }
}
