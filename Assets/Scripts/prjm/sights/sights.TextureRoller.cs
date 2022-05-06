using System.Runtime.CompilerServices;
using UnityEngine;

public partial class sights /*TexturePages*/
{
    public class textureRoller
    {
        public static Color DarkColor;

        public const int LshForRollSide = 4; // 1/16 = 1>>4
        public const int RollSide = 16;
        public const int RollSize = RollSide * RollSide;
        public const float OverRollSide = 0.0625f;
        public const float HalfRollSide = 8.0f;

        const int numPage = 3;
        texturePage[] _Pages = new texturePage[numPage];

        Color32[] _colDark = null;
        public Color32[] GetColDark() { return _colDark; }

        public Texture2D Pre { get { return _Pages[0].Texture; } }
        public Texture2D Cur { get { return _Pages[1].Texture; } }

        public void Init(float darkGamma) // darkGamma --> 0.0 = black, 1.0 = white
        {
            if (null == _colDark)
                _colDark = new Color32[texturePage.TextureSize];
            //_colDark[0] = Color32.Lerp(new Color32(0, 20, 0, 255), new Color32(255, 255, 255, 255), darkGamma);
            DarkColor = Color.Lerp(new Color(0.01f,0.03f,0.03f), new Color(1,1,1), darkGamma);
            _colDark[0] = new Color32((byte)(DarkColor.r*255), (byte)(DarkColor.g * 255), (byte)(DarkColor.b * 255), 0);
            for (int i = 1; i < texturePage.TextureSize; ++i)
                _colDark[i] = _colDark[0];

            for (int i = 0; i < numPage; ++i)
            {
                if (null == _Pages[i])
                    _Pages[i] = new texturePage();
                _Pages[i].Init(_colDark, darkGamma);
            }

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ShiftTexture()
        {
            _Pages[0].Texture.SetPixels32(_Pages[1].Texture.GetPixels32()); // 0 pre texture
            _Pages[1].Texture.SetPixels32(_Pages[2].Texture.GetPixels32()); // 1 pre texture
            _Pages[2].Texture.SetPixels32(_colDark);                    // 2 pre texture
            _Pages[0].Texture.Apply();
            _Pages[1].Texture.Apply();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ShiftTextureDark()
        {
            _Pages[0].Texture.SetPixels32(_Pages[1].Texture.GetPixels32()); // 0 pre texture
            _Pages[1].Texture.SetPixels32(_colDark); // 1 pre texture
            _Pages[2].Texture.SetPixels32(_colDark);                    // 2 pre texture
            _Pages[0].Texture.Apply();
            _Pages[1].Texture.Apply();
        }

        // World Idx => Texture Idx
        float overSide = 0.020833333333f;//
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetNextPixel(int idx_x, int idx_z, Color color)
        {
            if (idx_x < 0 || idx_z < 0)
                return;
            int pageXIdx = idx_x % texturePage.TextureSide;
            int pageZIdx = idx_z % texturePage.TextureSide;
            _Pages[2].Texture.SetPixel(pageXIdx, pageZIdx, color);
            /*
            int pageXIdx = (idx_x >> LshForRollSide) % texturePage.DivideNum;
            int pageZIdx = (idx_z >> LshForRollSide) % texturePage.DivideNum;
            _Pages[2].Texture.SetPixel(pageXIdx * RollSide + (idx_x % RollSide),
                                        pageZIdx * RollSide + (idx_z % RollSide), color);
                                        */
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBright(int idx_x, int idx_z)
        {
            int pageXIdx = idx_x % texturePage.TextureSide;
            int pageZIdx = idx_z % texturePage.TextureSide;
            return _Pages[1].Texture.GetPixel(pageXIdx, pageZIdx).r > alpha;
        }
    }
}
