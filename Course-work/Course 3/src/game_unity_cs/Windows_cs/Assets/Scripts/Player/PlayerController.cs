using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(-Time.deltaTime * 5, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Time.deltaTime * 5, 0, 0);
    }
}
