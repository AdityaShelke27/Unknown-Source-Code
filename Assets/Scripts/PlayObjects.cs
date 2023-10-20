using UnityEngine;

public class PlayObjects : MonoBehaviour
{
    public SpringJoint joint;
    public LineRenderer lr;
    public float WidthNuz;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        PlayWithObject();      
    }
    private void LateUpdate()
    {
        DrawLine();
    }

    public void PlayWithObject()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 3))
            {
                if(hit.rigidbody != null)
                {
                    joint = gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedBody = hit.rigidbody;
                    joint.anchor = new Vector3(0, 0, 1);
                    joint.connectedAnchor = Vector3.zero;
                    float distanceFromPoint = Vector3.Distance(joint.gameObject.transform.position + new Vector3(0, 0, 1), hit.point);
                    joint.maxDistance = distanceFromPoint * 0.07f;
                    joint.minDistance = distanceFromPoint * 0.001f;

                    joint.spring = 6f;
                    joint.damper = 0.8f;
                    joint.connectedMassScale = 1f;
                    lr.positionCount = 2;
                }
            }
        }
        else if(Input.GetKeyUp(KeyCode.Q))
        {
            lr.positionCount = 0;
            Destroy(joint);
        }
    }

    void DrawLine()
    {
        if (!joint)
        {
            return;
        }
        lr.SetPosition(0, transform.forward + transform.position);
        lr.SetPosition(1, joint.connectedBody.transform.position + joint.connectedAnchor);
        float LineDistance = Vector3.Distance(lr.GetPosition(0), lr.GetPosition(1));
        lr.startWidth = (1 / LineDistance) * WidthNuz;
    }
}
