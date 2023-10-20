using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject ThrustEffect;
    public TakeDamage PlayerDmgRef;
    public float damage = 2;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ThrustEffect = transform.GetChild(0).gameObject;
        PlayerDmgRef = GameObject.Find("Player").GetComponent<TakeDamage>();
    }

    // Update is called once per frame
    void Update()
    {               
        transform.forward = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.name == "Player")
        {
            PlayerDmgRef.Health -= damage;
        }
        ThrustEffect.SetActive(false);
        Destroy(gameObject, 5);
        enabled = false;
    }
}
