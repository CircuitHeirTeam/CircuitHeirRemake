using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemConnector : MonoBehaviour
{
    [SerializeField] private GameObject objectToConnect;
    [SerializeField] private Vector3 rotation;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != objectToConnect)
            return;
        
        GameObject parent = this.transform.parent.gameObject;
        
        // Тег AttachBase можно получить только от объекта с тегом AttachStatic.
        if (parent.gameObject.CompareTag("AttachStatic"))
            other.gameObject.tag = "AttachBase";
        // Если объект не Base, присоединения не происходит.
        if (!parent.gameObject.CompareTag("AttachBase") &&
            !parent.gameObject.CompareTag("AttachStatic"))
            return;
        
        other.transform.SetParent(transform.root.gameObject.transform);
        
        // Найти точку соединения родителя и передвинуть к ней центр other
        // У родителя обязательно должен быть parentJointPoint.
        Transform parentJointPos = this.transform.Find("parentJointPoint");
        other.transform.position = parentJointPos.position;
        other.transform.localEulerAngles = rotation;

        // Переместить на разность позиции центра other и его точки соединения.
        // JointPoint должен быть у всех объектов, которые к чему-либо крепятся.
        Transform jointPoint = other.transform.Find("JointPoint");
        Vector3 jointOffset = other.transform.position - jointPoint.position;
        other.transform.position += jointOffset;
        
        // Соединение
        var joint = parent.AddComponent<FixedJoint>();
        joint.connectedBody = other.GetComponent<Rigidbody>();
        
        // Объект больше нельзя поднимать
        other.GetComponent<XRGrabInteractable>().enabled = false;
        
        // Отключение физики во избежание столкновения с другими конечностями.
        // TODO Недостаток - объект теперь абсолютно прозрачный.
        other.GetComponent<Rigidbody>().isKinematic = true;

        Destroy(this);
    }
}