using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemConnector : MonoBehaviour
{
    [SerializeField] private GameObject counterObj;
    [SerializeField] private GameObject parentJointPoint;
    [SerializeField] private GameObject jointPoint;
    
    [SerializeField] private Vector3 rotation;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != jointPoint.transform.parent.gameObject)
            return;
        
        GameObject parent = this.transform.parent.gameObject;
        
        // Наследование полномочий присоединения от базы
        if (!parent.CompareTag("AttachBase"))
            return;
        other.tag = "AttachBase";
        
        // Найти точку соединения родителя и передвинуть к ней центр other
        other.transform.position = parentJointPoint.transform.position;
        other.transform.localRotation = Quaternion.Euler(rotation);

        // Переместить на разность позиции центра other и его точки соединения
        other.transform.position += other.transform.position - jointPoint.transform.position;
        
        // Соединение
        var joint = parent.AddComponent<FixedJoint>();
        joint.connectedBody = other.GetComponent<Rigidbody>();
        joint.massScale = 1;
        joint.connectedMassScale = 1;
        
        // Объект больше нельзя поднимать
        other.GetComponent<XRGrabInteractable>().enabled = false;
        
        // Подсчёт количества присоединённых частей
        if (counterObj)
        {
            var count = counterObj.GetComponent<LimbCounter>();
            count.Increment();
        }

        Destroy(this);
    }
}