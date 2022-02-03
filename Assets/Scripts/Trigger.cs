using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _doSomething, _doOnExit;
    [SerializeField]
    float _requiredDelay;
    float _timePassed = 0;


    [SerializeField]
    Text _timerText;
    [SerializeField]
    string _timerTextString = "Default Text.", _prefix;

    private void Awake()
    {
        if (_timerText)
            _timerText.text = _timerTextString;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_requiredDelay <= 0 && !other.gameObject.GetComponent<PlayerID>())
            return;
     
        _timePassed += .075f*Time.deltaTime;
        
        if (_timerText)
            _timerText.text = $"{_prefix}\n{Mathf.Abs(_timePassed - _requiredDelay).ToString("n2")}";

        if (_timePassed >= _requiredDelay)
            DoSomething();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerID>())
            return;

        if (_requiredDelay > 0)
            _timePassed = 0;

        if (_timerText)
            _timerText.text = _timerTextString;

        if (_doOnExit != null)
            _doOnExit.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_requiredDelay > 0)
            return;

        if (other.gameObject.GetComponent<PlayerID>())
            DoSomething();
    }

    void DoSomething()
    {
        _doSomething.Invoke();
    }
}
