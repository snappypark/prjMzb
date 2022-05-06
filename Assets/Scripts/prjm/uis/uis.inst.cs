using UnityEngine;

public partial class uis
{
    public static bool IsNullInst{get{return _inst == null;}}
    public static float Width { get { return _inst._rt.sizeDelta.x; } }
    public static float Height { get { return _inst._rt.sizeDelta.y; } }

    static uis _inst;
    public static ui_ingam ingam;
    public static ui_outgam outgam;
    public static uj_editmap editmap;
    public static ui_ad ad;
    public static ui_pops pops;
    public static ui_cover cover;
    public static ui_cursor cursor;

    [SerializeField] ui_ingam _ingam;
    [SerializeField] ui_outgam _outgam;
    [SerializeField] uj_editmap _editmap;
    [SerializeField] ui_ad _ad;
    [SerializeField] ui_pops _pops;
    [SerializeField] ui_cover _cover;
    [SerializeField] ui_cursor _cursor;

    void Awake()
    {
        _inst = this;
        
        ingam = _ingam;
        outgam = _outgam;
        editmap = _editmap;
        ad = _ad;
        pops = _pops;
        cover = _cover;
        cursor = _cursor;

        cursor.Set(CursorType.OutGame);
    }


    
    public enum CursorType:byte { OutGame, Ingame, }

    [System.Serializable]
    public class ui_cursor
    {
        [SerializeField] Texture2D _texture;
        
        public void Set(CursorType type)
        {
            int half = _texture.width >> 1;
            switch (type)
            {
                case CursorType.Ingame:
                    Cursor.SetCursor(_texture, new Vector2(half, half+(half>>1)), CursorMode.Auto);
                    break;
                case CursorType.OutGame:
                    Cursor.SetCursor(_texture, new Vector2(half, half), CursorMode.Auto);
                    break;
            }
        }
    }
}
