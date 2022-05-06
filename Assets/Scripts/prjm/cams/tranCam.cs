using UnityEngine;

public class tranCam : MonoBehaviour
{
    public static Vector3 OffsetPlay = new Vector3(0, 13.8f, -4.0f);
    [HideInInspector] Vector3 _offset = new Vector3(0.0f, 12.0f, -12.0f);
    [HideInInspector] Vector3 _offsetTarget = new Vector3(0.0f, 0, 0);
    Transform _target;

    public void SetTarget(Transform target, Vector3 offset, Vector3 offsetTarget)
    {
        _target = target;
        _offset = offset;
        _offsetTarget = offsetTarget;
    }

    public void SetTarget(Transform target, Vector3 offset)
    {
        _target = target;
        _offset = offset;
        _offsetTarget = Vector3.zero;
    }

    private void Awake()
    {
        _target = this.transform;
    }

    public void OnFixedUpdate(Transform target)
    {
        transform.position = target.localPosition + _offset;
        transform.LookAt(target.localPosition + _offsetTarget);
    }
}
