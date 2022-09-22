using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SetChoice : MonoBehaviour, GetChoice
{
    PhotonView pv;
    public string enemyStringVar;
    public Image enemyChoice;
    public Sprite Rock, Paper, Scissors, Interogation;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    public void TakeChoice(string choice)
    {

        Debug.Log("DEBUGGER TAKE STRING...");

        pv.RPC("RPC_TakeString", RpcTarget.Others, choice);

        //if (pv.IsMine)
        //{
        //    switch (choice)
        //    {
        //        case "Rock":
        //            enemyChoice.GetComponent<Image>().sprite = Rock;
        //            break;

        //        case "Paper":
        //            enemyChoice.GetComponent<Image>().sprite = Paper;
        //            break;

        //        case "Scissors":
        //            enemyChoice.GetComponent<Image>().sprite = Scissors;
        //            break;


        //    }
        //}
        //else
        //{
        //    switch (choice)
        //    {
        //        case "Rock":
        //            enemyChoice.GetComponent<Image>().sprite = Rock;
        //            break;

        //        case "Paper":
        //            enemyChoice.GetComponent<Image>().sprite = Paper;
        //            break;

        //        case "Scissors":
        //            enemyChoice.GetComponent<Image>().sprite = Scissors;
        //            break;

        //    }
        //}


    }

    //public void TakeString(string enemyString)
    //{
    //    pv.RPC("RPC_TakeString", RpcTarget.All, enemyString);
    //}

    [PunRPC]
    void RPC_TakeString(string enemyString)
    {
        enemyStringVar = enemyString;
        switch (enemyString)
        {
            case "Rock":
                enemyChoice.GetComponent<Image>().sprite = Rock;
                break;

            case "Paper":
                enemyChoice.GetComponent<Image>().sprite = Paper;
                break;

            case "Scissors":
                enemyChoice.GetComponent<Image>().sprite = Scissors;
                break;
        }

        //if (RoomManager.instance.managerMultiplayer1 != null)
        //{
            RoomManager.instance.managerMultiplayer1.Compare(enemyString);
        //}
        //else
        //{
        //    RoomManager.instance.managerMultiplayer2.Compare(enemyString);
        //}
    }

}
