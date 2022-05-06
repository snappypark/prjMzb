using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;

public partial class unit : nj.cObj
{
    public enum TranType { Default, Rigid, }

    [HideInInspector] public Pt pt = Pt.Zero;
    [HideInInspector] public cel1l cell = cel1l.Empty;

    [SerializeField] public hud_ hud;
    [HideInInspector] public model model;
    [HideInInspector] public Transform tran;

    [HideInInspector] public attrib_ attb = new attrib_();
    [HideInInspector] public weapon wp = new weapon();
    
    [HideInInspector] public unitEntity entity;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void FillHpAndWeapon(int amPistol, int amRifle, int nBomb)
    {
        attb.hp.ResetMax();
        wp.Fill(amPistol, amRifle, nBomb);
        hud.SetHpBar(1);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetSkinBack()
    {
        model.SetSkin(attb.skinIdx);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanBeTraced(unit u, float sqrMaxDist)
    {
        if (attb.hp.isZero())//|| _o.cell.pt.y != u.cell.pt.y)
            return false;

        switch (cell.type)
        {
            case cel1l.Type.AreaWin:
                return false;
            default:
                return true;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3 GetNextPos(Rigidbody rigi, float deltaFrame)
    {
        return tran.localPosition + rigi.velocity * deltaFrame;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveAndLook(bool hasMove, Rigidbody rigi, Vector3 nDir, Vector3 nLook)
    {
        if (hasMove)
            rigi.AddForce(nDir * attb.moveSpd);
        tran.forward = nLook;
    }


    [System.Serializable]
    public class hud_
    {
        public enum Type
        {
            None,
            OnlyMiniMap,
            HpBar,
            Name,
        }

        [SerializeField] Transform _tran;
        [SerializeField] TextMesh _lbName;
        [SerializeField] Transform _tranHpHover;
        [SerializeField] Transform _tranHp; 

         [SerializeField] GameObject _goShield;

        [SerializeField] public SpriteRenderer ring;
        [SerializeField] public SpriteRenderer mini;
        [SerializeField] public SpriteRenderer miniArea;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(Type type)
        {
            _tran.gameObject.SetActive(type != Type.None);
            switch (type)
            {
                case Type.HpBar:
                    _tranHpHover.gameObject.SetActive(true);
                    _lbName.gameObject.SetActive(false);
                    //SetHpBar();
                    break;
                case Type.Name:
                    _tranHpHover.gameObject.SetActive(false);
                    _lbName.gameObject.SetActive(true);
                    break;
                case Type.OnlyMiniMap:
                    _tranHpHover.gameObject.SetActive(false);
                    _lbName.gameObject.SetActive(false);
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPos(Vector3 pos)
        {
            _tran.localPosition = pos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPos(Vector3 pos, Type type)
        {
            _tran.localPosition = pos;
            Set(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetHpBar(float value01)
        {
            _tranHp.localScale = new Vector3(value01, 1, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetName(string name)
        {
            _lbName.text = name;
            Set(Type.Name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetShield(bool value)
        {
            _goShield.SetActive(value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMiniColor(Color color)
        {
            mini.color = color;
        }


        const float _sqrDistStandardForMini = 169;//13
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActiveMini_BySqrDist(float sqrDist)
        {
            mini.enabled = sqrDist < _sqrDistStandardForMini;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActiveMini(bool value)
        {
            mini.enabled = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActiveMiniArea(bool value)
        {
            miniArea.enabled = value;
        }
        
    }
}
