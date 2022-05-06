using UnityEngine;

public struct zmSz
{
    public int szGap, numGap, totalGap;
    public zmSz(int szGap_, int numGap_, int totalGap_) { szGap = szGap_; numGap = numGap_; totalGap = totalGap_; }

    public static zmSz s25 = new zmSz(2, 5, 9),  s35 = new zmSz(3, 5, 14),
                       s45 = new zmSz(4, 5, 19), s46 = new zmSz(4, 6, 23),
                       s47 = new zmSz(4, 7, 27), s48 = new zmSz(4, 8, 31), 
                       s49 = new zmSz(4, 9, 35), s4T = new zmSz(4, 10,39);

    public static zmSz s55 = new zmSz(5, 5, 24), s56 = new zmSz(5, 6, 29),
                       s57 = new zmSz(5, 7, 34), s58 = new zmSz(5, 8, 39);
}

public struct zmSZ
{
    public int szGapR, numGapR, totalGapR;
    public int szGapC, numGapC, totalGapC;
    public zmSZ(zmSz zmSz0, zmSz zmSz1)
    {
        szGapR = zmSz0.szGap; numGapR = zmSz0.numGap;
        totalGapR = zmSz0.totalGap;
        szGapC = zmSz1.szGap; numGapC = zmSz1.numGap;
        totalGapC = zmSz1.totalGap;
    }
    public int szGapL() { return Mathf.Max(szGapR, szGapC); }
    public int NumGapL() { return Mathf.Max(numGapR, numGapC); }
    public int szGapS() { return Mathf.Min(szGapR, szGapC); }
    public int NumGapS() { return Mathf.Min(numGapR, numGapC); }

    public i2 szs()
    {
        return new i2(szGapR, szGapC);
    }

    public i2 nums()
    {
        return new i2(numGapR, numGapC);
    }
}
