using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Kulikova.Network
{
    public class NetworkService : MonoBehaviourPunCallbacks
    {
        [SerializeField] private byte maxPlayers;
        [SerializeField] private bool isVisible;
        
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            RoomOptions options = new RoomOptions()
            {
                MaxPlayers = maxPlayers,
                IsVisible = isVisible
            };

            PhotonNetwork.JoinOrCreateRoom("test", options, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Player: " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
}