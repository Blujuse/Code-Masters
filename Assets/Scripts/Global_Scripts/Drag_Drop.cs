using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_Drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{ // The above are used to handle mouse pointer events, they are taken from the EventSystems libary
    [Header("Canvas References")]
    [SerializeField] private Canvas Canvas;

    [Header("Drop_Slot")]
    public Drop_Slot Drop_Box;

    [Header("Private Variables")]
    private RectTransform RT;
    private CanvasGroup CG;
    private Vector3 ReturnPos;

    private void Awake()
    {
        // These simply get these components from the game object which this script is attached to 
        RT = GetComponent<RectTransform>();
        CG = GetComponent<CanvasGroup>();

        ReturnPos = transform.position;
    }

    // this is called when the game object is clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");

        if (Drop_Box != null)
        {
            Drop_Box.Cross.SetActive(false);
            Drop_Box.HasFailSoundPlayed = false;
        }
       
    }

    // this is called when the mouse starts dragging the game object
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        // turning blocksRaycasts to false allows the object to interact with other game objects behind it
        CG.blocksRaycasts = false;
    }

    // this is called while the mouse is dragging the game object
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        /* this moves the objects transform by the amount of the mouses movement
        the division of Canvas.scaleFacter ensures that the object moves smoothly even when the canvas is scaled */
        RT.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }

    // this is called once the mouse stops dragging the object
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        // this restores the orignal way that the objects raycast worked
        CG.blocksRaycasts = true;

        if (gameObject.CompareTag("CodeBlock_Up"))
        {
            transform.position = ReturnPos;
        }
        else if (gameObject.CompareTag("CodeBlock_Down"))
        {
            transform.position = ReturnPos;
        }
        else if (gameObject.CompareTag("CodeBlock_Left"))
        {
            transform.position = ReturnPos;
        }
        else if (gameObject.CompareTag("CodeBlock_Right"))
        {
            transform.position = ReturnPos;
        }
    }
}
