using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    PhotonView pv;
    public GameObject playerUI;
    public GameObject enemyChoice;
    public PlayerManager playerManager;
    public ManagerMultiplayer managerMulti;
    public string answer;
    
    // Start is called before the first frame update

    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)pv.InstantiationData[0]).GetComponent<PlayerManager>();
        StartCoroutine(GetBase());
    }
    void Start()
    {
        if (!pv.IsMine)
        {
            Destroy(playerUI);
        }
    }

    IEnumerator GetBase()
    {
        yield return new WaitForSeconds(2);
        enemyChoice = playerManager.GetEnemyBase();
        if (pv.IsMine)
        {
            //enemyChoice.SetActive(true);
            enemyChoice.transform.localScale = new Vector3(0.62587f, 0.62587f, 0.62587f);
        }
    }

    public void TakeString(string enemyString)
    {
        Debug.Log("DEBUGGER TAKE STRING...");
        setAnswer(enemyString);
        pv.RPC("RPC_TakeString", RpcTarget.Others, enemyString);
    }

    [PunRPC]
    void RPC_TakeString(string enemyString)
    {
        Debug.Log("DEBUGGER RPC TAKE STRING...");
        if (!pv.IsMine)
        {
            Debug.Log("DEBUGGER RPC TAKE STRING... RETURN");
            return;
        }

        Debug.Log("DEBUGGER RPC TAKE STRING... OUT");
        Debug.Log("RPC_TakeString");
        enemyChoice.GetComponent<GetChoice>()?.TakeChoice(enemyString);

        //switch (enemyString)
        //{
        //    case "Rock":
                
        //        break;

        //    case "Paper":
        //        enemyChoice.GetComponent<Image>().sprite = Paper;
        //        break;

        //    case "Scissors":
        //        enemyChoice.GetComponent<Image>().sprite = Scissors;
        //        break;

        //}


    }

    public void setAnswer(string answerString)
    {
        answer = answerString;
    }
}
