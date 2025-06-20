using UnityEngine;

public class LimbCounter : MonoBehaviour
{
    [SerializeField] private int totalParts;
    [SerializeField] private GameObject messageCanvas;

    public int _count = 0;

    public void Increment()
    {
        _count++;
        if (_count == totalParts) messageCanvas.SetActive(true);
    } 
}
