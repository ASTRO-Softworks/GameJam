using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private int _type;
    [SerializeField]private float _duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform tr= other.transform.parent;
        
        CharControll CC;// = other.transform.parent.gameObject.GetComponent<CharControll>();
        if(transform)CC=tr.gameObject.GetComponent<CharControll>();
        else
        {
            CC=other.transform.gameObject.GetComponent<CharControll>();
        }
        if (CC)
        {
            Debug.Log("ENTER "+other.name);
            if (_type==0)
            {
                CC.slowDown(_duration);
            }
            else
            {
                CC.blind(_duration);
            }
            Destroy(gameObject);
        }
    }

    public void setUp(int type, float duration)
    {
        _type = type;
        _duration = duration;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x)>4)Destroy(gameObject);
    }
}
