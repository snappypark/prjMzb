using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine;

public partial class ctrls
{
    public static bool hasMove, hasAct;
    public static Vector3 iputNDir = Vector3.zero;
    public static Vector3 iputNLook = Vector3.forward;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetInputValue(Vector3 originLookAt, Vector3 nDir_)
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        getInput_OnMobile(originLookAt, nDir_);
#else
        getInput_OnPC(originLookAt, nDir_);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void getInput_OnPC(Vector3 originLookAt, Vector3 nDir_)
    {
        bool front = Input.GetKey(KeyCode.W);
        bool back = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);
        if (front == back)
            front = back = false;
        if (left == right)
            left = right = false;

        hasMove = getNorDirOfMove(front, back, left, right, out iputNDir);

        Vector3 posOnXZ;
        if (cams.PickingPlan(out posOnXZ))
        {
            Vector3 gap = new Vector3(posOnXZ.x - originLookAt.x, 0, posOnXZ.z - originLookAt.z);
            iputNLook = (gap.sqrMagnitude > 0.1f) ? gap.normalized : nDir_;
        }
        else
            iputNLook = nDir_;

        if (EventSystem.current.IsPointerOverGameObject()) /*---*/ return;
        hasAct = Input.GetMouseButton(0);

    }

    public static bool isActBtn = false;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void getInput_OnMobile(Vector3 originLookAt, Vector3 nDir_)
    {
        hasAct = uis.ingam.play.actor.IsMoved();

        Vector3 dirXZ = uis.ingam.play.mover.DirOnXZ();
        bool isMoved = dirXZ.sqrMagnitude > 0.35f;
        iputNDir = isMoved ? dirXZ.normalized : Vector3.zero;

        if (hasAct)
            iputNLook = uis.ingam.play.actor.DirOnXZ().normalized;
        else if (isMoved)
            iputNLook = iputNDir;
        else
            iputNLook = nDir_;

        if (isActBtn) {
            hasAct = true;
            isActBtn = false;
        }

        hasMove = isMoved;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void setInputNone()
    {
        hasAct = hasMove = false;
        iputNDir = Vector3.zero; iputNLook = Vector3.forward;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool getNorDirOfMove(bool front, bool back, bool left, bool right, out Vector3 result)
    {
        int bits = 0;
        if (front) bits |= 8;
        else if (back) bits |= 4;
        if (left) bits |= 2;
        else if (right) bits |= 1;
        switch (bits)
        {
            case 1: result = Vector3.right; return true;
            case 2: result = Vector3.left; return true;
            case 4: result = Vector3.back; return true;
            case 8: result = Vector3.forward; return true;
            //0.387106781186f; //0.7071067811865475f
            case 9: result = new Vector3(0.707106781186f, 0, 0.707106781186f); return true;
            case 10: result = new Vector3(-0.707106781186f, 0, 0.707106781186f); return true;
            case 5: result = new Vector3(0.707106781186f, 0, -0.707106781186f); return true;
            case 6: result = new Vector3(-0.707106781186f, 0, -0.707106781186f); return true;
            default:
                result = Vector3.zero;
                return false;
        }
    }
}
