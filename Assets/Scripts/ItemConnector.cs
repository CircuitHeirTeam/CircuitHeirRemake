using UnityEngine;

public class ItemConnector : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Cylinder")
        {
            other.transform.SetParent(transform.root.gameObject.transform);

            other.transform.position = this.transform.position;
            other.transform.localEulerAngles = new Vector3(0, 0, 0);

            FixedJoint _joint = transform.root.gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = other.attachedRigidbody;


        }
        Destroy(this);
    }
}
