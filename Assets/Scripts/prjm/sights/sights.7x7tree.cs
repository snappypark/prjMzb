using System.Runtime.CompilerServices;
using UnityEngine;

public partial class sights
{
    public sight trSs;
    void awake_sightsInfo()
    {
        trSs = new sight(2, new i2(1, 0));

        trSs[0] = new sight(2, new i2(2,0)); // 21
            trSs[0][0] = new sight(2, new i2(3,0),new i2(2,1));
                trSs[0][0][0] = new sight(0, new i2(4,0),new i2(5,0),new i2(6,0),new i2(7,0),new i2(8,0));
                trSs[0][0][1] = new sight(0, new i2(4,1),new i2(5,1),new i2(6,1),new i2(7,1),new i2(8,1));
            trSs[0][1] = new sight(0, new i2(2,1),new i2(3,1),new i2(4,1),new i2(5,1),new i2(5,2),new i2(6,2),new i2(7,2),new i2(8,2));
            
        trSs[1] = new sight(2, new i2(1,1), new i2(2,1)); // 20
            trSs[1][0] = new sight(0, new i2(3,1),new i2(4,2),new i2(5,2),new i2(6,2),new i2(6,3),new i2(7,3));
            trSs[1][1] = new sight(2, new i2(2,2),new i2(3,2));
                trSs[1][1][0] = new sight(0, new i2(4,2),new i2(4,3),new i2(5,3),new i2(5,4),new i2(6,4));
                trSs[1][1][1] = new sight(0, new i2(3,3),new i2(4,3),new i2(4,4),new i2(5,4),new i2(5,5));

        trSs.Refresh();
    }

    public class sight
    {
        int numNexts;
        sight[] nexts;
        public sight this[int idx] { get { return nexts[idx]; } set { nexts[idx] = value; } }
        
        int numGaps;
        i2[] gaps;
        float[] ratios;
        Color[] brights;

        const float lllvlvl = 0.015625f;
        public sight(int numNexts_, params i2[] gaps_)
        {
            numNexts = numNexts_;
            if(numNexts > 0 )
                nexts = new sight[numNexts];

            numGaps = gaps_.Length;
            gaps = gaps_;

            ratios = new float[numGaps];
            brights = new Color[numGaps];
            for (int i = 0; i < numGaps; ++i)
            {
                float sqrmag = new Vector2(gaps[i].x, gaps[i].z).sqrMagnitude;
                float dist = sqrmag * 0.015625f;
                float curve = Mathf.Sqrt((-dist + 2) * dist);
                ratios[i] = 1 - curve * curve * curve * curve * curve* curve;
            }
        }

        public void Refresh()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                float bright = Mathf.Min(Mathf.Lerp(_hiddenLightRatio, 1f, ratios[i]), 1);
                brights[i] = new Color(bright, bright, bright);
            }
            for (int i = 0; i < numNexts; ++i)
                nexts[i].Refresh();
        }
        
        public void SetNextPixel1()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x + gap.x, _ct.y, _ct.z + gap.z);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel1();
        }
        
        public void SetNextPixel2()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x + gap.z, _ct.y, _ct.z + gap.x);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel2();
        }
        
        public void SetNextPixel3()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x - gap.z, _ct.y, _ct.z + gap.x);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel3();
        }
        
        public void SetNextPixel4()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x - gap.x, _ct.y, _ct.z + gap.z);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel4();
        }
        
        public void SetNextPixel5()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x - gap.x, _ct.y, _ct.z - gap.z);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel5();
        }
        
        public void SetNextPixel6()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x - gap.z, _ct.y, _ct.z - gap.x);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel6();
        }
        
        public void SetNextPixel7()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x + gap.z, _ct.y, _ct.z - gap.x);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel7();
        }
        
        public void SetNextPixel8()
        {
            for (int i = 0; i < numGaps; ++i)
            {
                i2 gap = gaps[i];
                Pt pt = new Pt(_ct.x + gap.x, _ct.y, _ct.z - gap.z);

                if (cel1ls.IsOutIdx(pt.x, pt.z)) return;
                switch (core.zells[pt.x, pt.z].type)  {
                    case cel1l.Type.Wall: case cel1l.Type.Bush: case cel1l.Type.Bush1: case cel1l.Type.Bush2: case cel1l.Type.Bush3:
                        return;  }
                core.sights.textRoller.SetNextPixel(pt.x, pt.z, brights[i]);
            }

            for (int i = 0; i < numNexts; ++i)
                nexts[i].SetNextPixel8();
        }
    }


    //2,0 ~ 3,0 ~ 4,0 ~ 5,0 ~ 6,0 ~ 7,0
    //          ~ 4,1 ~ 5,1 ~ 6,1 ~ 7,1
    //    ~ 2,1 ~ 3,1 ~ 4,1 ~ 5,1 ~ 5,2 ~ 6,2 ~ 7,2
    //1,1 ~ 2,1 ~ 3,1 ~ 4,2 ~ 5,2 ~ 6,2 ~ 6,3 ~ 7,3
    //          ~ 2,2 ~ 3,2 ~ 4,2 ~ 4,3 ~ 5,3 ~ 5,4 ~ 6,4
    //                      ~ 3,3 ~ 4,3 ~ 4,4 ~ 5,4 ~ 5,5

}
