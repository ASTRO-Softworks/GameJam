using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private SpriteRenderer SR1;
    [SerializeField] private Text text;



    private float m_MovementSmoothing = 0.7f;
    Camera self;
    
    Vector3 desLoc = 10 * Vector3.back;
    Vector3 refLoc;

    // Start is called before the first frame update
    void Start()
    {
        //refLoc = transform.position;
        self = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.F1))
        {
            self.orthographicSize += .01f;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            self.orthographicSize -= .01f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            self.transform.Translate(new Vector3(0, .1f, 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            self.transform.Translate(new Vector3(0, -.1f, 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            self.transform.Translate(new Vector3(.1f, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            self.transform.Translate(new Vector3(-.1f, 0, 0));
        }

    }




    void FixedUpdate()
    {
        desLoc = new Vector3(0, player.position.y > -2.84f ? player.position.y : -2.84f, -10);
        transform.position = Vector3.SmoothDamp(transform.position, desLoc, ref refLoc, m_MovementSmoothing);
        /*
        if (transform.position.y >= 12.62f)
        {
            text.color = new Color(0, 0, 0, (transform.position.y - 14.62f) / 2);
            SR.color = new Color(1, 1, 1, (transform.position.y - 14.62f) / 2);
        }
        */
        
        if (transform.position.y >= 12.62f)
        {
            //text.color = new Color(0, 0, 0, (transform.position.y - 16.30f) / 1);
            SR.color = new Color(1, 1, 1, (16.30f-transform.position.y) / 1);
            SR1.color = new Color(1, 1, 1, (16.30f - transform.position.y) / 1);
        }
        else
        {
            SR.color = new Color(1,1,1,1);
        }
    }

}
