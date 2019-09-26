using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBehaviour : MonoBehaviour
{

    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private Text text;
    int i = 0;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            i--;
        } else if (Input.GetKeyDown(KeyCode.F4))
        {
            i++;
        }
    }

    void FixedUpdate()
    {
        
        //text.text = i.ToString();
        //text.
    }
        //GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);

       void OnMouseEnter()
    {
        rend.material.color = Color.red;
    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseOver()
    {
        rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }


}
