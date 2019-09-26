using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] SpriteRenderer Unchecked;
    [SerializeField] SpriteRenderer Checked;
    [SerializeField] Transform point;

    bool b_checked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (b_checked) Checked.color += new Color(0, 0, 0, .01f);
        if (!b_checked) Checked.color -= new Color(0, 0, 0, .01f);
        Checked.color = Checked.color.a >1?new Color(1,1,1,1): Checked.color.a <0?new Color(1,1,1,0): Checked.color;
        //Checked.color.
    }

    public Vector3 Check()
    {
        b_checked = true;
        return point.position;
    }

    public void Uncheck()
    {
        b_checked = false;
    }

}
