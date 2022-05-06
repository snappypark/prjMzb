using UnityEngine;

[System.Serializable]
public class spriteProps
{
    public const byte left = 0, right = 1, down = 2, up = 3, star = 4, heart = 5, oil = 6;

    [SerializeField] Sprite[] _sps;
    public Sprite this[byte idx] { get { return _sps[idx]; } }

    public static Color mvCol0 = new Color(0.4117647f, 0.7294118f, 1, 1);
    public static Color mvCol1 = new Color(0.1411765f, 0.4666667f, 0.9137255f, 0.5333334f);
    public static Color mvCol2 = new Color(0.1411765f, 0.4666667f, 0.9137255f, 0.5333334f);
    public static Color hlCol0 = new Color(0.4117647f, 1, 0.7294118f, 1);
    public static Color hlCol1 = new Color(0.1411765f, 0.9137255f, 0.4666667f, 0.5333334f);
    public static Color hlCol2 = new Color(0.1411765f, 0.9137255f, 0.4666667f, 0.5333334f);
    public static Color oiCol0 = new Color(0.3301887f, 0.2583226f, 0.1541919f, 0.9f);
    public static Color oiCol1 = new Color(0.3962264f, 0.3475174f, 0.2971698f, 0.6941177f);
    public static Color oiCol2 = new Color(0.3962264f, 0.3475174f, 0.2971698f, 0.6941177f);
}
