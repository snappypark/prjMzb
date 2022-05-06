#if UNITY_POST_PROCESSING_STACK_V2

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
public class LayerMaskPara : ParameterOverride<LayerMask> { }

class SightsPPSv2Mgr : nj.SightsPostProcMgr
{
    PostProcessRenderContext _context;
    PropertySheet _sheet;
    MaterialPropertyBlock _properties { get { return _sheet.properties; } }

    public void Setup(PostProcessRenderContext context)
    {
        _context = context;
        _sheet = _context.propertySheets.Get(Shader.Find("Hidden/SightsPPSv2"));
    }

    protected override void SetInt(int id, int value) { _properties.SetInt(id, value); }
    protected override void SetTexture(int id, Texture value) { _properties.SetTexture(id, value); }
    protected override void SetVector(int id, Vector4 value) { _properties.SetVector(id, value); }
    protected override void SetColor(int id, Color value) { _properties.SetColor(id, value); }
    protected override void SetFloat(int id, float value) { _properties.SetFloat(id, value); }
    protected override void SetMatrix(int id, Matrix4x4 value) { _properties.SetMatrix(id, value); }
    protected override void SetKeyword(string keyword, bool enabled)
    {
        if (enabled)
            _sheet.EnableKeyword(keyword);
        else
            _sheet.DisableKeyword(keyword);
    }

    protected override void GetTargetSize(out int width, out int height, out int depth)
    {
        width = _context.width;
        height = _context.height;
        depth = 16;
    }

    protected override void BlitToScreen()
    {
        _context.command.BlitFullscreenTriangle(_context.source, _context.destination, _sheet, 0);
    }
}

[System.Serializable]
[PostProcess(typeof(SightsPPSv2Renderer), PostProcessEvent.AfterStack, "Sights")]
public sealed class SightsPPSv2 : PostProcessEffectSettings
{
    public BoolParameter fogFarPlane = new BoolParameter { value = true };
    [Range(0.0f, 1.0f)]
    public FloatParameter outsideFogStrength = new FloatParameter { value = 1 };
    public BoolParameter pointFiltering = new BoolParameter { value = false };
    
    [Header("Clear Fog")]
    public BoolParameter clearFog = new BoolParameter { value = false };
    public LayerMaskPara clearFogMask = new LayerMaskPara { value = -1 };

    public override bool IsEnabledAndSupported(PostProcessRenderContext context)
    {
        return enabled.value && Application.isPlaying;
    }
}

public sealed class SightsPPSv2Renderer : PostProcessEffectRenderer<SightsPPSv2>
{
    SightsPPSv2Mgr _postProcess = null;

    public override void Render(PostProcessRenderContext context)
    {
        if (_postProcess == null)
            _postProcess = new SightsPPSv2Mgr();

        _postProcess.Setup(context);
        _postProcess.camera = context.camera;
        _postProcess.Render();
    }
}

#endif
