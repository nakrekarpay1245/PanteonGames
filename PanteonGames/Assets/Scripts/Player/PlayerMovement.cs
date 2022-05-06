using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float moveSpeed;

    [Header("Movement Clamps")]
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float minX;

    private float progress;

    [Header("References")]
    [SerializeField]
    private Rigidbody rigidbodyComponent;

    [SerializeField]
    private GameObject startLine;
    [SerializeField]
    private GameObject finishLine;

    private bool canMove;
    private bool isFall;

    public static PlayerMovement playerMovement;
    private void Awake()
    {
        if (playerMovement == null)
        {
            playerMovement = this;
        }

        rigidbodyComponent = GetComponent<Rigidbody>();

        Vector3 startPosition = new Vector3(startLine.transform.position.x,
            transform.position.y, startLine.transform.position.z);
        transform.position = startPosition;
    }

    void Start()
    {
        moveSpeed = speed;
        //canMove = true;
    }
    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y, transform.position.z);

        progress = (transform.position.z - startLine.transform.position.z) /
            (finishLine.transform.position.z - startLine.transform.position.z);

        UserInterfaceManager.userInterfaceManager.ShowProgress(progress);
    }
    void FixedUpdate()
    {
        //bool isGrounded = PlayerCollider.playerCollider.IsPlayerGrounded();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (canMove)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out RaycastHit raycastHit) /*&& isGrounded*/)
                {
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(raycastHit.point.x, transform.position.y, transform.position.z),
                        Time.deltaTime * 5);
                }
            }

            //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            //{
            //    rigidbodyComponent.AddForce(Vector3.up * jumpForce);
            //    isGrounded = false;
            //}

            rigidbodyComponent.velocity = Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rigidbodyComponent.velocity = Vector3.zero;
        }
    }

    public void RespawnAtStartLine()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        //Debug.Log("Dead Anim");
        //Debug.Log("Dead Sound");
        moveSpeed = 0;
        canMove = false;
        isFall = true;
        yield return new WaitForSeconds(0.35f);

        Vector3 startPosition = new Vector3(startLine.transform.position.x,
            transform.position.y, startLine.transform.position.z);
        transform.position = startPosition;
        moveSpeed = speed;
        canMove = true;
        isFall = false;
        //Debug.Log("Go Start Position");
        yield return new WaitForSeconds(1);
    }
    public void CantMove()
    {
        //Debug.Log("CantMove");
        StartCoroutine(CantMoveCoroutine());
    }
    public void CanMove()
    {
        canMove = true;
    }
    IEnumerator CantMoveCoroutine()
    {
        //Debug.Log("Fall Anim");
        //Debug.Log("Fall Sound");
        moveSpeed = 0;
        canMove = false;
        yield return new WaitForSeconds(1);

        //Debug.Log("StandUp Anim");
        //Debug.Log("StandUp Sound");
        moveSpeed = speed;
        canMove = true;
        yield return new WaitForSeconds(1);
    }

    public void PaintState()
    {
        canMove = false;

        Vector3 finishPosition = new Vector3(finishLine.transform.position.x,
                  transform.position.y, finishLine.transform.position.z);
        transform.position = finishPosition;

        WallManager.wallManager.PaintState(UserInterfaceManager.userInterfaceManager.GetGemPoint() / 10);
    }

    public bool IsPlayerCanMove()
    {
        return canMove;
    }
    public bool IsPlayerFall()
    {
        return isFall;
    }
}
