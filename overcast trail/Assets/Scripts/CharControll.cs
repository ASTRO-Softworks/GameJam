using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControll : MonoBehaviour
{


    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator animator;
    //[SerializeField] private SoundControl SC;
    [SerializeField] private Collider2D BaseCol;
    [SerializeField] private Missile EnemyPrefab;

    private Vector3 InitLoc;

    private Vector2 targetVelocity;

    public float runSpeed = .2f;
    private Vector2 m_Velocity = Vector2.zero;
    private float m_MovementSmoothing = 0.5f;
    public float jumpDuration = 0.5f;
    public float preDeathTimer = 0;
    public float preDeathDuration = 0.0f;
    public float jumpDelay = 0.2f;
    public float jumpCut= 0.2f;

    public bool makeEnemies = false;
    public float secPerEnemy = 1;
    public float secToFirstEnemy = 0;
    private float secToEnemy = 0;
    
    
    float jumpCycle = 0f;
    
    float horizontalMove = 0f;
    float verticalMove = 0f;
    float sprintMul = 5.0f;
    float pushForce = 50.0f;
    float mouseX = 0f;
    float mouseY = 0f;
    bool doomed = false;
    bool safe = false;
    bool sprint = false;
    bool jumpCheat = false;
    //bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        InitLoc = transform.position;
        secToEnemy = secToFirstEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (makeEnemies)
        {
            if (secToEnemy > 0) secToEnemy -= Time.deltaTime;
            else
            {
                CreateEnemy(Random.Range(-1f,1f)>0?1:0);
                secToEnemy = secPerEnemy;
            }
        }

        horizontalMove = Input.GetAxisRaw("Horizontal");

        verticalMove = Input.GetAxisRaw("Vertical");

        //if (sprint) Debug.Log("Sprint");

        mouseX = Input.GetAxisRaw("Mouse X");

        mouseY = Input.GetAxisRaw("Mouse Y");

        if (verticalMove > 0) animator.SetInteger("Dir", 0);
        if (verticalMove < 0) animator.SetInteger("Dir", 2);
        if (horizontalMove > 0) animator.SetInteger("Dir", 1);
        if (horizontalMove < 0) animator.SetInteger("Dir", 3);

        if(verticalMove==0 && horizontalMove==0) animator.SetInteger("Dir", 4);

        targetVelocity = new Vector2(horizontalMove * runSpeed * (sprint?sprintMul:1), verticalMove * runSpeed * (sprint ? sprintMul : 1));




        //Debug.Log(horizontalMove + " " + verticalMove);

        if (Input.GetButtonDown("Jump")&&(jumpCheat||jumpCycle<=0f))
        {
            animator.SetBool("Jump", true);
            jumpCycle = jumpDuration;
            //BaseCol.isTrigger = true;
            //SC.TogglePlay();
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            sprint = true;
        } else if (Input.GetButtonUp("Fire1"))
        {
            sprint = false;
        }
    }

    void FixedUpdate()
    {
        if (jumpCycle > 0){
            if((jumpDuration - jumpCycle)>jumpDelay)BaseCol.isTrigger = true;
            if(jumpCycle<jumpCut)BaseCol.isTrigger = false;
            jumpCycle -= Time.fixedDeltaTime;
        }
        else
        {
            BaseCol.isTrigger = false;
            animator.SetBool("Jump", false);
            
        }
        if (doomed && !safe && !BaseCol.isTrigger)
        {
                
            if (preDeathTimer > 0) preDeathTimer -= Time.fixedDeltaTime;
            else {Die();}
        }
        //Debug.Log(jumpCycle);
        
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    void Die()
    {
        transform.position = InitLoc;
    }

    void CreateEnemy(int type)
    {
        //makeEnemies = false;
        type = 0;
        float vRange = 3f;
        float hRange = 3f;
        Missile enemy = Instantiate(EnemyPrefab, new Vector3( transform.position.x+Random.Range(0f,1f)>.5f?hRange:-hRange,Random.Range(-vRange,vRange),0 ),Quaternion.Euler(0,0,0) );
        enemy.setType(type);
        enemy.setTarget(gameObject.transform);
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Lv1Border"))
        {
            doomed = true;            
        } else if (collider.CompareTag("Warp"))
        {
            //Vector3.Scale((collider.transform.position - transform.position), new Vector3(1.5f, 0, 0));
            //((collider.transform.position - transform.position).Scale(new Vector3(1.5f,0,0)))
            Vector3 offset = Vector3.Scale((collider.transform.position - transform.position), new Vector3(transform.position.x>0?-1.7f:1.7f, 0, 0));
            transform.position = collider.gameObject.GetComponent<Warp>().getDest().position + new Vector3(0, 0.12f, 0) + offset;
            m_Rigidbody2D.velocity += Vector2.Scale(offset,new Vector2(1,0));
        } else if (collider.CompareTag("Checkpoint"))
        {
            Checkpoint CP = collider.gameObject.GetComponent<Checkpoint>();
            InitLoc = CP.Check();
            //Debug.Log("CHECKPOINT");
        } else if (collider.CompareTag("Enemy"))
        {
            m_Rigidbody2D.AddForce((transform.position - collider.transform.position) * pushForce);
            Destroy(collider.gameObject);
        } else if (collider.CompareTag("SafeZone"))
        {
            safe = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Lv1Border"))
        {
            doomed = false;
        } else if (collider.CompareTag("SafeZone"))
        {
            safe = false;
        }
    }

    public void setSpawn(Vector3 point)
    {
        InitLoc = point;
    }

}
