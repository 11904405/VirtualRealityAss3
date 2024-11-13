using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseNotificationScript : MonoBehaviour
{
    public bool win;
    public bool lose;
    public GameObject youWin;
    public GameObject youLose;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        lose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            youWin.SetActive(true);
        }
        if (lose)
        {
            youLose.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: bin mir noch unsicher wie genau, also es soll wenn geentert wird geschaut werden ob gewonnen oder verloren wurde also welcher player als erster berührt bekommt die you win notification
    }
}
