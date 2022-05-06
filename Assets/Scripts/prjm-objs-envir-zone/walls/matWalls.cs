using UnityEngine;

[System.Serializable]
public class matWalls
{
    public enum Type : byte
    {
        SkyLight = 0, SkyDark = 1, BlueLight = 2, BlueDark = 3, NavyLight = 4, NavyDark = 5,
        ViridLight = 6, ViridDark = 7, GreenLight = 8, GreenDark = 9, SmarLight = 10, SmarDark = 11,
        RedLight = 12, RedIdle = 13, RedDark = 14, PurpLight = 15, PurpIdle = 16, PurpDark = 17,
        YelloLight = 18, YelloIdle = 19, YelloDark = 20, BrownLight = 21, BrownIdle = 22, BrownDark = 23,
        WhiteLight = 24, GrayLight = 25, GrayDark = 26, DarkLight = 27, DarkDark = 28, Metal = 29
    }

    public const byte
        SkyLight = 0, SkyDark = 1, BlueLight = 2, BlueDark = 3, NavyLight = 4, NavyDark = 5,
        ViridLight = 6, ViridDark = 7, GreenLight = 8, GreenDark = 9, SmarLight = 10, SmarDark = 11,
        RedLight = 12, RedIdle = 13, RedDark = 14,  PurpLight = 15, PurpIdle = 16, PurpDark = 17,
        YelloLight = 18, YelloIdle = 19, YelloDark = 20, BrownLight = 21, BrownIdle = 22, BrownDark = 23,
        WhiteLight = 24, GrayLight = 25, GrayDark = 26, DarkLight = 27, DarkDark = 28, Metal = 29;
    
    [SerializeField] Material[] _mats;
    public Material this[byte idx] { get { return _mats[idx]; } }


    static Color[] _cols = new Color[]
        {
            new Color(0.282353f, 0.4980392f, 0.6196079f, 1.0f), new Color(0.2666667f, 0.4431373f, 0.5411765f, 1.0f),
            new Color(0.2156863f, 0.3215686f, 0.4941177f, 1.0f), new Color(0.1960784f, 0.2588235f, 0.4235294f, 1.0f),
            new Color(0.1411765f, 0.2196079f, 0.3411765f, 1.0f), new Color(0.04705883f, 0.1176471f, 0.2509804f, 1.0f), 

            // green
            new Color(0.4627451f, 0.5333334f, 0.4235294f, 1.0f), new Color(0.345098f, 0.4f, 0.3098039f, 1.0f),
            new Color(0.227451f, 0.5137255f, 0.4196079f, 1.0f), new Color(0.1294118f, 0.3960785f, 0.3058824f, 1.0f),
            new Color(0.172549f, 0.3686275f, 0.3668628f, 1.0f), new Color(0.1254902f, 0.2666667f, 0.2666667f, 1.0f), 
            
            

            // purp
            new Color(0.4235294f, 0.3058824f, 0.3568628f, 1.0f), new Color(0.4039216f, 0.2627451f, 0.2941177f, 1.0f),
            new Color(0.3686275f, 0.227451f, 0.2588235f, 1.0f), new Color(0.2431373f, 0.2431373f, 0.4f, 1.0f),
            new Color(0.2784314f, 0.2745098f, 0.4196079f, 1.0f), new Color(0.3215686f, 0.3215686f, 0.482353f, 1.0f),


            new Color(0.4901961f, 0.5058824f, 0.254902f, 1.0f), new Color(0.4078432f, 0.4196079f, 0.2431373f, 1.0f),
            new Color(0.3294118f, 0.3372549f, 0.2039216f, 1.0f), new Color(0.5019608f, 0.3647059f, 0.2745098f, 1.0f),
            new Color(0.4235294f, 0.3019608f, 0.2196079f, 1.0f), new Color(0.3333333f, 0.2352941f, 0.1764706f, 1.0f),




            new Color(0.6f, 0.6f, 0.6f, 1.0f), new Color(0.4862745f, 0.4862745f, 0.4862745f, 1.0f),
            new Color(0.4509804f, 0.4509804f, 0.4509804f, 1.0f), new Color(0.3058824f, 0.3058824f, 0.3058824f, 1.0f),
            new Color(0.2392157f, 0.2392157f, 0.2392157f, 1.0f), 


            new Color(0.282353f, 0.2980392f, 0.3137255f, 1.0f),

        };

    const float gapCol = 0.15f;
    public static Color Col(byte idx)
    {
        return _cols[idx] + new Color(gapCol, gapCol, gapCol, 0.0f);
    }
}
