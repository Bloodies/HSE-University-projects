using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{

    public float movementSpeed;
    public float sensitivyX, sensitivyY;
    public new GameObject camera;

    private float xAxisClamp;
    public Menu_controller mc;
    public Keys_config InputManager;
    public Settings_controller oc;
    public bool isAc;
    
    void Start()    // Use this for initialization
    {
        Cursor.lockState = CursorLockMode.Locked;
        mc = FindObjectOfType<Menu_controller>();
        oc = FindObjectOfType<Settings_controller>();
        InputManager = FindObjectOfType<Keys_config>();
        isAc = mc.isActiveCanvas;
    }
    
    void Update()   // Update is called once per frame
    {
        if (mc.isActiveCanvas == true)
            return;
        MovePlayer();

    }

    private void FixedUpdate()
    {
        if (mc.isActiveCanvas == true)
            return;
        CameraLook();
    }

    void MovePlayer()
    {
        float axisY = 0, axisX = 0;
        if (Input.GetKey(InputManager.forward)) { axisY = 1; }
        if (Input.GetKey(InputManager.back)) { axisY = -1; }

        if (Input.GetKey(InputManager.right)) { axisX = 1; }
        if (Input.GetKey(InputManager.left)) { axisX = -1; }

        float forward = (axisY * movementSpeed) * Time.deltaTime;
        float straffe = (axisX * movementSpeed) * Time.deltaTime;

        transform.Translate(straffe, 0, forward);

    }

    void CameraLook()
    {
        // Get values from OptionsControl (json file)
        sensitivyX = oc._gameConfig.horizontalSensitivy;
        sensitivyY = oc._gameConfig.verticalSensitivy;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float msx = mouseX * sensitivyX * 5;
        float msy = mouseY * sensitivyY * 5;

        xAxisClamp -= msy;

        Vector3 RotCam = camera.transform.rotation.eulerAngles;
        Vector3 RotPlayer = this.transform.rotation.eulerAngles;

        RotCam.x -= msy;
        RotCam.z = 0;
        RotPlayer.y += msx;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            RotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            RotCam.x = 270;
        }

        camera.transform.rotation = Quaternion.Euler(RotCam);
        this.transform.rotation = Quaternion.Euler(RotPlayer);
    }
}