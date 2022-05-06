using UnityEngine;

public static partial class langs
{
    public static string Ok()  { switch (_lang)  { case SystemLanguage.Korean: return "확인"; case SystemLanguage.Japanese: return "Ok"; default: return "Ok"; } }
    public static string Yes()  { switch (_lang)  { case SystemLanguage.Korean: return "네"; case SystemLanguage.Japanese: return "はい"; default: return "Yes"; } }
    public static string No() {  switch (_lang) { case SystemLanguage.Korean: return "아니오"; case SystemLanguage.Japanese: return "いいえ"; default: return "No"; } }
    public static string Cancel() {  switch (_lang) { case SystemLanguage.Korean: return "취소"; case SystemLanguage.Japanese: return "キャンセル"; default: return "Cancel"; } }

    public static string GameName() { switch (_lang) {
            case SystemLanguage.Korean: return "미로 좀비 탈출";
            case SystemLanguage.Japanese: return "メイズ ゾンビ 割り";
            default: return "Maze Zombie Break"; } }
    
    public static string StartGame() { switch (_lang) {
            case SystemLanguage.Korean: return "게임 시작";
            case SystemLanguage.Japanese: return "ゲームを始める";
            default: return "Start Game"; } }

    public static string SinglePlayer() { switch (_lang) {
            case SystemLanguage.Korean: return "싱글 플레이";
            case SystemLanguage.Japanese: return "シングル  プレーヤー";
            default: return "Single Player"; } }

    public static string MultiPlayer() { switch (_lang) {
            case SystemLanguage.Korean: return "멀티 플레이";
            case SystemLanguage.Japanese: return "マルチ-  プレーヤー";
            default: return "Multi- Player"; } }
    

    public static string Character() { switch (_lang) {
            case SystemLanguage.Korean: return "캐릭터";
            case SystemLanguage.Japanese: return "キャラクター";
            default: return "Character"; } }

    public static string Rate() { switch (_lang) {
            case SystemLanguage.Korean: return "평가";
            case SystemLanguage.Japanese: return "Rate";
            default: return "Rate"; } }
            
    #region option
    public static string Option_credit() { switch (_lang) {
            case SystemLanguage.Korean: return "크레딧";
            case SystemLanguage.Japanese: return "クレジット";
            default: return "credit"; } }
    
    public static string Option_language() { switch (_lang) {
            case SystemLanguage.Korean: return "언어";
            case SystemLanguage.Japanese: return "言語";
            default: return "language"; } }

    public static string ExitGame() { switch (_lang) {
            case SystemLanguage.Korean: return "게임을 종료하시겠습니까?";
            case SystemLanguage.Japanese: return "ゲームを終了しますか？";
            default: return "QUIT GAME?"; }  }
    #endregion

    #region multi
    public static string MazeEscape() { switch (_lang) {
            case SystemLanguage.Korean: return "탈출 모드";
            case SystemLanguage.Japanese: return "Maze Escape";
            default: return "Maze Escape"; } }

    public static string TeamBattle() { switch (_lang) {
            case SystemLanguage.Korean: return "그룹 배틀";
            case SystemLanguage.Japanese: return "Team Battle";
            default: return "Team Battle"; } }

    public static string Maze_Escape() { switch (_lang) {
            case SystemLanguage.Korean: return "탈출\n모드";
            case SystemLanguage.Japanese: return "Maze\nEscape";
            default: return "Maze\nEscape"; } }

    public static string Team_Battle() { switch (_lang) {
            case SystemLanguage.Korean: return "그룹\n배틀";
            case SystemLanguage.Japanese: return "Team\nBattle";
            default: return "Team\nBattle"; } }
            
    public static string WhomRoom(string hostname)  { switch (_lang) {
            case SystemLanguage.Korean: return string.Format("{0}의 방", hostname);
            case SystemLanguage.Japanese: return string.Format("{0} Room", hostname);
            default: return string.Format("{0}'s room", hostname);  } }

    public static string RoomCode(string code) { switch (_lang) {
            case SystemLanguage.Korean: return string.Format("코드: {0}", code);
            case SystemLanguage.Japanese: return string.Format("Code: {0}", code);
            default: return string.Format("Code: {0}", code); } }

    public static string EnterCode() { switch (_lang) {
            case SystemLanguage.Korean: return "코드 입력...";
            case SystemLanguage.Japanese: return "Enter Code...";
            default: return "Enter Code..."; } }

    public static string JoinPrivateRoom() { switch (_lang) {
            case SystemLanguage.Korean: return "비공개 참가";
            case SystemLanguage.Japanese: return "Private Join";
            default: return "Private Join"; } }

    public static string CreateRoom() { switch (_lang) {
            case SystemLanguage.Korean: return "방 생성";
            case SystemLanguage.Japanese: return "Create Room";
            default: return "Create Room"; } }

    public static string MemberMuber(int count) { switch (_lang) {
            case SystemLanguage.Korean: return string.Format("멤버 ({0}/8)", count);
            case SystemLanguage.Japanese: return string.Format("Member ({0}/8)", count);
            default: return string.Format("Member ({0}/8)", count); } }

    public static string Public() { switch (_lang) {
            case SystemLanguage.Korean: return "공개 ";
            case SystemLanguage.Japanese: return "Public";
            default: return "Public"; } }
    
    public static string Dark() { switch (_lang) {
            case SystemLanguage.Korean: return "어두움";
            case SystemLanguage.Japanese: return "Dark ";
            default: return "Dark "; } }
    #endregion

    public static string AddedCustom(int n) { switch (_lang) {
            case SystemLanguage.Korean: return string.Format("커스텀이 추가 되었습니다. x{0}", n);
            case SystemLanguage.Japanese: return string.Format("Custom Added. x{0}", n);
            default: return string.Format("Custom Added. x{0}", n); } }


}
