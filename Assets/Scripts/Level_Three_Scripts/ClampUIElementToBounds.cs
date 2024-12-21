using UnityEngine;

public class ClampUIElementToBounds : MonoBehaviour
{
    [Header("Rect Transforms")]
    public RectTransform areaRect;
    private RectTransform rectTransform;
    private RectTransform[] overlapArea;

    [Header("Plug References")]
    public Grid_Movement GridMove;
    
    [Header("Puzzle 2 Blocks")]
    public string overlapAreaTag;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
       
        // Find all game objects in the scene with the specified tag and add them to the overlapArea array
        GameObject[] overlapObjects = GameObject.FindGameObjectsWithTag(overlapAreaTag);
       
        overlapArea = new RectTransform[overlapObjects.Length];
        
        for (int i = 0; i < overlapObjects.Length; i++)
        {
            overlapArea[i] = overlapObjects[i].GetComponent<RectTransform>();
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = rectTransform.position;
        Vector3 minPos = areaRect.position - new Vector3(areaRect.rect.width / 2, areaRect.rect.height / 2, 0);
        Vector3 maxPos = areaRect.position + new Vector3(areaRect.rect.width / 2, areaRect.rect.height / 2, 0);

        pos.x = Mathf.Clamp(pos.x, minPos.x + rectTransform.rect.width / 2, maxPos.x - rectTransform.rect.width / 2);
        pos.y = Mathf.Clamp(pos.y, minPos.y + rectTransform.rect.height / 2, maxPos.y - rectTransform.rect.height / 2);

        rectTransform.position = pos;

        if (overlapArea != null)
        {
            Vector3[] corners = new Vector3[4];
            Rect overlapRect = new Rect(); // declare overlapRect outside of loop

            for (int i = 0; i < overlapArea.Length; i++)
            {
                overlapArea[i].GetWorldCorners(corners);

                overlapRect.x = corners[0].x;
                overlapRect.y = corners[0].y;
                overlapRect.width = corners[2].x - corners[0].x;
                overlapRect.height = corners[2].y - corners[0].y;

                Vector3 position = rectTransform.position;


                if (overlapRect.Contains(position) && gameObject.CompareTag("Plug_Two"))
                {
                    // Do something if the movable object is inside the overlap area
                    Debug.Log("Get Out");

                    transform.position = GridMove.CurrentPos;
                }
            }
        }
    }
}

