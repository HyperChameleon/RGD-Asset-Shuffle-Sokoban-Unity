using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject[] Walls;
    private GameObject[] Crates;

    private bool playerMove;

    // Start is called before the first frame update
    void Start()
    {
        Walls = GameObject.FindGameObjectsWithTag("Walls");
        Crates = GameObject.FindGameObjectsWithTag("Crates");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveinput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveinput.Normalize();

        if (moveinput.sqrMagnitude > 0.5)
        {
            if (playerMove)
            {
                playerMove = false;
                Move(moveinput);
            }
        }
        else
            playerMove = true;
    }

    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5)
            direction.x = 0;
        else
            direction.y = 0;

        direction.Normalize();

        if (Blocked(transform.position, direction))
            return false;
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    public bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;

        foreach(var wall in Walls)
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
                return true;

        foreach (var crate in Crates)
        {
            if (crate.transform.position.x == newPos.x && crate.transform.position.y == newPos.y)
            {
                Push cratePush = crate.GetComponent<Push>();

                if (cratePush && cratePush.Move(direction))
                    return false;
                else
                    return true;
            }
        }

        return false;
    }
}
