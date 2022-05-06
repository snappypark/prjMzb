using UnityEngine;

namespace nj
{ 
[System.Serializable]
public struct scByte
{
    [System.NonSerialized]
    static readonly int MAX_TSSDATA = 2;

    [System.NonSerialized]
    TssSdtByte[] _tssData;
    
    bool IsSafe()
    {
        for (int i = 1; i < MAX_TSSDATA; i++)
        {
            int data1 = (byte)_tssData[i];
            int data2 = (byte)_tssData[i - 1];
            int dif = data1 - data2;
            if (dif != 0)
            {
                sc_static.ProblemDetected("[SecureByte] dif=" + dif + ", data1=" + data1 + ", data2=" + data2);
                return false;
            }
        }

        return true;
    }

    public byte GetValue()
    {
        if (_tssData != null)
        {
            if (IsSafe())
                return (byte)_tssData[0];
        }
        return 0;
    }
    public void SetValue(byte v)
    {
        if (_tssData == null)
            _tssData = new TssSdtByte[MAX_TSSDATA];

        for (int i = 0; i < MAX_TSSDATA; i++)
            _tssData[i] = v;
    }

    public int CompareTo(scByte value)
    {
        int v0 = GetValue();
        int v1 = (byte)value;

        return (v0 - v1);
    }

    public static implicit operator byte(scByte v)
    {
        return v.GetValue();
    }

    public static implicit operator scByte(byte v)
    {
        scByte obj = new scByte();
        obj.SetValue(v);
        return obj;
    }

    public static scByte operator ++(scByte v)
    {
        byte value = v.GetValue();
        value++;
        v.SetValue(value);
        return v;
    }

    public static scByte operator --(scByte v)
    {
        byte value = v.GetValue();
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