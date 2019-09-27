using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Missile : MonoBehaviour
{
    float delay = 1.0f;
    //[SerializeField] Transform sprite;
    [SerializeField] public Transform target;
    [SerializeField] public GameObject bulletPrefab;
    private Rigidbody2D targetRB;
    [SerializeField] Rigidbody2D m_RigidBody2d;
    [SerializeField] private float trRange = 5f;
     private float activeRange = 80000f;
    [SerializeField] private float m_speed = 2f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private int role = 0;
    [SerializeField] private float timeToLive = 25f;
    [SerializeField] private float timeToShoot = 20f;
    [SerializeField] private float effectDuration = 3f;
    private bool _shot = false;
    private float _lifeTime= 0f;
    private int type = 0;
    float m_SmoothTime = 0.7f;

    Vector2 refVel = Vector2.zero;

    //private Vector2 trg;//Point to go
    //private Vector2 refTrg;//Ref to pull
    //private float smoothTrg=0.1f;//Ref to pull

    private float refRange;
    [SerializeField] private float smoothRange=0.1f;//Ref to pull
    [SerializeField] private float rangePushMul = 2;
    [SerializeField] float _range;
    [SerializeField] float range;
    
    private float refTanVel;
    [SerializeField] private float smoothTanVel=0.5f;//Ref to pull    
    [SerializeField] float _tanVel;
    [SerializeField] float tanVel;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if(!_animator)Debug.Log("NOE ENEMI ANIMATOR!!!");
        _animator.SetBool("Enemy1",type != 0);
        
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 30);
        foreach (Collider2D coll in colls)
        {
            if (coll.CompareTag("Intro"))
            {
                Debug.Log("Found Intro " + coll.name);
                Intro intro = coll.gameObject.GetComponent<Intro>();
                if (intro) Debug.Log("Really found intro " + intro.name);
                intro.addEnemy(m_RigidBody2d.gameObject);

                //target = coll.transform;
                break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

    }

    void OnTrigerEnter(Collider2D col)
    {

    }

    public void setType(int typ, int role=0)
    {
        //Debug.Log("EN_SET_TYPE");
        type = typ;
        role = role;
        //if(!_animator)_animator = gameObject.GetComponent<Animator>();
        if(!_animator)Debug.Log("NOE ENEMI ANIMATOR!!!");
        
        _animator.SetBool("Enemy1",type != 0);
        
    }
    
    public void setTarget(Transform tr)
    {
        Debug.Log("Target set to " + tr.name);
        target = tr;
        targetRB = target.GetComponent<Rigidbody2D>();
    }

    void shoot(int type)
    {
        if (bulletPrefab)
        {
            GameObject _bullet = Instantiate(bulletPrefab, (Vector3) transform.position,
                Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y - transform.position.y,target.transform.position.x - transform.position.x)));
            _bullet.GetComponent<Bullet>().setUp(type,effectDuration);
            _bullet.GetComponent<Rigidbody2D>().velocity = (target.position - transform.position).normalized*bulletSpeed;

        }
        Debug.Log("PeW pEw!!!");
    }
    // Update is called once per frame
    
    void FixedUpdate()
    {
        _lifeTime += Time.fixedDeltaTime;

        if (!_shot && _lifeTime > timeToShoot)
        {
            shoot(type);
            _shot = true;
        }

        if (_lifeTime > timeToLive)//Time to die
        {
            m_RigidBody2d.velocity = (transform.position - target.position).normalized * m_speed;
            if (Mathf.Abs(transform.position.x)>4)
            {
                Debug.Log("no no noNO nO NO NONONO!!");
                Destroy(gameObject);
            }
        }
        else//We're alive
        {
            if (target)
            {
                if (delay >= 0) delay -= Time.fixedDeltaTime;

                _range = (target.position - transform.position).magnitude;
                range = Mathf.SmoothDamp(_range, trRange, ref refRange, smoothRange);

                tanVel = Mathf.SmoothDamp(tanVel, Random.Range(-m_speed / 2, m_speed / 2) * m_speed*10, ref refTanVel,
                    smoothTanVel);
                if (delay <= 0 && _range <= activeRange)
                {
                    //Debug.Log("BOO");
                    //Debug.Log(_range < trRange);
                    //Debug.Log((Vector2) (transform.position - target.position) * Mathf.Abs(trRange - _range) *
                             // rangePushMul);
                    //Debug.Log((Vector2) (target.position - transform.position) * m_speed);


                    m_RigidBody2d.velocity = (_range < trRange //Keep the range
                                                 ? (Vector2) (transform.position - target.position).normalized *
                                                   Mathf.Abs(trRange - _range) *
                                                   rangePushMul // closer vector from player to enemy, closer-stronger
                                                 : (Vector2) (target.position - transform.position).normalized *
                                                   m_speed)
                                             + Vector2.Perpendicular((Vector2) (target.position - transform.position))
                                                 .normalized *
                                             tanVel; //Vector2.Perpendicular((Vector2) (transform.position-target.position)).normalized * Random.Range(-m_speed/2,m_speed/2); // further vector from enemy to player , using speed

                    //m_RigidBody2d.AddForce(trg);
                }

                //if(transform)transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Acos(Vector3.Dot(trg,Vector3.right))*Mathf.Rad2Deg));
            }
        }
    }
}
