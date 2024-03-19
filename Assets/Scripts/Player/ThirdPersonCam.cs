using Cinemachine;
using UnityEngine;
public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;
    private bool camIsDisabled = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (camIsDisabled)
            return;

        //direction where the player is looking (used for store the forward direction)
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //input direction
        Vector3 inputDir = orientation.forward * vertical + orientation.right * horizontal;

        //rotate the player 
        if(inputDir != Vector3.zero)       
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);    
    }

    //disable input for camera rotation and disable input for player movement
    public void DisableCameraRotation()
    {
        camIsDisabled = true;
        //deactivate the camera input and speed
        CinemachineFreeLook camera = GetComponent<CinemachineFreeLook>();
        camera.m_XAxis.m_InputAxisName = "";
        camera.m_XAxis.m_InputAxisValue = 0f;
        player.GetComponent<PlayerMovement>()?.DisablePlayerMovement();
    }

    //enable input for camera rotation and enable input for player movement
    public void EnableCameraRotation()
    {
        camIsDisabled = false;
        //activate the camera input
        GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
        player.GetComponent<PlayerMovement>()?.EnablePlayerMovement();
    }
}
