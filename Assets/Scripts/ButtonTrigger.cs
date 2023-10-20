using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    MeshRenderer MeshColor;
    public Animator door;
    int health = 3;
    public GameObject ButtonBreakEffect;
    private void Start()
    {
        MeshColor = GetComponent<MeshRenderer>();
        MeshColor.material.color = Color.red;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            return;
        }
        GameObject _BreakEffect = Instantiate(ButtonBreakEffect, transform.position, Quaternion.Euler(0, 90, 0));
        Destroy(_BreakEffect, 1.5f);
        health--;
        if (health <= 0)
        {
            MeshColor.material.color = Color.green;
            door.Play("Door_Open");
        }       
    }

    private void Update()
    {
        
    }
}
