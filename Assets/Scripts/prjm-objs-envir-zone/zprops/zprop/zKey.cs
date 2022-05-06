using System.Runtime.CompilerServices;
using UnityEngine;

public class zKey
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnKey(zone zn, cel1l.Type keyType)
    {
        audios.Inst.PlaySound(audios.eSoundType.key);
        zn.EffectPrp(zprps.eKey, keyType);
        zn.RemovePrp(zprps.eKey, keyType);

        cel1l.Type doorType = cel1l.Type.Door1 + (keyType - cel1l.Type.Key1);
        zn.EffectPrp(zprps.eDoor, doorType);
        zn.RemovePrp(zprps.eDoor, doorType);

        zn.changed = true;
    }
}
