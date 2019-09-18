using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour
{
    public delegate void Jumper();
    public static event Jumper OnCrash;
    public static event Jumper OnSave;

    [SerializeField]
    public List<Transform> jumperPos = new List<Transform>();
    int jumpersCurrentPosition = 0;
    float lastMoveTime;
    float moveDelay = 1.0f;
    float deathDelay = 0.5f;

    [HideInInspector]
    public JumperSpawner jumperSpawner;

    private bool dead = false;

    public LayerMask layerMask;

    private void Start()
    {
        //  transform.position = jumperPos[jumpersCurrentPosition].position;
        UpdatePosition();
        lastMoveTime = Time.time;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(moveDelay);
            MoveToNextPosition();
        }
    }

    void MoveToNextPosition()
    {
        jumpersCurrentPosition++;

        if(jumpersCurrentPosition >= jumperPos.Count)
        {
            DestroyJumper();
        }
        else
        {
            UpdatePosition();
        }

    }

    private void UpdatePosition()
    {
        transform.position = jumperPos[jumpersCurrentPosition].position;
        if (jumperPos[jumpersCurrentPosition].gameObject.tag == "Danger")
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layerMask);


            if (hit.collider == null)
            {
                StartCoroutine(Crash());
                if(OnCrash != null)
                    OnCrash();
                //säg till gamemanager att vi crashat
            }
            else
            {
                if(OnSave != null)
                    OnSave();
                //säg till till gamemanager att vi räddats
            }
        }
    }

    IEnumerator Crash()
    {
        dead = true;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(deathDelay);
        DestroyJumper();
    }

    void DestroyJumper()
    {
        GameObject parent = transform.parent.gameObject;
        jumperSpawner.DestroyJumper(parent);
    }

}
