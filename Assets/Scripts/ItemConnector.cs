using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemConnector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Cylinder")
        {
            //калибровка расположения обьектов относительно друг друга
            other.transform.SetParent(transform.root.gameObject.transform);
            other.transform.position = this.transform.position;
            other.transform.localEulerAngles = new Vector3(0, 0, 0);

            FixedJoint _joint = transform.root.gameObject.AddComponent<FixedJoint>();

            _joint.connectedBody = other.attachedRigidbody; //коннект двух объектов

            other.GetComponent<XRGrabInteractable>().enabled = false;
        }
        Destroy(this);
    }
}
