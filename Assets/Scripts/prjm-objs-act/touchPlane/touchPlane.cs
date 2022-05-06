using UnityEngine;

public class touchPlane : MonoBehaviour
{
    public static touchPlane Inst;

    [SerializeField] Transform _tran;
    [SerializeField] public Renderer render;

    private void Awake()
    {
        Inst = this;
    }

    public void SetActive(bool active)
    {
        render.enabled = active;
        transform.localScale = new Vector3(cel1ls.MaxX, cel1ls.MaxZ, 1);
        _tran.localPosition = new Vector3(150, 1.099999f, 150);
    }

}
