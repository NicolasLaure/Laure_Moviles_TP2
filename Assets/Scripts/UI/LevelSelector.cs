using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private int maxUnlockedLevel = 1;
    [SerializeField] private List<GameObject> levelButtons = new List<GameObject>();

    [SerializeField] private Color defaultButtonColor;
    [SerializeField] private Color lockedButtonColor;

    void OnEnable()
    {
        TurnOFFLockedLevels();
    }

    public void TurnOFFLockedLevels()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].GetComponent<Button>().enabled = false;
            levelButtons[i].GetComponent<Image>().color = lockedButtonColor;

            if (i < maxUnlockedLevel)
            {
                levelButtons[i].GetComponent<Image>().color = defaultButtonColor;
                levelButtons[i].GetComponent<Button>().enabled = true;
            }
        }
    }
}