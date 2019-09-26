using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 mouse = Vector2.zero;
    float radius = 0.02f;
    
    Transform tr;

    bool flag  = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(flag) tr.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDown()
    {
        Dragable obj = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), radius).gameObject.GetComponent<Dragable>();
        if(obj && obj.CanBeDragged())
        {
            tr = obj.GetTransform();
            flag = true;
        }
        else flag = false;
        Debug.Log("MouseDown");
    }

    void OnMouseUp()
    {
        flag = false;
    }

}
