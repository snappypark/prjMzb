using UnityEngine;
using UnityEngine.UI;

public class ui_netstate : MonoBehaviour
{
    [SerializeField] Text _lb;

    /*
    private void LateUpdate()
    {
        switch (Photon.Pun.PhotonNetwork.NetworkClientState)
        {
            case Photon.Realtime.ClientState.Disconnected:
                break;
        }
        _lb.text = string.Format("{0}",
            Photon.Pun.PhotonNetwork.NetworkClientState);
    }
    */
}
