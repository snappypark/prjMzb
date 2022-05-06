using System.Runtime.CompilerServices;
using UnityEngine;

public partial class zone
{
    public void AddPrp(int x0, int z0, byte objType_, cel1l.Type cellType_, f4 opts_)
    {
        AddPrp(x0, z0, x0, z0, objType_, cellType_, opts_);
    }

    public void AddPrp(i4 bd, byte objType_, cel1l.Type cellType_, f4 opts_)
    {
        AddPrp(bd.X0, bd.Z0, bd.X1, bd.Z1, objType_, cellType_, opts_);
    }

    public void AddPrp(int x0, int z0, int x1, int z1, byte oType_, cel1l.Type cellType_, f4 opts_)
    {
        i5 bd_ = new i5(x0, z0, x1, z1, 0);
        f2 size = new f2((x1 - x0 + 1), (z1 - z0 + 1));
        f2 center = new f2(x0 + size.x * 0.5f, z0 + size.z * 0.5f);
        Vector3 pos = new Vector3(center.x, PosY, center.z);
        switch (oType_)
        {
            case zprps.eDoor:
                size = size.x > size.z ? new f2(size.x, size.z * 0.5f) : new f2(size.x * 0.5f, size.z);
                core.zells.Set(x0, z0, x1, z1, bd_.Y, cellType_, cel1l.ColliType.Cube);
                _prps[oType_].offs.Enqueue(new prp_(bd_, pos, size.x, 1.8f, size.z, oType_, cellType_, opts_, this));
                break;

            case zprps.eSpin:
            case zprps.eArrow:
                _prps[oType_].offs.Enqueue(new prp_(bd_, pos, size.x, 1, size.z, oType_, cellType_, opts_, this));
                break;
            default:
                core.zells.Set(x0, z0, x1, z1, bd_.Y, cellType_, cel1l.ColliType.None);
                _prps[oType_].offs.Enqueue(new prp_(bd_, pos, size.x, 1, size.z, oType_, cellType_, opts_, this));
                break;
        }
        
    }

    public class prp_
    {
        public i5 bd;
        public Vector3 Ps, Sz;
        public byte type;
        public cel1l.Type cellType;
        public f4 opts;
        public prp_(i5 bd_, Vector3 pos_,
            float sx, float sy, float sz, byte type_, cel1l.Type cellType_, f4 opts_, zone zn_)
        {
            bd = bd_;
            Ps = pos_; Sz = new Vector3(sx, sy, sz);
            type = type_;
            cellType = cellType_;
            opts = opts_;
            cdx = -1;
            zn = zn_;
            checkForPrePath = true;
        }
        public short cdx;

        public tt timin = new tt(1);
        public zone zn;

        public bool checkForPrePath;
    }

    [HideInInspector] qq<prp_>[] _prps = new qq<prp_>[zprps.eNumType] { new qq<prp_>(), new qq<prp_>(), new qq<prp_>(), new qq<prp_>(), new qq<prp_>(), new qq<prp_>(), new qq<prp_>(), new qq<prp_>(), new qq<prp_>(), new qq<prp_>() };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool hasTask_OnOn_Prps()
    {
        byte cnt = 0;
        for(int i=0; i< zprps.eNumType; ++i)
            while (_prps[i].offs.Count > 0 && ++cnt < CntTask)
                zjs.zprps.PutOn(_prps[i].Off_To_On());
        return cnt != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool hasTask_OnOff_Prps()
    {
        byte cnt = 0;
        for (int i = 0; i < zprps.eNumType; ++i)
            while (_prps[i].ons.Count > 0 && ++cnt < CntTask)
                zjs.zprps.PutOff(_prps[i].On_To_Off());
        return cnt != 0;
    }

    public void clear_Prps()
    {
        for (int i = 0; i < zprps.eNumType; ++i)
        { 
            while (_prps[i].ons.Count > 0)
                zjs.zprps.PutOff(_prps[i].ons.Dequeue());
            _prps[i].offs.Clear();
        }
    }

    void refreshZProps_ByJs()
    {
        for (int i = 0; i < zprps.eNumType; ++i)
            while (_prps[i].ons.Count > 0)
                zjs.zprps.PutOff(_prps[i].On_To_Off());
        
        for (int i = 0; i < zprps.eNumType; ++i)
            while (_prps[i].offs.Count > 0)
                zjs.zprps.PutOn(_prps[i].Off_To_On());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EffectPrp(byte oType, cel1l.Type ctype)
    {
        int num = _prps[oType].ons.Count;
        for (int i = 0; i < num; ++i)
        {
            prp_ info = _prps[oType].ons.Dequeue();
            _prps[oType].ons.Enqueue(info);
            if (info.cellType == ctype)
                zjs.zprps.Effect(info);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemovePrp(byte oType, cel1l.Type ctype)
    {
        int num = _prps[oType].ons.Count;
        for (int i = 0; i < num; ++i) {
            prp_ info = _prps[oType].ons.Dequeue();
            if (info.cellType != ctype)
                _prps[oType].ons.Enqueue(info);
            else
                zjs.zprps.Remove(info);
        }
    }

}
