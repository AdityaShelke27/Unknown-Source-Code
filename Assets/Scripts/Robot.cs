using UnityEngine;

public class Robot : MonoBehaviour
{
    public Transform Head, bulletSpawn;
    public GameObject DestroyEffect;
    public GameObject[] Enemies;
    public GameObject bullet;
    private GameObject MainEnemy;
    public float range;
    public float ShootingTime;
    public float health = 10;
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    void UpdateTarget()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;       
        GameObject nearestEnemy = null;
        foreach (GameObject Enemy in Enemies)
        {                       
            float distancetoEnemy = Vector3.Distance(transform.position, Enemy.transform.position);
            if(distancetoEnemy < shortestDistance)
            {
                shortestDistance = distancetoEnemy;
                nearestEnemy = Enemy;
            }

            if(nearestEnemy != null && shortestDistance <= range)
            {
                MainEnemy = nearestEnemy;
            }
            else
            {
                MainEnemy = null;
            }
            
        }
    }

    private void Update()
    {
        if(MainEnemy != null)
        {
            Head.rotation = Quaternion.Lerp(Head.rotation, Quaternion.LookRotation(MainEnemy.transform.position - Head.position), Time.deltaTime * 4);
            if(ShootingTime <= 0)
            {
                GameObject _bullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                _bullet.GetComponent<Bullet>().Direction = MainEnemy.transform.position - bulletSpawn.position;
                Destroy(_bullet, 2);
                ShootingTime = 1;
            }
                
        }
        if(ShootingTime > 0)
        {
            ShootingTime -= Time.deltaTime;
        }

        health -= Time.deltaTime;
        Ded();
        
    }

    public void Ded()
    {
        if(health <= 0)
        {
            GameObject DestroyEff = Instantiate(DestroyEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(DestroyEff, 2);
            Destroy(gameObject);
        }
    }

}
