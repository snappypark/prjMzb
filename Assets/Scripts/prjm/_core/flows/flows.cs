using System.Collections;
using UnityEngine;
using UnityEngineEx;
using nj;

public class Flow_Menu : Flow
{
    protected override eType Type { get { return eType.Menu; } }

    public override IEnumerator OnEnter_()
    {
        audios.Inst.SetNeedToPlayDefaultMusic();
        zone.CntTask = 27; cams.Inst.SetBgColorSky();
        core.sights.SetAlpha(sights.alpha);
        core.sights.trSs.Refresh();
        yield return null;
        uis.ad.Logo.SetActive(false);
        audios.Inst.PlayMusic(audios.eMusicType.outgame);

        yield return core.stages.StartMenu_();
        
        if(BoltNetwork.IsRunning)
            uis.outgam.Active(ui_outgam.eState.none, ui_cover.State.Disconnecting);
        else
            uis.outgam.Active(ui_outgam.eState.menu, ui_cover.State.Menu);
        yield return uis.cover.Img.FadeOut_();
    }

    public override IEnumerator OnExit_()
    {
        yield return uis.cover.Img.FadeIn_();
        cams.Inst.SetClearFlag(CameraClearFlags.Color);
        yield return core.loads.Clear_();
        core.sights.SetAlpha(sights.alpha);
        audios.Inst.StopMusic();
        zone.CntTask = 10;
    }
}

public class Flow_SoloPlay : Flow
{
    protected override eType Type { get { return eType.SoloPlay; } }

    public override IEnumerator OnEnter_()
    {
        yield return core.stages.StartSolo_();
        yield return core.stages.ResetJsp();
        uis.cursor.Set(uis.CursorType.Ingame);
        uis.ingam.Active(ui_ingam.State.solo);
        yield return uis.cover.Img.FadeOut_();
    }

    public override IEnumerator OnExit_()
    {
        yield return uis.cover.Img.FadeIn_();
        yield return uis.ingam.Inactive_();
        yield return core.loads.Clear_();
    }
}

public class Flow_MultiLobby : Flow
{
    protected override eType Type { get { return eType.MultiLobby; } }

    public override IEnumerator OnEnter_()
    {
        yield return core.multis.StartMultiLobby_();
        yield return core.stages.ResetJsp();
        uis.cursor.Set(uis.CursorType.Ingame);
        yield return uis.cover.Img.FadeOut_();
    }

    public override IEnumerator OnExit_()
    {
        yield return uis.cover.Img.FadeIn_();
        yield return uis.outgam.Inactive_();
        if (core.Inst.flowMgr.NextType == Flow.iTypeMultiSession)
            yield return core.loads.ClearWithoutUnit_();
        else
            yield return core.loads.Clear_();
    }
}

public class Flow_MultiSession : Flow
{
    protected override eType Type { get { return eType.MultiPlay; } }

    static bool _isFirst = true;
    public override IEnumerator OnEnter_()
    {
        yield return core.multis.StartMultiSession_(_isFirst);
        yield return core.stages.ResetJsp();
        uis.cursor.Set(uis.CursorType.Ingame);
        uis.cover.SetState(ui_cover.State.None);

     //   if (core.Inst.flowMgr.PreType != Flow.iTypeMultiSession)
            yield return uis.cover.Img.FadeOut_();
    }

    public override IEnumerator OnExit_()
    {
        switch (core.Inst.flowMgr.NextType)
        {
            case Flow.iTypeMultiSession:
                _isFirst = false;
                yield return uis.cover.Img.FadeIn_();
                yield return core.loads.ClearWithoutUnit_();
                break;
            default:
                _isFirst = true;
                yield return uis.cover.Img.FadeIn_();
                yield return uis.ingam.Inactive_();
                yield return core.loads.Clear_();
                break;
        }
    }
}

public class Flow_EditMap : Flow
{
    protected override eType Type { get { return eType.EditMap; } }

    public override IEnumerator OnEnter_()
    {
        cams.Inst.SetOrthMode(true);
        touchPlane.Inst.SetActive(true);
        core.sights.SetState(sights.eState.None);
        yield return core.stages.StartEditMap_();
        yield return uis.editmap.Active_();
        uis.cover.SetState(ui_cover.State.None);
        yield return uis.cover.Img.FadeOut_();
    }

    public override IEnumerator OnExit_()
    {
        yield return core.stages.EndEditMap_();
        yield return uis.cover.Img.FadeIn_();
        yield return uis.editmap.Inactive_();
        touchPlane.Inst.SetActive(false);
        cams.Inst.SetOrthMode(false);
        yield return core.loads.Clear_();
    }
}
