using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private GameObject[] Walls;
    private GameObject[] Crates;

    // Start is called before the first frame update
    void Start()
    {
        Walls = GameObject.FindGameObjectsWithTag("Walls");
        Crates = GameObject.FindGameObjectsWithTag("Crates");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Move(Vector2 direction)
    {
        if (CrateBlocked(transform.position, direction))
            return false;
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    public bool CrateBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;

        foreach (var wall in Walls)
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
                return true;

        foreach (var crate in Crates)
            if (crate.transform.position.x == newPos.x && crate.transform.position.y == newPos.y)
                return true;

        return false;
    }
}
