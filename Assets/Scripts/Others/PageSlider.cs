using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PageSlider : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public ScrollRect scrollRect;
    public float snapSpeed = 10f;
    public int totalPages;
    public float pageFraction = 1;
    // public int pagesNumber;

    private int currentPage  = 0;
    private bool isDragging = false;
    private float targetHorizontalPosition;

    void Start()
    {
        // Ensure the scrollRect is at the starting position
        scrollRect.horizontalNormalizedPosition = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        SnapToPage();
    }

    void Update()
    {
        if (!isDragging)
        {
            float currentPosition = scrollRect.horizontalNormalizedPosition;
            float newPosition = Mathf.Lerp(currentPosition, targetHorizontalPosition, snapSpeed * Time.deltaTime);

            float clampNewPos = Mathf.Clamp(newPosition, 0f, totalPages -1);

            scrollRect.horizontalNormalizedPosition = clampNewPos;
        }

        Debug.Log(currentPage);
    }

    private void SnapToPage()
    {
        float horizontalPosition = scrollRect.horizontalNormalizedPosition;
        Debug.Log("scrollRect horizontal pos: " + horizontalPosition);
        // float pageFraction = 1 / (totalPages - 1); // totalPages: 3
        currentPage = Mathf.RoundToInt(horizontalPosition / pageFraction);

        targetHorizontalPosition = currentPage * pageFraction;
    }

    public int GetCurrentPage()
    {
        return currentPage;
    }

    /* public void GoToNextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            targetHorizontalPosition = (float)currentPage / (totalPages - 1);
        }
    }

    public void GoToPreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            targetHorizontalPosition = (float)currentPage / (totalPages - 1);
        }
    } 
    */
}
