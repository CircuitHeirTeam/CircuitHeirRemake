using UnityEngine;

public class ItemConnector : MonoBehaviour
{
    [SerializeField] private FixedJoint _joint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Cylinder")
        {
            _joint.connectedBody = other.attachedRigidbody;
        }
    }
}
