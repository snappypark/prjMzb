//#define _test_90j_

using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngineEx;

public static class dCust
{
    public const int NumType = 4;
    public static byte Type { get { return _tdx; } }

    public static byte SkinMat { get { return _tdx.GetValue() < 2 ? matUnits.CitizenA : matUnits.CitizenB; } }
    public static byte Model { get { return _tdx.GetValue() < 2 ? _tdx.GetValue() : (byte)((int)_tdx.GetValue() - 2); } }

    public static byte Head { get{ return _types[_tdx].ihead; } }
    public static byte Body { get{ return _types[_tdx].ibody; } }
    public static byte Hand { get{ return _types[_tdx].ihand; }}
    public static byte HeadMax { get{ return _types[_tdx].iheadMax; }}
    public static byte BodyMax { get{ return _types[_tdx].ibodyMax; }}
    public static byte HandMax { get{ return _types[_tdx].ihandMax; }}

    static nj.scByte _tdx = 0; // model.eType

    static type_[] _types = new type_[4] { new type_(), new type_(), new type_(), new type_() };

    public static void Load()
    {
        _tdx.SetValue((byte)ObscuredPrefs.GetInt("epyeydob", Random.Range(0, 2)));

        _types[0].Load(0, 65, 48, 16);
        _types[1].Load(1, 65, 39, 16);
        _types[2].Load(2, 65, 48, 16);
        _types[3].Load(3, 65, 39, 16);
    }

    public static void Save()
    {
        ObscuredPrefs.SetInt("epyeydob", _tdx);
        for (int i = 0; i < 4; ++i)
            _types[i].Save(i);
        ObscuredPrefs.Save();
    }

    public static bool HasAll()
    {
        return _types[0].hasAll() && _types[1].hasAll() && 
                _types[2].hasAll() && _types[3].hasAll();
    }

    public static void Add()
    {
        // random
        if (!_types[0].hasAll())
        { _types[0].Add();return; }

        if (!_types[1].hasAll())
        { _types[1].Add(); return; }

        if (!_types[2].hasAll())
        { _types[2].Add(); return; }

        if (!_types[3].hasAll())
        { _types[3].Add(); return; }
    }

    class type_
    {
        public nj.scByte ihead, ibody, ihand;
        public nj.scByte iheadMax, ibodyMax, ihandMax;
        public int iheadLimit, ibodyLimit, ihandLimit;

        public void Load(int idx, int iheadLimit_, int ibodyLimit_, int ihandLimit_)
        {
            ihead.SetValue((byte)ObscuredPrefs.GetInt(string.Format("{0}daeh", idx), 0));
            ibody.SetValue((byte)ObscuredPrefs.GetInt(string.Format("{0}ydob", idx), 0));
            ihand.SetValue((byte)ObscuredPrefs.GetInt(string.Format("{0}dnah", idx), 0));
            iheadMax.SetValue((byte)ObscuredPrefs.GetInt(string.Format("{0}daehxam", idx), 1));
            ibodyMax.SetValue((byte)ObscuredPrefs.GetInt(string.Format("{0}ydxamob", idx), 1));
            ihandMax.SetValue((byte)ObscuredPrefs.GetInt(string.Format("{0}dxamnah", idx), 1));

#if _test_90j_
           // iheadMax.SetValue((byte)1);
          //  ibodyMax.SetValue((byte)1);
          //  ihandMax.SetValue((byte)1);
#endif

            iheadLimit = iheadLimit_; ibodyLimit = ibodyLimit_; ihandLimit = ihandLimit_;
        }

        public void Save(int idx)
        {
            ObscuredPrefs.SetInt(string.Format("{0}daeh", idx), ihead);
            ObscuredPrefs.SetInt(string.Format("{0}ydob", idx), ibody);
            ObscuredPrefs.SetInt(string.Format("{0}dnah", idx), ihand);
            ObscuredPrefs.SetInt(string.Format("{0}daehxam", idx), iheadMax);
            ObscuredPrefs.SetInt(string.Format("{0}ydxamob", idx), ibodyMax);
            ObscuredPrefs.SetInt(string.Format("{0}dxamnah", idx), ihandMax);
        }

        public bool hasAll()
        {
            return iheadMax == iheadLimit && ibodyMax == ibodyLimit;
        }

        public void Add()
        {
            if (iheadMax < iheadLimit)  {
                ++iheadMax;
                return;  }
            if (ibodyMax < ibodyLimit)  {
                ++ibodyMax;
                return; }
          //  if (ihandMax < ihandLimit)  {
          //      ++ihandMax;
           //     return; }
        }
    }

    
    public static void RollType(int gap) { _tdx = RollClamp(_tdx, 3, gap); }
    public static void RollHead(int gap) { _types[_tdx].ihead = RollClamp(_types[_tdx].ihead, _types[_tdx].iheadMax, gap); }
    public static void RollBody(int gap) { _types[_tdx].ibody = RollClamp(_types[_tdx].ibody, _types[_tdx].ibodyMax, gap); }
    public static void RollHand(int gap) { _types[_tdx].ihand = RollClamp(_types[_tdx].ihand, _types[_tdx].ihandMax, gap); }
    static byte RollClamp(int value_, int max_, int gap)
    {
        int value = value_ + gap;
        if (value < 0)
            return (byte)max_;
        else if (value > max_)
            return 0;
        return (byte)value;
    }
    
    public static int CurMdxes_h()
    {
        return (int)Model * 100000000 +
             (int)SkinMat * 1000000 +
                (int)Head * 10000 +
                (int)Body * 100 +
                (int)Hand;
    }

    public static int GetMobMdxes_h()
    {
        model.eType m = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
        byte mat = matUnits.GetByZombieLv(unit.SubType.MeleeLv1);
        byte melee = (byte)model.GetRandMelee(Melee.BaseBallBat, Melee.Handgun);
        byte head = 0, body = 0;
        switch (m) {
            case model.eType.Zombie_Female:
                head = (byte)ZFModel.GetRandHead();
                body = (byte)ZFModel.GetRandBody();
                break;
            case model.eType.Zombie_Male:
                head = (byte)ZMModel.GetRandHead();
                body = (byte)ZMModel.GetRandBody();
                break; }
        return (int)m * 100000000 +
             (int)mat * 1000000 +
            (int)head * 10000 +
            (int)body * 100 +
            (int)melee;
    }
}
