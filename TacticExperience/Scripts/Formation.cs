using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;
using System.Linq;


public class Formation : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    public enum Form
    {
        TwoLines,
        Tortule,
        Sektor,
        Triangle
    }

    //public GameObject go;
    private Camera mainCamera;
    public Vector3 target ;
    public Form form;
    public bool isGood = true;

    //private Vector2 vector2;
    public int Health;
    public int Armor;
    public int Attack;

    private NavMeshAgent _agent;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, Action>();

    private string EnemyTag;
    public string Name;
    // Start is called before the first frame update
    void KeywordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
    void Start()
    {
        target = transform.position;
        
        keywords.Add(Name +" " + "foward", Forvard );
        keywords.Add(Name +" "+ "right", Right);
        keywords.Add(Name +" "+ "left", Left);
        keywords.Add(Name +" "+ "back", Back);
        keywords.Add(Name +" "+ "righter",()=>
        {
            Foward_Right();
        });
        keywords.Add(Name +" "+ "lefter", ()=>
        {
            Foward_Left();
        });
        keywords.Add(Name +" "+ "Attack", ()=>
        {
            Attack_Forward();
        });
        keywords.Add(Name +" "+ "Change to Turtle", ()=>
        {
            form = Form.Tortule; 
            SetArmy();
        });
        keywords.Add(Name +" "+ "Change to Triangle", ()=>
        {
            form = Form.Triangle; 
            SetArmy();
        });
        keywords.Add(Name +" "+ "Change to Lines", ()=>
        {
            form = Form.TwoLines; 
            SetArmy();
        });
        keywords.Add(Name +" "+ "Change to Sektor", ()=>
        {
            form = Form.Sektor; 
            SetArmy();
        });
        
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnPhraseRecognized;
        keywordRecognizer.Start();

        time = Time.time;
        SetArmy();

        if (isGood)
        {
            gameObject.tag = "Player";
            EnemyTag = "Enemy";
        }
        else
        {
            gameObject.tag = "Enemy";
            EnemyTag = "Player";
            
        }
        mainCamera = Camera.main;
        //  target = go.GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void SetArmy()
    {
        int i, j, k, n;
        switch (form)
        {
            case Form.Tortule:
                Attack = 3;
                Armor = 5;
                gameObject.GetComponent<BoxCollider>().size = new Vector3(7, 0, 7);
                i = -3;
                j = -3;
                k = 0;

                foreach (Transform child in transform)
                {
                    child.localPosition = new Vector3(i, 0, j);
                    i += 2;
                    k++;
                    if (k == 4)
                    {
                        k = 0;
                        i = -3;
                        j += 2;
                    }
                }

                break;
            case Form.TwoLines:
                gameObject.GetComponent<BoxCollider>().size = new Vector3(13, 0, 4);

                Attack = 4;
                Armor = 4;
                i = -7;
                j = -1;
                k = 0;
                foreach (Transform child in transform)
                {
                    child.localPosition = new Vector3(i, 0, j);
                    i += 2;
                    k++;
                    if (k == 8)
                    {
                        k = 0;
                        i = -7;
                        j += 2;
                    }
                }

                break;
            case Form.Sektor:
                gameObject.GetComponent<BoxCollider>().size = new Vector3(11, 0, 7);

                Attack = 6;
                Armor = 2;
                i = -5;
                j = -1;
                k = 0;
                foreach (Transform child in transform)
                {
                    if (k < 12)
                    {
                        child.localPosition = new Vector3(i, 0, j);
                        i += 2;
                        if (i == 7)
                        {
                            i = -5;
                            j += 2;
                        }
                    }
                    else if (k == 12)
                    {
                        child.localPosition = new Vector3(-5, 0, j);
                    }
                    else if (k == 13)
                    {
                        child.localPosition = new Vector3(5, 0, j);
                    }
                    else if (k == 14)
                    {
                        child.localPosition = new Vector3(-7, 0, j - 2);
                    }
                    else if (k == 15)
                    {
                        child.localPosition = new Vector3(7, 0, j - 2);
                    }

                    if (k == 9)
                    {
                        child.localPosition = new Vector3(-1, 0, j - 4);
                    }

                    if (k == 8)
                    {
                        child.localPosition = new Vector3(1, 0, j - 4);
                    }

                    k++;
                }

                break;
            case Form.Triangle:
                gameObject.GetComponent<BoxCollider>().size = new Vector3(9, 0, 7);
                Attack = 5;
                Armor = 3;

                i = -6;
                j = -3;
                k = 0;
                n = 7;
                foreach (Transform child in transform)
                {
                    child.localPosition = new Vector3(i, 0, j);
                    i += 2;
                    k++;
                    if (k == n)
                    {
                        n -= 2;
                        k = 0;
                        i = -n + 1;
                        j += 2;
                    }
                }

                break;
        }

        Health += Armor;
    }

    public void Damage(int d)
    {
        Health -= d;
        Destroy(gameObject,1.5f);
        isFight = false;

    }

    private bool isFight = false;
    private float time;
    private float old_speed;

    
    void OnTriggerEnter(Collider other)
    {
        if(other == null) return;
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Formation>().isGood ^ isGood)
            {
                isFight = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == null) return;
        if (isFight)
        {
            
            old_speed = GetComponent<NavMeshAgent>().speed;
            GetComponent<NavMeshAgent>().speed = 0;
                

            if (Time.time - time >= 3)
            {

                time = Time.time;
                target = other.gameObject.transform.position;
                foreach (Transform child in transform)
                {
                    child.GetComponent<Animator>().SetBool("isFight", true);
                    child.GetComponent<Animator>().SetInteger("HP", Health);
                }

                other.gameObject.GetComponent<Formation>().Damage(Attack);


                if (!other.gameObject.GetComponent<Formation>().isFight)
                {
                    isFight = false;
                  //  GetComponent<NavMeshAgent>().speed = old_speed;
                }
            }
        }   



    }

    

    void FindEnemy(Vector3 center, float radius) { 
        Collider[] hitColliders = Physics.OverlapSphere(center, radius); 
        foreach (var hitCollider in hitColliders) 
        { 
            if (hitCollider.gameObject.CompareTag(EnemyTag)) 
            { 
                target = hitCollider.gameObject.transform.position; 
                Debug.Log("HIT"); 
            } 
        } 
    } 


    void Attack_Forward(){ 
        RaycastHit hit; 
        _agent.speed = 10;
        Physics.Raycast(transform.position + transform.forward*transform.localScale.x, transform.forward,out hit); 
        if (hit.collider.gameObject.CompareTag(EnemyTag)) 
            target = hit.collider.gameObject.transform.position; 
    } 

    void Forvard() 
    { 
        _agent.speed = 10;
        target = transform.position + transform.forward * Mathf.Infinity; 
    } 
    void Back() 
    { 
        _agent.speed = 10;
        target = transform.position -transform.forward * Mathf.Infinity; 
    } 
    void Right() 
    { 
        _agent.speed = 10;
        target = transform.position + transform.right * Mathf.Infinity; 
    } 
    void Left() 
    { 
        _agent.speed = 10;
        target = transform.position - transform.right * Mathf.Infinity; 
    } 
    void Foward_Left() 
    { 
        _agent.speed = 10;
        target = transform.position - transform.right * Mathf.Infinity + transform.forward * Mathf.Infinity; 
    } 
    void Foward_Right() 
    { 
        _agent.speed = 10;
        target = transform.position + transform.right * Mathf.Infinity + transform.forward * Mathf.Infinity; 
    } 
    void Back_Left() 
    { 
        _agent.speed = 10;
        target = transform.position - transform.right * Mathf.Infinity - transform.forward * Mathf.Infinity; 
    } 
    void Back_Right()
    {
        _agent.speed = 10;
        target = transform.position + transform.right * Mathf.Infinity - transform.forward * Mathf.Infinity; 
    } 
    // Update is called once per frame
    void Update()
    {


        if (target != transform.position && _agent.speed!= 0)
        {
            _agent.SetDestination(target);

        }
    }
}