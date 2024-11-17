using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WinLoseNotificationScript : MonoBehaviourPun
{
    public bool triggerWinLose;
    public GameObject youWin;
    public GameObject youLose;

    private bool gameFinished;

    // Start is called before the first frame update
    void Start()
    {
        gameFinished = false;

        triggerWinLose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerWinLose)
        {
            if (!gameFinished)
                {
                    photonView.RPC("othersLose", RpcTarget.Others);
                    gameFinished = true;
                    youWin.SetActive(true);
                }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FinishWall")
        {
            if (!gameFinished)
            {
                photonView.RPC("othersLose", RpcTarget.Others);
                gameFinished = true;
                youWin.SetActive(true);
            }
        }
        
    }



    [PunRPC]
    public void othersLose()
    {
        gameFinished = true;
        youLose.SetActive(true);        
    }
}
