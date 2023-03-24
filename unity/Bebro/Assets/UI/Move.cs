using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public Image _moveImage;
    [SerializeField] private Image _target;
    [SerializeField] private float _trueDistance;
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;

    public UnityEvent OnWin;

    private float _min;
    private float _max;

    private float _targetX = 405;
    private bool _th = false;
    // Start is called before the first frame update
    void Start()
    {
        _min = _leftBorder.transform.localPosition.x;
        _max = _rightBorder.transform.localPosition.x;
        _targetX = _max;
        _target.transform.localPosition = new Vector2(Random.Range(_min, _max), _target.transform.localPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        _th = Mathf.Abs(_target.transform.localPosition.x - _moveImage.transform.localPosition.x) / (_max - _min) < _trueDistance;

        if (_moveImage.transform.localPosition.x == _max)
        {
            _targetX = _min;
        }
        else if (_moveImage.transform.localPosition.x == _min)
        {
            _targetX = _max;
        }


        _moveImage.transform.localPosition = new Vector2(Mathf.MoveTowards(_moveImage.transform.localPosition.x, _targetX, (_max - _min) / 2 *Time.deltaTime), _moveImage.transform.localPosition.y);
        
    }

    public void btn()
    {
        if (_th == true)
            OnWin?.Invoke();
        else if (_th == false)
        {
            Debug.Log("net");
            _target.transform.localPosition = new Vector2(Random.Range(_min, _max), _target.transform.localPosition.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("12332432332");
        if(collision.tag == "TriggerImg")
        {
            _th = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TriggerImg")
        {
            _th = false;
        }
    }
}
