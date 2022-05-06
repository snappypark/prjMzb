using UnityEngine;

public class model : nj.Obj
{
    public enum eType
    {
        Male = 0,
        Female,
        Zombie_Female,
        Zombie_Male,
        Max,
    }

    public enum Equip
    {
        Melee = 0,
        Pistol,
        Rifle,
        Bomb,

        None,
    }
    
    // root
    [SerializeField] public Transform tranHead;
    [SerializeField] public Transform tranHand;
    [SerializeField] public Animator Ani;

    [SerializeField] public Transform body;
    [HideInInspector] public Transform head;
    [HideInInspector] public GameObject melee;
    [HideInInspector] public tranGun pistol;
    [HideInInspector] public tranGun rifle;
    [HideInInspector] public GameObject bomb;

    public void OnSetEquip(Equip state)
    {
        switch (state)
        {
            case Equip.Melee:
                melee.SetActive(true);
                if(pistol != null)
                    pistol.gameObject.SetActive(false);
                if (rifle != null)
                    rifle.gameObject.SetActive(false);
                if (bomb != null)
                    bomb.SetActive(false);
                break;
            case Equip.Pistol:
                melee.SetActive(false);
                if (pistol != null)
                    pistol.gameObject.SetActive(true);
                if (rifle != null)
                    rifle.gameObject.SetActive(false);
                if (bomb != null)
                    bomb.SetActive(false);
                break;
            case Equip.Rifle:
                melee.SetActive(false);
                if (pistol != null)
                    pistol.gameObject.SetActive(false);
                if (rifle != null)
                    rifle.gameObject.SetActive(true);
                if (bomb != null)
                    bomb.SetActive(false);
                break;
            case Equip.Bomb:
                melee.SetActive(false);
                if (pistol != null)
                    pistol.gameObject.SetActive(false);
                if (rifle != null)
                    rifle.gameObject.SetActive(false);
                if (bomb != null)
                    bomb.SetActive(true);
                break;
            default:
                melee.SetActive(false);
                if (pistol != null)
                    pistol.gameObject.SetActive(false);
                if (rifle != null)
                    rifle.gameObject.SetActive(false);
                if (bomb != null)
                    bomb.SetActive(false);
                break;
        }
    }

    public void SetSkin(byte iSkinMat_)
    {
        body.GetComponent<SkinnedMeshRenderer>().sharedMaterial = core.units.mats[iSkinMat_];
        head.GetComponent<MeshRenderer>().sharedMaterial = core.units.mats[iSkinMat_];
    }

    public static int AniId_bRun, AniId_nAct, AniId_State, AniId_Attack;
    public static void InitAniIDs()
    {
        AniId_bRun = Animator.StringToHash("bRun");
        AniId_nAct = Animator.StringToHash("nAct");
        AniId_State = Animator.StringToHash("state");

        AniId_Attack = Animator.StringToHash("Base Layer.Attack");

    }

    public static eType GetRandType(eType idx0, eType idx2)
    {
        return (eType)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static Melee GetRandMelee(Melee idx0, Melee idx2)
    {
        return (Melee)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
}
