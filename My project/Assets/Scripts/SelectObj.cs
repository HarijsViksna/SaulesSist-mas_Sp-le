using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectObj : MonoBehaviour
{
    public float maxOutlineDistance = 300f;

    [System.Serializable]
    public class PlanetInfo
    {
        public Transform planetTransform;
        public GameObject panel;
    }

    public List<PlanetInfo> planets = new List<PlanetInfo>(); // List of planet information

    private Outline currentOutline;
    private Transform selectedPlanet;
    private RaycastHit hit;

    public GameManager gameManager; // Reference to the GameManager

    void Update()
    {
        if (selectedPlanet != null && currentOutline != null)
        {
            currentOutline.enabled = false;
            selectedPlanet = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, maxOutlineDistance))
        {
            Transform hitTransform = hit.transform;

            if (hitTransform.CompareTag("Planets") && hitTransform != selectedPlanet)
            {
                selectedPlanet = hitTransform;
                if (selectedPlanet.TryGetComponent<Outline>(out Outline outline))
                {
                    outline.enabled = true;
                    currentOutline = outline;
                }
                else
                {
                    currentOutline = selectedPlanet.gameObject.AddComponent<Outline>();
                    currentOutline.OutlineColor = Color.blue;
                    currentOutline.OutlineWidth = 5.0f;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedPlanet != null)
            {
                if (currentOutline != null)
                {
                    currentOutline.enabled = false;
                }
                currentOutline = selectedPlanet.GetComponent<Outline>();
                if (currentOutline != null)
                {
                    currentOutline.enabled = true;
                }

                // Check if the left click was on the selected planet
                foreach (var planetInfo in planets)
                {
                    if (hit.transform == planetInfo.planetTransform)
                    {
                        // Pause the game
                        gameManager.PauseGame();

                        // Open the panel associated with the selected planet
                        OpenPanel(planetInfo.panel);
                        break;
                    }
                }
            }
        }
    }

    void OpenPanel(GameObject panel)
    {
        if (panel != null)
        {
            // Open the panel
            panel.SetActive(true);
        }
    }
}
