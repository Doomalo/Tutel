using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescriptionButton : MonoBehaviour, IPointerClickHandler
{
    public List<GameObject> descriptions;
    public int pageNum = 0;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < descriptions.Count; i++)
        {
            descriptions[i].SetActive(false);
        }
        descriptions[0].SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < descriptions.Count; i++)
        {
            descriptions[i].SetActive(false);
        }
        descriptions[pageNum].SetActive(true);
        audioSource.Play();
    }
}
