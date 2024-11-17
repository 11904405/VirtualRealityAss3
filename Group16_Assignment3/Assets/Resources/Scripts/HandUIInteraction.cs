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

    private bool thisPlayerRdy;

    public GameObject playerRig;

    public GameObject start;
    public GameObject youWin;
    public GameObject youLose;

    // Start is called before the first frame update
    void Start()
    {
        resetGameButton.onClick.AddListener(ResetGame);
        readyToPlayButton.onClick.AddListener(ReadyToPlay);

        networkManager = GameObject.FindGameObjectWithTag("NetworkManager");


        rdyPlayers = 0;

        thisPlayerRdy = false;




    }

    // Update is called once per frame
    void Update()
    {
        playersCounter = PhotonNetwork.PlayerList.Length;

    }

    void ResetGame()
    {
        photonView.RPC("resetValues", RpcTarget.All);
        photonView.RPC("resetGamePositionAll", RpcTarget.All);
    }

    void ReadyToPlay()
    {
        if (!thisPlayerRdy)
        {
            thisPlayerRdy = true;
            photonView.RPC("increaseRdyToPlayCounter", RpcTarget.All);

            if (rdyPlayers == playersCounter)
            {
                photonView.RPC("resetGamePositionAll", RpcTarget.All);
                photonView.RPC("startGame", RpcTarget.All);

            }
        }
       
    }

    [PunRPC]
    public void startGame()
    {
        start.SetActive(true);
        StartCoroutine(DeactivateAfterSeconds());
    }


    IEnumerator DeactivateAfterSeconds()
    {
        yield return new WaitForSeconds(3f);
        start.SetActive(false);
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

    [PunRPC]
    public void resetValues()
    {
        thisPlayerRdy = false;
        youWin.SetActive(false);
        youLose.SetActive(false);
        playerRig.GetComponent<WinLoseNotificationScript>().resetGameFinished();
    }
}
