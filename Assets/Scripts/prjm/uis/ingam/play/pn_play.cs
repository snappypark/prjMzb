using UnityEngine;
using UnityEngine.UI;
using Beebyte.Obfuscator;

public class pn_play : MonoBehaviour
{
    [SerializeField] public pnn_intf intf;

    [SerializeField] public joystick mover;
    [SerializeField] public joystick actor;

    [SerializeField] public lbCountdownRemain lbCountdownRemain;

    public void OnActive(ui_ingam.State state_)
    {
        bool isInGame = state_ != ui_ingam.State.none;

        gameObject.SetActive(isInGame);

        intf.OnActive(state_);

#if UNITY_ANDROID || UNITY_IOS
        mover.Scroll.gameObject.SetActive(isInGame);
        actor.Scroll.gameObject.SetActive(isInGame);
#endif

        if (!isInGame)
        {
            mover.TransBtn.anchoredPosition = Vector2.zero;
            actor.TransBtn.anchoredPosition = Vector2.zero;
        }
    }

    #region UI Action
    [SkipRename]
    public void OnBtn_JoystickRight()
    {
        ctrls.isActBtn = true;
    }
    #endregion

    [System.Serializable]
    public class joystick
    {
        [SerializeField] public ScrollRect Scroll = null;
        [SerializeField] public RectTransform TransBtn = null;
        [HideInInspector] public float sqrDist = 0.0f;
        [HideInInspector] public Vector2 Dir = Vector2.zero;

        public bool IsMoved()
        {
            float size02 = TransBtn.sizeDelta.x * 0.2f;
            return TransBtn.anchoredPosition.sqrMagnitude > size02 * size02;
        }

        public bool IsMoved(float ratio)
        {
            float sizeRatio = TransBtn.sizeDelta.x * ratio;
            return TransBtn.anchoredPosition.sqrMagnitude > sizeRatio * sizeRatio;
        }

        public Vector3 nDirOnXZ()
        {
            return new Vector3(TransBtn.anchoredPosition.x, 0, TransBtn.anchoredPosition.y).normalized;
        }

        public Vector3 DirOnXZ()
        {
            return new Vector3(TransBtn.anchoredPosition.x, 0, TransBtn.anchoredPosition.y);
        }
    }
}
