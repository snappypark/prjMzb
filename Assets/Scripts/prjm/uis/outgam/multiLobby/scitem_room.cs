using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mosframe;

public class scitem_room : UIBehaviour, IDynamicScrollViewItem
{
    public Text title;

    private int dataIndex = -1;

    public void onUpdateItem(int idx)
    {
        string ssName = nets.Inst.bolts.GetSessionName(idx);
        title.text = langs.WhomRoom(ssName.Substring(0, ssName.Length - 5));
        this.dataIndex = idx;
    }

    #region UI Action
    public void OnBtn_Join()
    {
        nets.Inst.bolts.JoinSession(dataIndex);
    }
    #endregion

    /*
    public void Initialize(string name, byte currentPlayers, byte maxPlayers)
    {
        roomName = name;

        RoomNameText.text = name;
        RoomPlayersText.text = currentPlayers + " / " + maxPlayers;
    }*/
}
