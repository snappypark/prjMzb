public class collis : nj.ObjsQuePool<collis, nj.qObj>
{
    public const byte eBox = 0, eCapsule = 1, eOutOf = 222;
    
    short[] _numClones = new short[] { 18, 18 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }
    protected override bool getInitActiveOfType(byte type) { return true; }

    public colliCubeRoller cubeRoller = new colliCubeRoller(3, 3, 1, 3, 1);

    public void Clear()
    {
        UnuseAllGamObj();
        cubeRoller.ResetCt();
    }
}
