using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    bool Part1 = false;
    public bool CanShakeCamera;
    Vector3 velocity = Vector3.zero;
    Vector3 CameraOriginalPosition;
    
    void Awake()
    {
        CameraOriginalPosition = transform.localPosition;
    }
    public IEnumerator Shake(Vector3 Mag, float magnitude, float Duration)
    {
        if (!Part1)
        {

            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Mag * magnitude, ref velocity, Time.deltaTime * Duration);
            yield return null;

            Part1 = true;
        }
        else
        {

            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, CameraOriginalPosition, ref velocity, Time.deltaTime * Duration);
            yield return null;
            Part1 = false;
            CanShakeCamera = false;

        }
    }
}
