using UnityEngine;
using System.Collections;

public class CustomDragRigidBody : MonoBehaviour
{
    public float objectDistance = 1.5f;

    const float k_Spring = 10000.0f;
    const float k_Damper = 5.0f;
    const float k_Drag = 10.0f;
    const float k_AngularDrag = 5.0f;
    const float k_Distance = 0.2f;
    const bool k_AttachToCenterOfMass = false;

    private FixedJoint m_SpringJoint;
    private bool isDragging = false;


    private void Update()
    {
        // Make sure the user pressed the mouse down
        if (!Input.GetButton("Fire1") || isDragging)
        {
            return;
        }

        var mainCamera = FindCamera();

        // We need to actually hit an object
        RaycastHit hit = new RaycastHit();
        if (
            !Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                             mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, objectDistance,
                             Physics.DefaultRaycastLayers))
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            IInteract interactScript = hit.collider.gameObject.GetComponent<IInteract>();
            if (interactScript != null)
            {
                interactScript.interact();
                return;
            }
        }
        // We need to hit a rigidbody that is not kinematic
        if (!hit.rigidbody || hit.rigidbody.isKinematic)
        {
            return;
        }
        

        if (!m_SpringJoint)
        {
            var go = new GameObject("Rigidbody dragger");
            Rigidbody body = go.AddComponent<Rigidbody>();
            m_SpringJoint = go.AddComponent<FixedJoint>();
            body.isKinematic = true;
        }

        m_SpringJoint.transform.position = hit.point;
        m_SpringJoint.anchor = Vector3.zero;

        //m_SpringJoint.spring = k_Spring;
        //m_SpringJoint.damper = k_Damper;
        //m_SpringJoint.maxDistance = k_Distance;
        m_SpringJoint.connectedBody = hit.rigidbody;
        Rigidbody rBody = m_SpringJoint.connectedBody;
        rBody.constraints = RigidbodyConstraints.FreezeRotation;

        StartCoroutine("DragObject", hit.distance);
    }


    private IEnumerator DragObject(float distance)
    {
        isDragging = true;
        //Debug.Log(m_SpringJoint.connectedBody.drag);
        var oldDrag = m_SpringJoint.connectedBody.drag;
        //Debug.Log(m_SpringJoint.connectedBody.drag);
        var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
        m_SpringJoint.connectedBody.drag = k_Drag;
        m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
        var mainCamera = FindCamera();
        while (Input.GetButton("Fire1"))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            m_SpringJoint.transform.position = ray.GetPoint(distance);
            yield return null;
        }
        if (m_SpringJoint.connectedBody)
        {
            m_SpringJoint.connectedBody.constraints = RigidbodyConstraints.None;
            m_SpringJoint.connectedBody.drag = oldDrag;
            m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
            m_SpringJoint.connectedBody = null;
        }
        isDragging = false;
        GameObject.Destroy(m_SpringJoint.gameObject);
    }


    private Camera FindCamera()
    {
        if (GetComponent<Camera>())
        {
            return GetComponent<Camera>();
        }

        return Camera.main;
    }
}

