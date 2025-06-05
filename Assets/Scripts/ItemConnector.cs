using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemConnector : MonoBehaviour
{
    [SerializeField] private GameObject objectToConnect;

    [SerializeField] private Vector3 offset;
    
    [SerializeField] private Vector3 rotation;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToConnect)
        {
            GameObject parent = this.transform.parent.gameObject;
            
            //калибровка расположения объектов относительно друг друга
            other.transform.SetParent(transform.root.gameObject.transform);
            other.transform.position = this.transform.position - offset;
            other.transform.localEulerAngles = rotation;
            
            // Соединение
            var joint = parent.AddComponent<FixedJoint>();
            joint.connectedBody = other.GetComponent<Rigidbody>();
            
            // Второй предмет больше не тягать
            other.GetComponent<XRGrabInteractable>().enabled = false;
        }
        Destroy(this);
    }
}