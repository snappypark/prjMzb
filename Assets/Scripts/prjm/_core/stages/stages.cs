using System;
using System.Text;
using System.Collections;
using UnityEngine;

public partial class stages : nj.MonoSingleton<stages>
{
    [SerializeField] txts_[] _txts;
    
    public float startTime;
    public float durationTime;

    public string GetJson(int idx)
    {
        int i1 = (int)(idx / 100);
        return _txts[i1].GetJson(idx % 100);
    }
    public string GetName(int idx)
    {
        int i1 = (int)(idx / 100);
        return _txts[i1][idx % 100].name;
    }

    public void SetTimes(float duration_)
    {
        startTime = Time.time;
        durationTime = duration_;
    }
    
    public IEnumerator ResetJsp()
    {
        for (int i = 0; i < core.zones.Num; ++i)
        {
            yield return null;
            core.zones[i].ResetJspState(zone.eJspState.Step0_CheckChange);
        }
    }
}

[System.Serializable]
public class txts_
{
    [SerializeField] TextAsset[] textAssets;
    public int Num { get { return textAssets.Length; } }

    public TextAsset this[int idx] { get { return textAssets[idx]; } }

    public string GetJson(int idx)
    {
        string result = Encoding.UTF8.GetString(textAssets[idx].bytes);
        byte[] decodedBytes = Convert.FromBase64String(result);
        result = Encoding.UTF8.GetString(decodedBytes);
        return result;
    }
}
