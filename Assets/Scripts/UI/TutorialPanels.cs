using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TutorialPanels : MonoBehaviour
{
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private List<GameObject> panels = new List<GameObject>();
    private int _currentPanel = 0;

    private void OnEnable()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(true);
        _currentPanel = 0;
        SetActivePanel(0);
    }

    public void SetPrevPanel()
    {
        if (_currentPanel > 0)
        {
            _currentPanel--;
            SetActivePanel(_currentPanel);
        }

        if (!rightButton.activeInHierarchy)
            rightButton.SetActive(true);

        if (_currentPanel == 0)
        {
            leftButton.SetActive(false);
        }
    }

    public void SetNextPanel()
    {
        if (_currentPanel < panels.Count - 1)
        {
            _currentPanel++;
            SetActivePanel(_currentPanel);
        }

        if (!leftButton.activeInHierarchy)
            leftButton.SetActive(true);

        if (_currentPanel == panels.Count - 1)
        {
            rightButton.SetActive(false);
        }
    }

    private void SetActivePanel(int index)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(false);
            if (i == index)
                panels[i].SetActive(true);
        }
    }
}