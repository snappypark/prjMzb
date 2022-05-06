using System.Collections.Generic;
using UnityEngine;

public partial class zone
{
    public void AddBgs(byte oType_, Vector3 ps_, f4 opts_)
    {
        _bgs.offs.Enqueue(new zbg_(oType_, ps_, opts_));
    }

    public class zbg_
    {
        public byte oType;
        public Vector3 ps;
        public f4 opts;
        public zbg_(byte oType_, Vector3 ps_, f4 opts_)
        {
            oType = oType_;
            ps = ps_;
            opts = opts_;
            cdx = -1;
        }
        public short cdx;
    }

    [HideInInspector] qq<zbg_> _bgs = new qq<zbg_>();

    bool hasTask_OnOn_Bgs()
    {
        byte count = 0;
        while (_bgs.offs.Count > 0 && ++count < CntTask)
            zjs.zbgs.PutOn(_bgs.Off_To_On());
        return _bgs.offs.Count > 0;
    }

    bool hasTask_OnOff_Bgs()
    {
        byte count = 0;
        while (_bgs.ons.Count > 0 && ++count < CntTask)
            zjs.zbgs.PutOff(_bgs.On_To_Off());
        return _bgs.ons.Count > 0;
    }

    public void clear_Bgs()
    {
        while (_bgs.ons.Count > 0)
            zjs.zbgs.PutOff(_bgs.ons.Dequeue());
        _bgs.offs.Clear();
    }

    void refreshBgs_ByJs()
    {
        clear_Bgs();

        List<js.zbg_> zbgs = js.Inst.zones[idx].zbgs;
        for (int i = 0; i < zbgs.Count; ++i)
        {
            js.zbg_ infp = zbgs[i];
            AddBgs((byte)infp.oType, infp.ps, infp.opts);
        }

        while (_bgs.offs.Count > 0)
            zjs.zbgs.PutOn(_bgs.Off_To_On());
    }

}

