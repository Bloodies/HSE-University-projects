using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    private MeshRenderer meshRenderer;

    private bool isRed;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isRed);
        }
        else
        {
            isRed = (bool)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(-Time.deltaTime * 5, 0, 0);
            if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Time.deltaTime * 5, 0, 0);
        
            if (Input.GetKey(KeyCode.Space))
            {
                isRed = true;
            }
            else
            {
                isRed = false;
            }
        }

        if (isRed)
        {
            
        }
        else
        {
            
        }
    }
}
