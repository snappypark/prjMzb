using System.Collections;
using UnityEngine;
using UnityEngineEx;

public class MiniMapCam : MonoBehaviour
{
    public enum State
    {
        None,
        Move,
    }

    [SerializeField] Camera _cam;
    [SerializeField] VecDamp _damp = new VecDamp();
    //  [SerializeField] Texture _miniMapTexture;
    //  [SerializeField] Material _miniMapMaterial;
    State _state = State.None;

    IEnumerator _update_;
    public void Active(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetPosAndOrth(Vector3 pos, float orth, bool moveSmooth)
    {
        _cam.orthographicSize = Mathf.Max(12, orth + orth * 0.064f);
        if (moveSmooth)
        {
            _damp.SetNewTarget(new Vector3(pos.x, pos.y + 7, pos.z));
            _state = State.Move;
            _update_ = update_();
            StartCoroutine(_update_);
        }
        else
        {
            transform.position = new Vector3(pos.x, pos.y + 7, pos.z);
            _damp.Init(transform.position);
        }
    }

    IEnumerator update_()
    {
        while (_damp.UpdateUntilCoincide())
        {
            transform.position = _damp.Cur;
            yield return null;
        }
    }

    /*
void OnGUI()
{
    if (_cam.targetTexture != null)
    {
        _cam.targetTexture.Release();
    }
        //float beginX = 0.02f * Screen.width;
        // float beginZ = 0.04f * Screen.height;
        float beginZ = 0 * Screen.height;
        float rectWidth = 0.22f * Screen.width;
        float beginX = Screen.width - rectWidth;
        Rect map_rect = new Rect(beginX, beginZ,
            rectWidth, rectWidth);

        if (Event.current.type == EventType.Repaint)
            Graphics.DrawTexture(map_rect, _miniMapTexture, _miniMapMaterial);

}
    //*/
}
