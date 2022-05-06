using UnityEngine;

public static partial class langs
{
    static SystemLanguage _lang { get {
#if UNITY_EDITOR
            return SystemLanguage.Korean; // English;
#else
            return Application.systemLanguage;
#endif
        } }

    public static string Version() { return string.Format("v.{0}", Application.version); }

    public static string MoveJoystic() {
        switch (_lang) {
            case SystemLanguage.Korean: return "조이스틱을 움직여주세요.";
            case SystemLanguage.Japanese: return "ジョイスティックを使って遊ぶ.";
            default: return "MOVE THE JOYSTICK TO PLAY";
        }
    }

    public static string UninstallLoss()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return "앱을 삭제하시면 데이터가 손실됩니다.";
            case SystemLanguage.Japanese: return "のアプリを削除するとデータは失われます.";
            default: return "uninstall app cause data loss.";
        }
    }
    
    public static string GameOver() {
        switch (_lang){
            case SystemLanguage.Korean: return "게임 오버";
            case SystemLanguage.Japanese: return "ゲーム オーバー";
            default: return "GAME OVER";
        }
    }

    public static string TimeOut()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return "타임 아웃";
            case SystemLanguage.Japanese: return "タイム アウト";
            default: return "Time Out";
        }
    }
    public static string GameClear(){
        switch (_lang){
            case SystemLanguage.Korean: return "게임 클리어";
            case SystemLanguage.Japanese: return "ゲーム クリア";
            default: return "GAME CLEAR";
        }
    }

    public static string CustomType(int idx, int max) {
        switch (_lang) {
            case SystemLanguage.Korean: return string.Format("타입 ({0}/{1})", idx + 1, max);
            case SystemLanguage.Japanese: return string.Format("タイプ ({0}/{1})", idx + 1, max);
            default: return string.Format("type ({0}/{1})", idx + 1, max);
        }
    }

    public static string CustomHead(int idx, int maxIdx) {
        switch (_lang) {
            case SystemLanguage.Korean: return string.Format("머리 ({0}/{1})", idx + 1, maxIdx + 1);
            case SystemLanguage.Japanese: return string.Format("ヘッド ({0}/{1})", idx + 1, maxIdx + 1);
            default: return string.Format("head ({0}/{1})", idx + 1, maxIdx + 1);
        }
    }

    public static string CustomBody(int idx, int maxIdx) {
        switch (_lang) {
            case SystemLanguage.Korean: return string.Format("몸통 ({0}/{1})", idx + 1, maxIdx + 1);
            case SystemLanguage.Japanese: return string.Format("ボディ ({0}/{1})", idx + 1, maxIdx + 1);
            default: return string.Format("body ({0}/{1})", idx + 1, maxIdx + 1);
        }
    }

    public static string CustomHand(int idx, int maxIdx) {
        switch (_lang) {
            case SystemLanguage.Korean: return string.Format("핸드 ({0}/{1})", idx + 1, maxIdx + 1);
            case SystemLanguage.Japanese: return string.Format("ハンド ({0}/{1})", idx + 1, maxIdx + 1);
            default: return string.Format("hand ({0}/{1})", idx + 1, maxIdx + 1);
        }
    }

    public static string fullOfItems()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return "코스튬을 이미 다 모았습니다.";
            case SystemLanguage.Japanese: return "full of custom.";
            default: return "full of custom.";
        }
    }

    public static string PlusAmmo()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return string.Format("+총알");
            case SystemLanguage.Japanese: return string.Format("+弾丸");
            default: return string.Format("+Ammo");
        }
    }

    public static string PlusBomb()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return string.Format("+폭탄");
            case SystemLanguage.Japanese: return string.Format("+爆弾");
            default: return string.Format("+Bomb");
        }
    }


    public static string rateUs()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return "평가하기";
            case SystemLanguage.Japanese: return "RATE US";
            default: return "RATE US";
        }
    }
    public static string tryNow()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return "추천게임";
            case SystemLanguage.Japanese: return "TRY NOW";
            default: return "TRY NOW";
        }
    }

    
    public static string adIsNotReady()
    {
        switch (_lang)
        {
            case SystemLanguage.Korean: return "광고가 준비되어 있지 않습니다.";
            case SystemLanguage.Japanese: return "Ad is not ready.";
            default: return "Ad is not ready.";
        }
    }

    
}
