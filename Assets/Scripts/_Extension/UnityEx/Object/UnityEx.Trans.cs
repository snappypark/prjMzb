using UnityEngine;

namespace UnityEngineEx
{
    public static class Trans
    {
        public static T GetChild<T>(this Transform t, int idx) where T : Component
        {
            return t.GetChild(idx).GetComponent<T>();
        }
        
        public static Vector3 NorVecFrom(this Transform to, Transform from)
        {
            return (to.localPosition - from.localPosition).normalized;
        }

        public static Vector3 NorXzVecFrom(this Transform to, Transform from)
        {
            return new Vector3(to.localPosition.x - from.localPosition.x, 0, to.localPosition.z - from.localPosition.z ).normalized;
        }

        public static bool IsNearBy(this Transform t, Vector3 pos, float dist)
        {
            return (t.localPosition - pos).sqrMagnitude < dist * dist;
        }

        public static bool IsFarFrom(this Transform t, Vector3 pos, float dist)
        {
            return (t.localPosition - pos).sqrMagnitude > dist * dist;
        }

        public static void SetAngleOnXY(this Transform t, Vector3 nDir)
        {
            t.rotation = Quaternion.AngleAxis( angleByNormalVecXY(nDir), Vector3.forward);
        }

        public static void SetAngleOnXY_Fast(this Transform t, Vector3 nDir)
        {
            t.rotation = Quaternion.AngleAxis( angleByNormalVecXY_Fast(nDir), Vector3.forward);
        }

        public static void SetAngleOnXY2(this Transform t, Vector3 nDir)
        {
            t.rotation = Quaternion.AngleAxis(90+ angleByNormalVecXY_Fast(nDir), Vector3.forward);
        }

        static float angleByNormalVecXY(Vector3 nV)
        {
            float angle180 = Vector3.Angle(nV, Vector3.up);
            if (Vector3.Dot(nV, Vector3.left) > 0)
                return angle180;
            return -angle180;
        }

        static float angleByNormalVecXY_Fast(Vector3 nV)
        {
            float angle180 = acos(Vector3.Dot(nV, Vector3.up)) * Mathf.Rad2Deg;// * 90;
            if (Vector3.Dot(nV, Vector3.left) > 0)
                return angle180;
            return -angle180;
        }

        static float acos(float x)
        {
            return (-0.69813170079773212f * x * x - 0.87266462599716477f) * x + 1.5707963267948966f;
        }


    }

    public static class RectTrans
    {
        #region Center

        public static void SetBound_Center(this RectTransform rt, float x, float y, float width, float height)
        {
            // left(0) -> right, up(0) -> down
            rt.localPosition = new Vector3(x, y, 0);
            rt.sizeDelta = new Vector2(width, height);
        }

        #endregion

        #region Center Right

        public static void SetBound_CenterRight(this RectTransform rt, float x, float y)
        {
            rt.anchoredPosition = new Vector2(x, y);
            rt.anchorMin = new Vector2(1, 0.5f);
            rt.anchorMax = new Vector2(1, 0.5f);
        }
        #endregion

        public static float GetTop(this RectTransform rt)
        {
            return -rt.offsetMax.y;
        }

        public static float GetBotton(this RectTransform rt)
        {
            return rt.offsetMin.y;
        }

        public static void SetBound(this RectTransform rt, float minX, float minY, float maxX, float maxY)
        {
            // left(0) -> right, up(0) -> down
            rt.offsetMin = new Vector2(minX, maxY);
            rt.offsetMax = new Vector2(-maxX, minY);
            //PanelSpacerRectTransform.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom); 
            //PanelSpacerRectTransform.offsetMax = new Vector2(-360, -0); // new Vector2(-right, -top)
        }

        public static void SetBound_Strectch(this RectTransform rt, float left, float top, float right, float bottom)
        {
            // left(0) -> right, up(0) -> down
            rt.offsetMin = new Vector2(left, bottom);
            rt.offsetMax = new Vector2(-right, -top);
        }

        #region HorisontalStretch

        public static void SetHorisontalStretchBound(this RectTransform rt, float posY, float height, float left = 0, float right = 0)
        {
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(1, 1);

            Vector2 temp = new Vector2(left, posY - (height * 0.5f));
            rt.offsetMin = temp;
            rt.offsetMax = new Vector3(-right, temp.y + height);
        }

        public static void SetHorisontalStretchBound_Botton(this RectTransform rt, float posY, float height, float left = 0, float right = 0)
        {
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 0);

            Vector2 temp = new Vector2(left, posY - (height * 0.5f));
            rt.offsetMin = temp;
            rt.offsetMax = new Vector3(-right, temp.y + height);
        }

        public static float GetPosY_OnHorisonStretch(this RectTransform rt)
        {
            return rt.anchoredPosition.y;
        }

        #endregion

    }
}
