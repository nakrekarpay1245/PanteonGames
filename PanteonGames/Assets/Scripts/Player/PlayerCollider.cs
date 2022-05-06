using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private bool isGrounded;

    public static PlayerCollider playerCollider;
    private void Awake()
    {
        if (playerCollider == null)
        {
            playerCollider = this;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("FireObstacle"))
        {
            PlayerMovement.playerMovement.RespawnAtStartLine();
        }

        if (other.gameObject.CompareTag("FinishLine"))
        {
            PlayerMovement.playerMovement.PaintState();
            UserInterfaceManager.userInterfaceManager.VisibleDrawPercentage();
            UserInterfaceManager.userInterfaceManager.HideGameUI();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("UnFireObstacle"))
        {
            //Debug.Log("UnFireObstacle");
            PlayerMovement.playerMovement.CantMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            UserInterfaceManager.userInterfaceManager.IncreaseGemPoint();
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(other.gameObject,1);
        }
    }
    public bool IsPlayerGrounded()
    {
        return isGrounded;
    }
}
