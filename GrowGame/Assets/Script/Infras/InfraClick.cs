using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfraClick : MonoBehaviour
{
    public GameObject uiPanel; 
    public Text titleText;
    public Camera cam;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        uiPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                GameObject hitObj = hit.collider.gameObject;
                ShowUIFor(hitObj, hit.point);
                return;
            }

            uiPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) uiPanel.SetActive(false);
    }

    void ShowUIFor(GameObject obj, Vector3 hitWorldPos)
    {
        uiPanel.SetActive(true);
        if (titleText != null) titleText.text = obj.name;

        Vector3 screenPos = cam.WorldToScreenPoint(hitWorldPos);
        uiPanel.transform.position = screenPos + new Vector3(0, 50f, 0);
    }
}
