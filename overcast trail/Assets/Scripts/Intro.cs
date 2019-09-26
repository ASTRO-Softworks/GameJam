using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    [SerializeField] CameraController ctr;
    [SerializeField] SpriteRenderer text;
    GameObject go;
    float timer = 2.0f;
    bool begin = false;
    bool end = false;
    public List<Missile> enemys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !end)
        {
            animator.SetBool("Begin", true);
            begin = true;
        }
    }

    void FixedUpdate()
    {
        if (begin&&!end)
        {
            if (timer < 0)//Here we go
            {
                if (!end)
                {
                    Debug.Log("Started");
                    Destroy(gameObject);
                    Vector3 off = new Vector3(0.21f, 0, 0);
                    GameObject go = Instantiate(player, transform.position + off, Quaternion.Euler(Vector3.zero));
                    ctr.player = go.transform;
                    foreach (Missile m in enemys)
                    {
                        m.setTarget(go.transform);
                        //m.target = go.transform;
                    }
                    
                    end = true;
                    gameObject.GetComponent<SpriteRenderer>().color+=new Color(0,0,0,0);
                    //Destroy(gameObject);
                }
                
            }
            else
            {
                text.color -= new Color(0, 0, 0, 0.01f);
                timer -= Time.fixedDeltaTime;
            }
        }
    }

    public void addEnemy(GameObject enemy)
    {
        if (!enemy) Debug.Log("WTFFFF!!!!!");
        Debug.Log("Adding enemy " + enemy.name);
        Missile m = enemy.GetComponent<Missile>();
        if (m) enemys.Add(m);
    }
}
