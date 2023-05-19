using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public List<GameObject> images;
    public List<GameObject> pages;
    public List<GameObject> descriptionPages;
    public int pageNum = 0;
    private bool selected = false;

    void Start()
    {
        if (pageNum == 0)
        {
            images[1].SetActive(true);
            images[0].SetActive(false);
        }
        else
        {
            images[0].SetActive(true);
            images[1].SetActive(false);
        }
        for(int i = 1; i<pages.Count; i++)
        {
            pages[i].SetActive(false);
        }
        pages[0].SetActive(true);
        for (int i = 1; i < descriptionPages.Count; i++)
        {
            descriptionPages[i].SetActive(false);
        }
        descriptionPages[0].SetActive(true);
    }

    void Update()
    {
        if (selected == true)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].activeSelf == true && (i != pageNum))
                {
                    images[0].SetActive(true);
                    images[1].SetActive(false);
                    selected = false;
                    break;
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selected == false)
        {
            images[1].SetActive(true);
            images[0].SetActive(false);
            selected = true;
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].SetActive(false);
            }
            pages[pageNum].SetActive(true);
            for (int i = 0; i < descriptionPages.Count; i++)
            {
                descriptionPages[i].SetActive(false);
            }
            descriptionPages[pageNum].SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        images[1].SetActive(true);
        images[0].SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected == false)
        {
            images[0].SetActive(true);
            images[1].SetActive(false);
        }
    }
}
