using UnityEngine;

public class Grapple : MonoBehaviour
{
    public LineRenderer lr;
    public Transform GrappleSpawn;
    public Transform Cam;
    public Transform player;
    public float maxDistance = 100f;
    public LayerMask grappleable;
    public SpringJoint joint;
    public Vector3 grapplePoint;
    public Gun gun;
    public GameObject ConnectedBody;
    public int GrappleBugFix;
    private Equiping IsEquipped;

    private void Start()
    {
        IsEquipped = Cam.GetComponent<Equiping>();
    }
    // Update is called once per frame
    void Update()
    {
        if(IsEquipped.GunEquipped)
        {
            
            if (Input.GetButtonDown("Fire2"))
            {
                StartGrapple();

            }
            else if (Input.GetButtonUp("Fire2"))
            {
                StopGrapple();

            }
        }
        
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    public void StartGrapple()
    {
        RaycastHit hit;       
        if (Physics.Raycast(Cam.position, Cam.forward, out hit, maxDistance, grappleable))
        {
            joint = player.gameObject.AddComponent<SpringJoint>();
            if (hit.rigidbody != null)
            {
                ConnectedBody = hit.collider.gameObject;                
                joint.connectedBody = hit.rigidbody;
                joint.anchor = transform.position;
                GrappleBugFix = 5;

            }
            else
            {
                //ConnectedBody = hit.collider.gameObject;
                grapplePoint = hit.point;
                joint.autoConfigureConnectedAnchor = false;
                //ConnectedBody.transform.position = hit.point;
                joint.connectedAnchor = grapplePoint;
                GrappleBugFix = 1;
            }
            
            
            

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = (distanceFromPoint * 0.4f) / GrappleBugFix;
            joint.minDistance = (distanceFromPoint * 0.1f) / GrappleBugFix;

            joint.spring = 4.5f * GrappleBugFix;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    public void DrawRope()
    {
        if(ConnectedBody != null)
        {
            grapplePoint = ConnectedBody.transform.position;
        }
        
        if(!joint)
        {
            return;
        }
        lr.SetPosition(0, GrappleSpawn.position);
        lr.SetPosition(1, grapplePoint);
    }

    public void StopGrapple()
    {
        ConnectedBody = null;
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
