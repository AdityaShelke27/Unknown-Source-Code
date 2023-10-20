using UnityEngine;

public class SpawningRobot : MonoBehaviour
{
    public GameObject robot;
    private bool CanSpawn = false;
    public Vector3 Offset = new Vector3(0, 0.2f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {            
            CanSpawn = true;                      
        }
    }

    private void FixedUpdate()
    {       
        if (CanSpawn)
        {          
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
                {
                    GameObject _robot = Instantiate(robot, hit.point + Offset, Quaternion.identity);
                    CanSpawn = false;
                    
                }
                
            }
            
        }
    }
   
}
