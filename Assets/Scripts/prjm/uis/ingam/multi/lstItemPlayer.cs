using UnityEngine;
using UnityEngine.UI;

public class lstItemPlayer : MonoBehaviour
{
    [SerializeField] Image _signal;
    [SerializeField] Text _lb;


    public void AssignOnEscape(int idx, boltPlyor p)
    {
        gameObject.SetActive(true);
        _lb.text = string.Format("{0}. {1}", idx + 1, p.name);

        _lb.color = p.entity.IsOwner ? pnn_plyors.sky : pnn_plyors.blue;

        // add time
    }

    public void AssignOnBattle(int idx, boltPlyor p)
    {
        gameObject.SetActive(true);
        _lb.text = string.Format("{0}: {1}", p.name, p.state.score);
        switch (p.ally) {
            case 0: _lb.color = p.entity.IsOwner ? pnn_plyors.sky : pnn_plyors.blue; break;
            case 1: _lb.color = p.entity.IsOwner ? pnn_plyors.org : pnn_plyors.red; break;
        }
    }
}
