using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngineEx
{
    public static class CompEx
    {
        public static T AddComp_NoDupliated<T>(this GameObject goObj) where T : Component
        {
            T comp = goObj.GetComponent<T>();
            if (comp == null)
                comp = goObj.AddComponent<T>();
            return comp;
        }

        public static void Destory<T>(this GameObject goObj) where T : Component
        {
            T comp = goObj.GetComponent<T>();
            if(comp != null)
                GameObject.DestroyImmediate(comp);
        }

        #region Renderer
        public static void DisableRendererOption(this Renderer renderer)
        {
            renderer.lightProbeUsage = LightProbeUsage.Off;
            renderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            renderer.allowOcclusionWhenDynamic = false;
        }
        public static void EnableRendererOption(this Renderer renderer)
        {
            renderer.lightProbeUsage = LightProbeUsage.BlendProbes;
            renderer.reflectionProbeUsage = ReflectionProbeUsage.BlendProbes;
            renderer.shadowCastingMode = ShadowCastingMode.On;
            renderer.receiveShadows = true;
            renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            renderer.allowOcclusionWhenDynamic = true;
        }
        #endregion
    }
}

