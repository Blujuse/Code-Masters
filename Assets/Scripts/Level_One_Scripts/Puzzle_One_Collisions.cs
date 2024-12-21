using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_One_Collisions : MonoBehaviour
{
    [Header("Drop_Slot Reference")]
    public Drop_Slot Drop_Point;
    
    [Header("Private Variables")]
    private BoxCollider Collider;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<BoxCollider>();
        Collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Drop_Point.Correct == true)
        {
            Collider.enabled = true;
        }
    }
}
