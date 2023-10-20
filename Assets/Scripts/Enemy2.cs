using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public int health = 5;
    public Rigidbody rb;
    public GameObject blood, Bow, arrow;
    public Transform bow;
    public float ASpeed;

    //Patroling
    public Vector3 WalkPoint;
    public bool WalkPointSet;
    public float WalkPointRange;

    //Attacking
    public float TimeBetweenAttacks = 1f;
    public bool alreadyAttacked;

    //Stats
    public float SightRange, AttackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        bow = transform.GetChild(0);
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        //Check for Sight and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        RaycastHit hit;
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange)
        {
            if (Physics.Raycast(transform.position, playerDirection, out hit, SightRange))
            {
                if (hit.collider.gameObject.name == player.name)
                {

                    ChasePlayer();
                }
                else
                {
                    Patrolling();
                }
            }
            else
            {
                Patrolling();
            }
        }
        if (playerInSightRange && playerInAttackRange)
        {
            if (Physics.Raycast(transform.position, playerDirection, out hit, SightRange))
            {
                if (hit.collider.gameObject.name == player.name)
                {

                    AttackPlayer();
                }
                else
                {
                    Patrolling();
                }
            }
            else
            {
                Patrolling();
            }
        }
        Ded();
    }

    void Patrolling()
    {
        if (!WalkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(WalkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        //WalkPoint
        if (distanceToWalkPoint.magnitude < 1f)
        {
            WalkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        RaycastHit hit;
        Vector3 Dis = WalkPoint - transform.position;
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround) && !Physics.Raycast(transform.position, Dis.normalized, out hit, Dis.magnitude - 0.01f))
        {
            if (hit.collider == null)
            {
                WalkPointSet = true;
            }

        }
    }
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here

            Attack();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);

        }

    }

    public void ResetAttack()
    {
        alreadyAttacked = false;

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Bullet")
        {
            health--;
        }
        if (other.collider.tag == "Sword")
        {
            health -= 2;
        }
    }

    public void Ded()
    {
        if (health <= 0 || transform.position.y <= -30)
        {
            GameObject bloodSplit = Instantiate(blood, transform.position, transform.rotation);
            Destroy(bloodSplit, 2f);
            Destroy(this.gameObject);
        }

    }

    void Attack()
    {
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(transform.position, player.position);
        float angleRad = Mathf.Asin((distance * 9.81f)/Mathf.Pow(ASpeed, 2))/2;
        float angleDeg = Mathf.Rad2Deg * angleRad;
        float OffSetRad = Mathf.Atan(-0.325f / distance);
        float OffSetDeg = Mathf.Rad2Deg * OffSetRad;
        Quaternion angleQuat = Quaternion.Euler(new Vector3(-angleDeg, -OffSetDeg, 0) + transform.eulerAngles);
        GameObject arr = Instantiate(arrow, transform.GetChild(0).position, angleQuat);        
        arr.GetComponent<Rigidbody>().velocity = arr.transform.forward * ASpeed;
    }

    
}
