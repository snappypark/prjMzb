using UnityEngine;

namespace nj
{
    class FoWIDs
    {
        public int mainTex;
        public int inverseView;

        public int MainY;
        public int LrepFogTex;
        public int iState;
        public int ingoreTex, preTex, curTex, rollSide, overRollSide, halfRollSide;

        internal FoWIDs()
        {
            mainTex = Shader.PropertyToID("_MainTex");
            inverseView = Shader.PropertyToID("_InverseView");

            MainY = Shader.PropertyToID("_MainY");
            LrepFogTex = Shader.PropertyToID("_fLrepFogTex");

            iState = Shader.PropertyToID("_iState");
            ingoreTex = Shader.PropertyToID("_IgnoreTex");
            preTex = Shader.PropertyToID("_PreFogTex");
            curTex = Shader.PropertyToID("_CurFogTex");

            rollSide = Shader.PropertyToID("_RollSide");
            overRollSide = Shader.PropertyToID("_OverRollSide");
            halfRollSide = Shader.PropertyToID("_HalfRollSide");
        }
    }

    public abstract class SightsPostProcMgr
    {
        public Camera camera { get; set; }
        static FoWIDs _ids = new FoWIDs();
        static Camera _clearFogCamera = null;

        protected abstract void SetTexture(int id, Texture value);
        protected abstract void SetVector(int id, Vector4 value);
        protected abstract void SetColor(int id, Color value);
        protected abstract void SetFloat(int id, float value);
        protected abstract void SetInt(int id, int value);
        protected abstract void SetMatrix(int id, Matrix4x4 value);
        protected abstract void SetKeyword(string keyword, bool enabled);
        protected abstract void GetTargetSize(out int width, out int height, out int depth);
        protected abstract void BlitToScreen();

        public void Render()
        {
            if ((camera.depthTextureMode & DepthTextureMode.Depth) == 0)
                camera.depthTextureMode |= DepthTextureMode.Depth;

            //if (ctrls.Unit.tran == null)
            //    return;

            //SetInt(_ids.iState, (int)core.sights.State);
            SetFloat(_ids.MainY, 0);
            SetFloat(_ids.LrepFogTex, core.sights.timerUpdate.Ratio01());

            SetFloat(_ids.rollSide, sights.textureRoller.RollSide);
            SetFloat(_ids.overRollSide, sights.textureRoller.OverRollSide);
            SetFloat(_ids.halfRollSide, sights.textureRoller.HalfRollSide);

            //SetTexture(_ids.ingoreTex, core.sights.igrTexs.Texture);
            SetTexture(_ids.curTex, core.sights.textRoller.Cur);
            SetTexture(_ids.preTex, core.sights.textRoller.Pre);

            SetMatrix(_ids.inverseView, camera.cameraToWorldMatrix);

            // orthographic is treated very differently in the shader, so we have to make sure it executes the right code
            SetKeyword("CAMERA_PERSPECTIVE", !camera.orthographic);
            SetKeyword("CAMERA_ORTHOGRAPHIC", camera.orthographic);

            BlitToScreen();
        }
        
    }
}
