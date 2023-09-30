using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_OneWayPlatform : MonoBehaviour
{
    private GameObject currentPlatform;

    [SerializeField] private Collider2D playerCollider;

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Vertical");

        if (direction < 0f)
        {
            if (currentPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCol = currentPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCol);
        yield return new WaitForSeconds(0.75f);
        Physics2D.IgnoreCollision(playerCollider, platformCol, false);
    }
}
