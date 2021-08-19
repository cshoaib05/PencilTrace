using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject Fill;
    public GameObject Line;
    public GameObject FillPrefab;
    public GameObject Marker;
   
    public static Color selectedColor = Color.black;
    public static string mode;
    public static int trimCount = 0;
    public static int sortLayerCount = 4;
    int fillcount = 0;
    public List<GameObject> ColorList;
    Vector3 camPos;

    public ParticleSystem[] confetti;

    private void Start()
    {
        mode = "fill";
    }

    private void Update()
    {
        if (mode == "fill")
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                CreateFill();
                fillcount++;
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
            {

                Marker.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(20f, 20f, 1f));
                Fill.transform.localScale = Fill.transform.localScale * 1.05f;
            }
        }

    }


    void CreateFill()
    {
        Fill = Instantiate(FillPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 3f)), Quaternion.identity);
        Fill.GetComponent<SpriteRenderer>().color = selectedColor;
        if (sortLayerCount != 4)
        {
            Fill.GetComponent<SpriteRenderer>().sortingOrder = sortLayerCount;
        }
        ColorList.Add(Fill);
        sortLayerCount--;
    }



    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
