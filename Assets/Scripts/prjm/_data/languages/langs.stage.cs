using UnityEngine;
public static partial class langs
{
    public static string stage() { switch (_lang) {
            case SystemLanguage.Korean: return "스테이지:";
            case SystemLanguage.Japanese: return "ステージ:";
            default: return "Stage:"; } }
    public static string stagePre() { switch (_lang) {
            case SystemLanguage.Korean: return "이전 스테이지";
            case SystemLanguage.Japanese: return "前-ステージ";
            default: return "Pre-Stage"; } }
            
    public static string stageNext(int stageIdx, int maxStageIdx) {
        int stage = stageIdx + 1;
        if (stageIdx >= maxStageIdx)
            return string.Format("THE END #{0}", stage);
        switch (_lang)
        {
            case SystemLanguage.Korean: return string.Format("다음 스테이지 #{0}", stage);
            case SystemLanguage.Japanese: return string.Format("つぎ ステージ #{0}", stage);
            default: return string.Format("Next Stage #{0}", stage);
        }
    }

    public static string stageSharp(int stageIdx, int maxStageIdx) {
        int stage = stageIdx + 1;
        if (stageIdx >= maxStageIdx)
            return string.Format("THE END #{0}", stage);

        switch (_lang)
        {
            case SystemLanguage.Korean: return string.Format("스테이지 #{0}", stage);
            case SystemLanguage.Japanese: return string.Format("ステージ #{0}", stage);
            default: return string.Format("STAGE #{0}", stage);
        }
    }
    
    public static string stageNeedToClearNext() { switch (_lang) {
            case SystemLanguage.Korean: return "다음 스테이지를 클리어 필요합니다.";
            default: return "You need to clear next stage"; } }

    public static string stageWrongNumber() { switch (_lang) {
            case SystemLanguage.Korean: return "잘못된 스테이지 번호";
            default: return "Wrong Stage Number"; } }

}
