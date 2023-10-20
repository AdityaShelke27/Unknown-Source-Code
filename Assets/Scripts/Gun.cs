using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo;
    public int MaxAmmo;
    public Transform BulletSpawn;
    public GameObject Bullet;
    public GameObject spark;
    public float BulletSpeed = 10f;
    public Animator gunRecoil;
    public float shootingTime = 0.3f;   
    public Transform cam;  
    private Equiping IsEquipped;

    void Start()
    {
        IsEquipped = cam.GetComponent<Equiping>();
        
    }

    void Update()
    {
        if(IsEquipped.GunEquipped)
        {
            Fire();
            
        }               
    }

    public void Fire()
    {
        if (shootingTime > 0)
        {
            shootingTime -= Time.deltaTime;
        }
   
        if (Input.GetButtonDown("Fire1") && shootingTime <= 0)
        {                      
            RaycastHit hit;
            if(Physics.Raycast(cam.position, cam.forward, out hit))
            {
                GameObject spark1 = Instantiate(spark, BulletSpawn.position, BulletSpawn.rotation);
                GameObject bullet1 = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                
                bullet1.GetComponent<Bullet>().Direction = hit.point - BulletSpawn.position;
                shootingTime = 0.3f;
                gunRecoil.Play("Gun_Recoil");
                Destroy(bullet1, 2f);
                Destroy(spark1, 1.5f);
            }
            else
            {
                GameObject spark1 = Instantiate(spark, BulletSpawn.position, BulletSpawn.rotation);
                GameObject bullet1 = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                bullet1.GetComponent<Bullet>().Direction = cam.forward;
                shootingTime = 0.3f;
                gunRecoil.Play("Gun_Recoil");
                Destroy(bullet1, 2f);
                Destroy(spark1, 1.5f);
            }
            
        }           
    }

    

    
}
