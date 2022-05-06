using UnityEngine;

public enum FsmNpcType
{
    None,
    Default,
    Tmp1,

    ZombieLv1, // 林炔
    ZombieLv2, ZombieLv3, ZombieLv4, ZombieLv5,

    MeleeLv1, // 林炔 - 弧碍
    MeleeLv2, MeleeLv3, MeleeLv4,

    PistolLv1, // 户碍
    PistolLv2, PistolLv3, PistolLv4,

    //ZzzLv1, // 俏农
}

public class unitEntityAbsNpc : unitEntity
{
    protected const int aniIdle = 0, aniWalk = 1, aniRun = 2, aniMelee = 3, aniDie = 4;
    protected enum state  {None = -1,
        Idle = 0, Melee, Die,
        RunSmoothly, RunSmoothly2, RunSmoothly3, RunRoughly, RunRoughly2,
        LookAt,
    }

    protected state _state = state.None;
    protected pather _pather = new pather();

    protected int _Hp0RandomIdx;

}
