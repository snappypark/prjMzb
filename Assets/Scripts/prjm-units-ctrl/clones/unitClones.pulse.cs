using System.Runtime.CompilerServices;
using System.Collections.Generic;

public partial class unitClones
{
    public Queue<short> pulses = new Queue<short>();

    void _awakePulse()
    {
        pulses.Enqueue(0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnUpdate_1by1()
    {
        short cdx = pulses.Dequeue();
        if (_cObj[cdx].entity.Command_1by1())
            pulses.Enqueue(cdx);
    }
}
