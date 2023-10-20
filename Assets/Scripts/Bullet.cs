using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 30f;
    public Vector3 Direction;
    public Rigidbody rb;
    public GameObject BulletImpParticle;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
        transform.forward = Direction;
    }

    private void FixedUpdate()
    {
        Movement(Direction);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.name == "Player" || other.gameObject.tag == "Robot")
        {
            return;
        }
        GameObject bullet_Imp = Instantiate(BulletImpParticle, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(bullet_Imp, 0.5f);
    }

    public void Movement(Vector3 Direction)
    {
        rb.velocity = Direction.normalized * BulletSpeed;
    }
}
