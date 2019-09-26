using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    [SerializeField]private Transform dest;
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Saw a " + collider.gameObject.name);
            //Vector3 offset = (collider.transform.position - transform.position);
            //collider.transform.position = dest.position + new Vector3(0, -0.12f, 0) + (offset * 10);
        }
        

    }
    public Transform getDest()
    {
        return dest;
    }
}
