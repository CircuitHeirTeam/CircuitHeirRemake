using UnityEngine;

public class ItemConnector : MonoBehaviour
{
    [SerializeField] private FixedJoint _joint;

    private void OnCollisionEnter(Collision collision)
    {
        _joint.connectedBody = collision.rigidbody;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _joint.connectedBody = hit.rigidbody;
    }
}
