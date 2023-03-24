using Rover;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Tasks))]
public class GameScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        var tasks = GetComponent<Tasks>();

        tasks.OnGameFail.AddListener(HandleGameFail);
        tasks.OnGameSuccess.AddListener(HandleGameSuccess);

        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
        _text.text = "";
    }

    private void HandleGameSuccess()
    {
        _winScreen.SetActive(true);
    }

    private void HandleGameFail(GameFailReason arg0)
    {
        _loseScreen.SetActive(true);
        Debug.Log(arg0.GetType().Name);

        if (arg0 is InvalidAction)
        {
            _text.text = "неверная последовательность действий";
        }
        else if (arg0 is SampleBroken)
        {
            _text.text = "образец уничтожен";
        }
        else
        {
            var roverBreakReason = (RoverBrokenDown)arg0;
            switch (roverBreakReason.BreakDownCase)
            {
                case Rover.Rover.BreakDownCause.BatteryLow:
                    _text.text = "заряд аккумулятора достиг критического значения";
                    break;
                case Rover.Rover.BreakDownCause.BoxInvalid:
                    _text.text = "ящик для образцов был заполнен неверно";
                    break;
                case Rover.Rover.BreakDownCause.Distance:
                    _text.text = "связь с ровером потеряна";
                    break;
                case Rover.Rover.BreakDownCause.Flip:
                    _text.text = "ровер перевернулся";
                    break;
                case Rover.Rover.BreakDownCause.Health:
                    _text.text = "ровер уничтожен";
                    break;
            }
        }
    }
}
