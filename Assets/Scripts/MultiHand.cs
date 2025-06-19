using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MultiHand : MonoBehaviour
{
    [Tooltip("Attach Transform при захвате левой рукой")]
    [SerializeField] private GameObject attachLeft;
    
    [Tooltip("Attach Transform при захвате правой рукой")]
    [SerializeField] private GameObject attachRight;
    
    public void GetHandSetAttach(SelectEnterEventArgs args)
    {
        InteractorHandedness hand = args.interactorObject.handedness;
        
        var grab = this.GetComponent<XRGrabInteractable>();

        grab.attachTransform = hand switch
        {
            InteractorHandedness.Right => attachRight.transform,
            InteractorHandedness.Left  => attachLeft.transform,
            _ => grab.attachTransform
        };
        
        // TODO
        // Rotation выбранного Attach Transform применяется
        // только со второго поднятия предмета.
    }
}