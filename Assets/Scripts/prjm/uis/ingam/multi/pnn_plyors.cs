using System.Collections.Generic;
using UnityEngine;

public class pnn_plyors : MonoBehaviour
{
    [SerializeField] lstItemPlayer[] _uis;

    public void Refresh(ref List<boltPlyor> players, multis.Mode mode)
    {
        switch (mode)
        {
            case multis.Mode.Escap:
                for (int i = 0; i < players.Count; ++i)
                    _uis[i].AssignOnEscape(i, players[i]);
                break;
            case multis.Mode.Battle:
                for (int i = 0; i < players.Count; ++i)
                    _uis[i].AssignOnBattle(i, players[i]);
                break;
        }

        for (int i = players.Count; i < _uis.Length; ++i)
            _uis[i].gameObject.SetActive(false);
    }
    
    public static readonly Color sky = new Color(0.2f, 0.6f, 1.0f, 0.9f);
    public static readonly Color blue = new Color(0.0f, 0.2f, 1.0f, 0.7f);

    public static readonly Color org = new Color(1.0f, 0.7f, 0.4f, 0.9f);
    public static readonly Color red = new Color(1.0f, 0.4f, 0.2f, 0.7f);
}
