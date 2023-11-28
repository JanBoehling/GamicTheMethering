using UnityEngine;
using UnityEngine.Events;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private UnityEvent onTargetVisible;
    [SerializeField] private UnityEvent onTargetInvisible;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0) return false;
        }

        return true;
    }

    private void Update()
    {
        if (IsVisible(cam, target)) onTargetVisible.Invoke();
        else onTargetInvisible.Invoke();
    }
}
