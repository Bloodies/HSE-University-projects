using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int HP = 5;
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody rigidbody;
    private PhotonView photonView;
    private MeshRenderer meshRenderer;

    private bool isRed;
    private bool grounded = false;

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
        rigidbody = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        IsGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(-Time.deltaTime * 5, 0, 0);
            if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Time.deltaTime * 5, 0, 0);
            if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(0, 0, Time.deltaTime * 5);
            if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(0, 0, -Time.deltaTime * 5);

            if (!grounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            else
            {
                isRed = false;
            }
        }

        if (grounded)
        {
            Jump();
        }
        else
        {
            
        }
    }

    private void Jump()
    {
        //rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        transform.Translate(0, Time.deltaTime * 500, 0);
    }

    private void IsGrounded()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 0.3f);
        grounded = collider.Length > 1;
    }
}