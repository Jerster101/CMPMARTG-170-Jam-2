using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Vector3 moveDirection;
    public Vector3 teleportLoc;
    public GameObject Player;


    //Remove player controls if this is not my player
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) enabled = !enabled;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        teleportLoc = new Vector3(49.64f, 1f, -77.43f);

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("pressed p");
            Player.transform.position = teleportLoc;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Weapon")
        {
            moveDirection = transform.position - col.transform.position;
            GetComponent<Rigidbody>().AddForce(moveDirection.normalized * 250f);
        }
    }
}