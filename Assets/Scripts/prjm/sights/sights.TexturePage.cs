using UnityEngine;

public partial class sights /*TexturePage*/
{
    private class texturePage
    {
        public const int DivideNum = 3;
        public const int TextureSide = textureRoller.RollSide * DivideNum;
        public const int TextureSize = TextureSide * TextureSide;

        public Texture2D Texture = null;

        public void Init(Color32[] colDark = null, float darkGamma = 0.23f) // darkGamma --> 0.0 = black, 1.0 = white
        {
            if (null == Texture)
            {
                Texture = new Texture2D(TextureSide, TextureSide, TextureFormat.ARGB32, false)
                {
                    wrapMode = TextureWrapMode.Repeat,
                    //filterMode = FilterMode.Trilinear
                    filterMode = FilterMode.Bilinear
                    //filterMode = FilterMode.Point
                };
            }
            else if (Texture.width != TextureSide || Texture.height != TextureSide)
                Texture.Reinitialize(TextureSide, TextureSide);
            Texture.SetPixels32(colDark);
            Texture.Apply();
        }
    }
}
