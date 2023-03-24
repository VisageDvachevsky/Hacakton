using Rover;
using UnityEngine;

public class RadarMinigame : MonoBehaviour
{
    [SerializeField] private GameObject _minigamePanel;
    [SerializeField] private Transform _radarRotation;
    [SerializeField] private Move _minigame;
    public bool IsWon { get; private set; }

    private void Start()
    {
        _minigame.OnWin.AddListener(HandleWin);
    }

    private void Update()
    {
        if (IsWon)
            _radarRotation.rotation = Quaternion.Slerp(_radarRotation.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 0.5f);
    }

    private void HandleWin()
    {
        IsWon = true;
        Tasks.SetRadarFixed();
        CloseMinigame();
    }

    public void CloseMinigame()
    {
        _minigamePanel.SetActive(false);
    }

    public void HandleButton()
    {
        if (_minigamePanel.activeInHierarchy)
        {
            _minigame.btn();
        }
    }

    public void OpenMinigame()
    {
        if (!IsWon)
        {
            _minigamePanel.SetActive(true);
        }
    }
}
