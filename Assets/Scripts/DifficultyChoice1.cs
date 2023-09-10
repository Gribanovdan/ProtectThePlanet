using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyChoice1 : MonoBehaviour
{
    const int numOfBtns = 3;

    [SerializeField]
    Image[] buttonsImages = new Image[numOfBtns];

    [SerializeField]
    Sprite[] buttons = new Sprite[numOfBtns];

    [SerializeField]
    Sprite[] pressedButtons = new Sprite[numOfBtns];

    int chosenDif;
    void Start()
    {
        chosenDif = GunChoice.Difficulty;
        if (chosenDif != 1 || chosenDif != 2 || chosenDif != 3) chosenDif = 1;
        ChooseDif(chosenDif);
    }

    public void ChooseDif(int dif)
    {
        for (int i = 0; i < numOfBtns; i++)
        {
            buttonsImages[i].sprite = buttons[i];
        }
        buttonsImages[dif - 1].sprite = pressedButtons[dif - 1];
        chosenDif = dif;
        GunChoice.Difficulty = chosenDif;
    }
}
