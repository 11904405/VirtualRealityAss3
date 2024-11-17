using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HandUIInteraction : MonoBehaviourPun
{
    public Button resetGameButton;
    public Button readyToPlayButton;

    public GameObject networkManager;
    private int playersCounter;
    private int rdyPlayers;

    public GameObject playerRig;

    // Start is called before the first frame update
    void Start()
    {
        resetGameButton.onClick.AddListener(ResetGame);
        readyToPlayButton.onClick.AddListener(ReadyToPlay);

        networkManager = GameObject.FindGameObjectWithTag("NetworkManager");


        rdyPlayers = 0;
        


    }

    // Update is called once per frame
    void Update()
    {
        playersCounter = PhotonNetwork.PlayerList.Length;

    }

    void ResetGame()
    {
        photonView.RPC("resetGamePositionAll", RpcTarget.All);
    }

    void ReadyToPlay()
    {
        photonView.RPC("increaseRdyToPlayCounter", RpcTarget.All);

        if(rdyPlayers == playersCounter)
        {
            photonView.RPC("resetGamePositionAll", RpcTarget.All);
            photonView.RPC("startGame", RpcTarget.All);

        }
    }

    [PunRPC]
    public void startGame()
    {

    }

    [PunRPC]
    public void resetGamePositionAll()
    {
        playerRig.GetComponent<LocomotionWithHandMov>().resetMoveSpeed();
        playerRig.transform.position = (new Vector3(0, playerRig.transform.position.y, 13.5f + (networkManager.GetComponent<NetworkManager>().playerID) * 35));
    }

    [PunRPC]
    public void increaseRdyToPlayCounter()
    {
        rdyPlayers += 1;
    }
}
