using UnityEngine;
using UnityEngineEx;

public class pBot : rpr
{
    enum state
    {
        first,
        second,
    }

    public enum Type
    {
        Gun,
        //Fire,
        //Bomb,
    }

    [SerializeField] Transform _gun;

    delay _delayAttack = new delay(1.5f);
    float _speed = 0;

    state _state = state.first;
    //options: delay, bullet speed,
    public override void Assign(cel1l cell)
    {
        _delayAttack = new delay(cell.opts.F1);
        _speed = cell.opts.F2;
        _state = state.first;
        gameObject.SetActive(true);
    }

    public override void UnAssign()
    {
        gameObject.SetActive(false);
    }
    
    void Update()
    {
        unit u = ctrls.Unit;
        if (u.attb.hp.isZero())
            return;
        switch (_state)
        {
            case state.first:
                Vector3 dir = u.tran.localPosition - transform.localPosition;
                if (dir.sqrMagnitude < 100)
                {
                    dir = new Vector3(dir.x, 0, dir.z).normalized;
                    _gun.forward = dir;
                    if (_delayAttack.IsEndAndReset())
                    {
                        _state = state.second;
                    }
                }
                break;
            case state.second:
                abjs.slugs.Fire(111, transform.localPosition.WithGapY(1.2f) + _gun.forward * 0.71f,
                    _gun.forward, _speed, 20);
                audios.Inst.pistol.PlaySound();
                _state = state.first;
                break;
        }
    }
}
