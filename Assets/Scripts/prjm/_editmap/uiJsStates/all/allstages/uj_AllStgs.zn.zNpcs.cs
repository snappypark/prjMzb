using System.Collections;
using UnityEngine;
using UnityEngineEx;


public partial class uj_AllStgs
{
    void generateNpcs_(js.zone_ zn, ref cnd[,] cellnodes)
    {
        zn.npcs.Clear();

        switch (INFO.iBy6) {
            case 1: case 4:
                generateNpcs_Spder(zn, ref cellnodes);
                break;
            default:
                generateNpcs_Basic(zn, ref cellnodes);
                break;
        }
    }

    void generateNpcs_Basic(js.zone_ zn, ref cnd[,] cellnodes)
    {
        float minDist = 9;
        
        int maxNpcs = 1 + (INFO.idxIn100 / 8) + INFO.idxX100
                        + Random.Range(INFO.idxX100, INFO.idxX100+5); // 1 ~ 9, 

        for (int r = 0; r < cnd.nR; r++)
            for (int c = 0; c < cnd.nC; c++)
            {
                cnd cnd = cellnodes[r, c];

                if (maxNpcs <= 0)
                    return;

                if (cnd.dist < minDist)
                    continue;

                float randMinDist = Random.Range(minDist, 12.0f);
                float randMaxDist = Random.Range(12.0f, 17.0f);
                if (cnd.distPath < randMinDist || cnd.distPath > randMaxDist)
                    continue;

                i2 pt = cnd.GetCenter(zn);

                cel1l cell = core.zells[pt];
                if (cell.type == cel1l.Type.Tile)
                {
                    unit.SubType subType = unit.SubType.ZombieLv1 + UnityEngine.Random.Range(0, 3);

                    model.eType modelType = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
                    byte mat = matUnits.GetByZombieLv(subType);
                    byte melee = (byte)model.GetRandMelee(Melee.BaseBallBat, Melee.Handgun);

                    byte head = 0, body = 0;

                    switch (modelType)
                    {
                        case model.eType.Zombie_Female:
                            head = (byte)ZFModel.GetRandHead();
                            body = (byte)ZFModel.GetRandBody();
                            break;
                        case model.eType.Zombie_Male:
                            head = (byte)ZMModel.GetRandHead();
                            body = (byte)ZMModel.GetRandBody();
                            break;
                    }

                    js.Inst.AddNpc(zn, cell.ct, RandEx.Dir8(),
                        modelType, mat, model.Equip.Melee, 1,
                        head, body, melee, units.eAniCtrlNpcZombie, subType, js.SeriType.WithCell);
                    --maxNpcs;
                }
            }
    }


    void generateNpcs_Spder(js.zone_ zn, ref cnd[,] cellnodes)
    {
        float minDist = 12;
        
        int maxNpcs = 1 + (INFO.idxIn100 / 12) + INFO.idxX100
                        + Random.Range(INFO.idxX100, INFO.idxX100+5);

        for (int r = 0; r < cnd.nR; r++)
            for (int c = 0; c < cnd.nC; c++)
            {
                cnd cnd = cellnodes[r, c];

                if (maxNpcs <= 0)
                    return;

                if (cnd.dist < minDist)
                    continue;

                float randMinDist = Random.Range(minDist, 16.0f);
                float randMaxDist = Random.Range(16.0f, 22.0f);
                if (cnd.distPath < randMinDist || cnd.distPath > randMaxDist)
                    continue;

                i2 pt = cnd.GetCenter(zn);

                cel1l cell = core.zells[pt];
                if (cell.type == cel1l.Type.Tile)
                {
                    unit.SubType subType = unit.SubType.ZombieLv4 + UnityEngine.Random.Range(0, 2);

                    model.eType modelType = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
                    byte mat = matUnits.GetByZombieLv(subType);
                    byte melee = (byte)model.GetRandMelee(Melee.BaseBallBat, Melee.Handgun);

                    byte head = 0, body = 0;

                    switch (modelType)
                    {
                        case model.eType.Zombie_Female:
                            head = (byte)ZFModel.GetRandHead();
                            body = (byte)ZFModel.GetRandBody();
                            break;
                        case model.eType.Zombie_Male:
                            head = (byte)ZMModel.GetRandHead();
                            body = (byte)ZMModel.GetRandBody();
                            break;
                    }

                    js.Inst.AddNpc(zn, cell.ct, RandEx.Dir8(),
                        modelType, mat, model.Equip.Melee, 1,
                        head, body, melee, units.eAniCtrlNpcZombie, subType, js.SeriType.WithCell);
                    --maxNpcs;
                }
            }
    }


    void generateNpcs_Gun(js.zone_ zn, ref cnd[,] cellnodes)
    {
        // > 10
        //  %6 == 5
    }
}
