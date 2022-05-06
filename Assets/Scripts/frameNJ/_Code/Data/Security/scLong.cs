using UnityEngine;

namespace nj
{ 
[System.Serializable]
public struct scLong
{
    [System.NonSerialized]
    static readonly int MAX_TSSDATA = 2;

    [System.NonSerialized]
    TssSdtLong[] m_tssData;

    bool IsSafe()
    {
        for (int i = 1; i < MAX_TSSDATA; i++)
        {
            long data1 = (long)m_tssData[i];
            long data2 = (long)m_tssData[i - 1];
            long dif = data1 - data2;
            if (dif != 0)
            {
                sc_static.ProblemDetected("[SecureLong] dif=" + dif + ", data1=" + data1 + ", data2=" + data2);
                return false;
            }
        }

        return true;
    }

    private long GetValue()
    {
        if (m_tssData != null)
        {
            if (IsSafe())
                return (long)m_tssData[0];
        }
        return 0;
    }
    private void SetValue(long v)
    {
        if (m_tssData == null)
            m_tssData = new TssSdtLong[MAX_TSSDATA];

        for (int i = 0; i < MAX_TSSDATA; i++)
            m_tssData[i] = v;

    }

    public static implicit operator long(scLong v)
    {
        return v.GetValue();
    }

    public static implicit operator scLong(long v)
    {
        scLong obj = new scLong();
        obj.SetValue(v);
        return obj;
    }

    public static scLong operator ++(scLong v)
    {
        long value = v.GetValue();
        value++;
        v.SetValue(value);
        return v;
    }

    public static scLong operator --(scLong v)
    {
        long value = v.GetValue();
        value--;
        v.SetValue(value);
        return v;
    }
    public override string ToString()
    {
        return string.Format("{0}", GetValue());
    }
}
}