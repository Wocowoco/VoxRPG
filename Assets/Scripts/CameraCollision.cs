using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {


    public float minDistance = 1.0f;
    public float MaxDistance = 4.0f;
    public float Smooth = 10.0f;
    public Vector3 dollyDir;
    public Vector3 DollyDirectionAdjusted;
    public float Distance;
    public float currentMaxDistance;
    public LayerMask layerMask;
	// Use this for initialization
	void Awake () {
        dollyDir = transform.localPosition.normalized;
        Distance = transform.localPosition.magnitude;
        currentMaxDistance = MaxDistance;
	}
	
	// Update is called once per frame
	void Update ()
    {

        //Change Max Zoom based on scroll input
        currentMaxDistance -= Input.GetAxis("Mouse ScrollWheel");
        if (currentMaxDistance > MaxDistance)
        {
            currentMaxDistance = MaxDistance;
        }
        else if (currentMaxDistance < minDistance + 0.3f)
        {
            currentMaxDistance = minDistance + 0.3f;
        }

        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * currentMaxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, 1))
        {
            Distance = Mathf.Clamp((hit.distance * 0.95f), minDistance, currentMaxDistance);

        }

        else
        {
            Distance = currentMaxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * Distance, Time.deltaTime * Smooth);
	}
}
