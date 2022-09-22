using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public GameObject [] enemyBase;
    PhotonView PV;
    GameObject playerController;
    Transform spawnPoint;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    public void CreateController()
    {
        playerController = PhotonNetwork.Instantiate(Path.Combine("Prefab", "PlayerController"), Vector3.zero, Quaternion.identity, 0, new object[] { PV.ViewID });

        //if (PV.Owner.IsMasterClient)
        //{
        //    RoomManager.instance.managerMultiplayer1 = playerController.GetComponent<PlayerController>().managerMulti;
        //}
        //else
        //{
        //    RoomManager.instance.managerMultiplayer2 = playerController.GetComponent<PlayerController>().managerMulti;
        //}
        RoomManager.instance.managerMultiplayer1 = playerController.GetComponent<PlayerController>().managerMulti;

        //playerController.GetComponent<PlayerController>().enemyChoice = enemyBase;
    }

    public GameObject GetEnemyBase()
    {
        enemyBase = GameObject.FindGameObjectsWithTag("EnemyBase");

        if (PV.Owner.IsMasterClient)
        {
            return enemyBase[1];
        }
        else
        {
            return enemyBase[0];
        }
        return null;
    }
}
