using UnityEngine;

public partial class zone
{
    public void AddTile_l1lRot45(f4 opts_, byte matId)
    {
        _tiles.offs.Enqueue(new tile_(opts_.F1, PosY + 0.1f, opts_.F2, 0.809f, 0.809f, 1, 1, 1, matTiles.Category.One45Rot, matId));
    }

    public void AddTile_l1l(int x0, int z0, int x1, int z1, byte matId)
    {
        for (int x = x0; x <= x1; ++x)
            for (int z = z0; z <= z1; ++z)
                _tiles.offs.Enqueue(new tile_(x + 0.5f, PosY + 0.05f, z + 0.5f,
                                                1, 1, 1, 1, 1, matTiles.Category.One, matId));
    }

    public void AddTile_l5l(int x0, int z0, int x1, int z1, byte matId)
    {
    }

    public void AddTile_lTl(int x0, int z0, int x1, int z1, byte matId)
    {
        core.zells.Set(x0, z0, x1, z1, bd.Y, cel1l.Type.Tile, cel1l.ColliType.None);

        f2 size = new f2((x1-x0) + 1, (z1-z0) + 1);

        _tiles.offs.Enqueue(new tile_(
            x0 + size.x * 0.5f, PosY, z0 + size.z * 0.5f,
            size.x, size.z, 1, size.x, size.z, matTiles.Category.NxN, matId));
    }

    public class tile_
    {
        public Vector3 Ps, Sz;
        public Vector2 Tiling;
        public matTiles.Category matCategory;
        public byte matType;
        public tile_(float x, float y, float z,
            float sx, float sy, float sz,
            float tilingX, float tilingY, matTiles.Category matCategory_, byte matType_)
        {
            Ps = new Vector3(x, y, z);
            Sz = new Vector3(sx, sy, sz);
            Tiling = new Vector2(tilingX, tilingY);
            matCategory = matCategory_;
            matType = matType_;

            switch (matCategory){
                case matTiles.Category.One45Rot:
                    angle = 45 + Random.Range(0, 3) * 90;
                    break;
                default:
                    angle = 0;// Random.Range(0, 3) * 90;
                    break;
            }

            cdx = -1;
        }
        public float angle;
        public short cdx;
    }
    
    [HideInInspector] qq<tile_> _tiles = new qq<tile_>();

    bool hasTask_OnOn_Tiles()
    {
        byte cnt = 0;
        while (_tiles.offs.Count > 0 && ++cnt < CntTask)
            zjs.tiles.PutOn(_tiles.Off_To_On());
        return cnt != 0;
    }

    bool hasTask_OnOff_Tiles()
    {
        byte cnt = 0;
        while (_tiles.ons.Count > 0 && ++cnt < CntTask)
            zjs.tiles.PutOff(_tiles.On_To_Off());
        return cnt != 0;
    }

    public void clear_Tiles()
    {
        while (_tiles.ons.Count > 0)
            zjs.tiles.PutOff(_tiles.ons.Dequeue());
        _tiles.offs.Clear();
    }

    void refreshTiles_ByJs()
    {
        while (_tiles.ons.Count > 0)
            zjs.tiles.PutOff(_tiles.On_To_Off());
        
        while (_tiles.offs.Count > 0)
            zjs.tiles.PutOn(_tiles.Off_To_On());
    }
}
