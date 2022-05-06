using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentCharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform finishLine;
    [SerializeField]
    private Transform startLine;


    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Animator animator;

    [Header("Variables")]
    [SerializeField]
    private float speed;

    private bool canMove;
    private bool isFall;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        navMeshAgent.SetDestination(finishLine.position);
        navMeshAgent.speed = 0;
        OpponentManager.opponentManager.AddOpponent(this);
    }

    private void Update()
    {
        animator.SetBool("isMove", IsOpponentCanMove());
        animator.SetBool("isFall", IsOpponentFall());
    }

    public void CanMove()
    {
        canMove = true;
        navMeshAgent.speed = speed;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("UnFireObstacle"))
        {
            //Debug.Log("UnFire");
            CantMove();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("FireObstacle"))
        {
            //Debug.Log("Fire");
            RespawnAtStartLine();
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
        //Debug.Log("Speed B: " + navMeshAgent.speed);
        navMeshAgent.speed = 0;
        //Debug.Log("Speed A: " + navMeshAgent.speed);
        canMove = false;
        isFall = true;
        yield return new WaitForSeconds(1.5f);

        Vector3 startPosition = new Vector3(startLine.transform.position.x,
            transform.position.y, startLine.transform.position.z);
        transform.position = startPosition;
        navMeshAgent.speed = speed;
        canMove = true;
        isFall = false;
        //Debug.Log("Go Start Position");
        yield return new WaitForSeconds(1);
    }
    public void CantMove()
    {
        StartCoroutine(CantMoveCoroutine());
    }
    IEnumerator CantMoveCoroutine()
    {
        //Debug.Log("Fall Anim");
        //Debug.Log("Fall Sound");
        navMeshAgent.speed = 0;
        canMove = false;
        yield return new WaitForSeconds(1);

        //Debug.Log("StandUp Anim");
        //Debug.Log("StandUp Sound");
        navMeshAgent.speed = speed;
        canMove = true;
        yield return new WaitForSeconds(1);
    }

    public bool IsOpponentCanMove()
    {
        return canMove;
    }
    public bool IsOpponentFall()
    {
        return isFall;
    }
}
