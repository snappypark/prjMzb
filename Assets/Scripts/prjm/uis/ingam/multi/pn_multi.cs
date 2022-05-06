using System.Collections.Generic;
using UnityEngine;

public class pn_multi : MonoBehaviour
{
    [SerializeField] public pnn_chat chat;
    [SerializeField] public pnn_plyors players;
    [SerializeField] public pnn_room room;

    [SerializeField] public lbCenter lbCenter;

    public void OnActive(ui_ingam.State state_)
    {
        bool isMulti = state_ != ui_ingam.State.none && state_ != ui_ingam.State.solo;

        gameObject.SetActive(isMulti);
        //chat.OnActive(isMulti);
        //players.gameObject.SetActive(isMulti);
        room.OnActive(state_ == ui_ingam.State.multi_wait);

        if (!isMulti && uis.pops.result.endMulti.gameObject.activeSelf)
            uis.pops.result.endMulti.gameObject.SetActive(true);
    }

    public void Refresh_RelatedPlayers(ref List<boltPlyor> plyors, multis.Mode mode)
    {
        room.RefreshPlayerNum(plyors.Count);
        players.Refresh(ref plyors, mode);
    }
}
