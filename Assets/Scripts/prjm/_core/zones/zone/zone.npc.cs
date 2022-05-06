using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;

public partial class zone
{
    [HideInInspector] public Queue<short> waitNpcs = new Queue<short>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SpawnNpc()
    {
        if (waitNpcs.Count == 0)
            return false;
        short cdx = waitNpcs.Dequeue();
        core.unitClones[cdx].entity.OnSpawn();
        core.unitClones.pulses.Enqueue(cdx);
        return true;
    }

    [HideInInspector] public Queue<iff> Mobs = new Queue<iff>();

}
