using UnityEngine;

public partial class units
{
    public const byte eAniCtrlPlayor = 0, eAniCtrlNpcCitizen = 1, eAniCtrlNpcZombie = 2;
    
    [System.Serializable]
    public class aniControllers
    {
        [SerializeField]
        RuntimeAnimatorController[] _aniControllers;
        public RuntimeAnimatorController this[byte idx] { get { return _aniControllers[idx]; } }
    }
}
