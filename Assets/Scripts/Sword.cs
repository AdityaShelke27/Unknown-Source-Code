using UnityEngine;

public class Sword : MonoBehaviour
{
    public Equiping GoSword;
    public Animator SwordAnim;
    public bool IsAttacking = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(IsEquipped);
        if (GoSword.SwordEquipped)
        {
            Attack();
            

        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Debug.Log("Attack!!!");
        }
    }

    public void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(IsAttacking)
            {
                SwordAnim.Play("SwordAttack2");
                IsAttacking = false;
                return;
            }
           SwordAnim.Play("SwordAttack1");
            IsAttacking = true;
        }
    }
}
