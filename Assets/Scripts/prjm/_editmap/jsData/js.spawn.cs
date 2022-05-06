using UnityEngine;
using UnityEngineEx;

public partial class js /*.spawn*/
{
    public void AddSpawn(byte ally_, float x_, float y_, float z_,
            float rx_, float ry_, float rz_,
            int numAmmoPistol_, int numAmmoRifle_, int numAmmoBomb_, SeriType seriType)
    {
        spawn_ spawn = new spawn_(ally_, x_, y_, z_, rx_, ry_, rz_,
             numAmmoPistol_, numAmmoRifle_, numAmmoBomb_);
        spawns.Add(spawn);
        if (seriType == SeriType.WithLoad)
        {
            ctrls.Inst.AddSpawn(ally_, x_, y_, z_, rx_, ry_, rz_, numAmmoPistol_, numAmmoRifle_, numAmmoBomb_);
            if (null == spawn.hud)
                spawn.hud = core.huds.editmap.CreateCel1l(new Vector3(x_, y_, z_), Color.white, 0.1f, "spawn(Clone)");
            else
                spawn.hud.ActiveRectOnXZ(new Vector3(x_, y_, z_).WithGapY(0.01f));
        }
    }

    public void RemoveSpawn(Vector3 pos)
    {
        for (int i = 0; i < spawns.Count; ++i)
            if (spawns[i].pos == pos)
            {
                if (null != spawns[i].hud)
                    GameObject.DestroyImmediate(spawns[i].hud.gameObject);
                spawns.RemoveAt(i);
                return;
            }
    }

    public bool CheckHasSpawn(Vector3 pos)
    {
        for (int i = 0; i < spawns.Count; ++i)
            if (spawns[i].pos == pos)
                return true;
        return false;
    }

    public void ResetSpawn(float x, float z)
    {
        spawns.Clear();
        AddSpawn(0, x, 0, z, 0, 0, 1, 10, 10, 0, js.SeriType.Default);
    }

    [System.Serializable]
    public class spawn_
    {
        [SerializeField] public byte ally = 0;
        [SerializeField] public Vector3 pos;
        [SerializeField] public Vector3 dir = new Vector3(0, 0, 1);

        [SerializeField] public int numAmmoPistol = 10;
        [SerializeField] public int numAmmoRifle = 30;
        [SerializeField] public int numBomb = 1;
        
        [HideInInspector] public hudLine hud;

        public spawn_(byte ally_, float x_, float y_, float z_,
            float rx_, float ry_, float rz_,
            int numAmmoPistol_, int numAmmoRifle_, int numAmmoBomb_)
        {
            ally = ally_;
            pos = new Vector3(x_, y_, z_);
            dir = new Vector3(rx_, ry_, rz_);
            numAmmoPistol = numAmmoPistol_;
            numAmmoRifle = numAmmoRifle_;
            numBomb = numAmmoBomb_;
        }
    }
}
