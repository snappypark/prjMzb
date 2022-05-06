using System.Collections;
using UnityEngine;
using nj;

public partial class core : MonoBehaviour
{
    [SerializeField] public FlowMgr flowMgr = new FlowMgr();

    IEnumerator Start()
    {
        yield return null;yield return null;yield return null;yield return null;
        ads.Inst.Init();
        dCore.Load();
        yield return new WaitForSeconds(1.1f);
        flowMgr.Change<Flow_Menu>();
        StartCoroutine(flowMgr.Update_());
    }

    void OnDisable()
    {
        StopCoroutine(flowMgr.Update_());
    }

    delay _delaySound0 = new delay(0.253f);
    void Update()//
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uis.pops.Show_YesOrNo(langs.ExitGame(), ()=>{ Application.Quit();});
        }
        switch (skx)
        {
            case -1:
                return;
            default:
                unitClones.OnUpdate_Msgr();
                return;

        }

    }
    public static int skx = -1;
    public const int lastskx = 8;
    //void LateUpdate() 
    void FixedUpdate()
    {
        switch (++skx)
        {
            case -1:
                //  sights.SetNextPixels_Sight(-sights.heroDist, -sights.heroDist, sights.heroDist, sights.heroDist);
                //  sights.textRoller.ShiftTexture();
                return;
            case 1:
                ctrls.Inst.OnFixedUpdate();
                sights.SetForNextState();
                sights.trSs.SetNextPixel1();
                unitClones.OnUpdate_1by1();
                break;
            case 2:
                ctrls.Inst.OnFixedUpdate();
                sights.trSs.SetNextPixel2();
                unitClones.OnUpdate_1by1();
                break;
            case 3:
                ctrls.Inst.OnFixedUpdate();
                sights.trSs.SetNextPixel3();
                collis.cubeRoller.OnUpdate(ctrls.Unit.pt);
                break;
            case 4:
                ctrls.Inst.OnFixedUpdate();
                sights.trSs.SetNextPixel4();
                unitClones.OnUpdate_1by1();
                break;
            case 5:
                ctrls.Inst.OnFixedUpdate();
                sights.trSs.SetNextPixel5();
                unitClones.OnUpdate_1by1();
                break;
            case 6:
                ctrls.Inst.OnFixedUpdate();
                sights.trSs.SetNextPixel6();
                zjs.rprs.roller.OnUpdate(ctrls.Unit.pt);
                break;
            case 7:
                ctrls.Inst.OnFixedUpdate();
                sights.trSs.SetNextPixel7();
                unitClones.OnUpdate_1by1();
                break;
            case lastskx:
                ctrls.Inst.OnFixedUpdate();
                sights.trSs.SetNextPixel8();
                break;
            default:
                ctrls.Inst.OnFixedUpdate();
                sights.textRoller.ShiftTexture();
                sights.timerUpdate.Reset();
                skx = 0;
                break;
        }
    }



    /*
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //   UIs.Inst.BackBtn.Show();
        }
    }
    */

}
