using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _doSomething, _doOnExit;
    [SerializeField]
    float _requiredDelay;
    float _timePassed = 0;

    [SerializeField]
    TextMesh _timerText;
    [SerializeField]
    string _timerTextString = "Default Text.";

    private void Awake()
    {
        if (_timerText)
            _timerText.text = _timerTextString;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_requiredDelay <= 0 && !other.gameObject.GetComponent<Player>())
            return;
     
        _timePassed += Time.deltaTime;
        
        if (_timerText)
            _timerText.text = _timePassed.ToString();

        if (_timePassed >= _requiredDelay)
            DoSomething();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponent<Player>())
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

        if (other.gameObject.GetComponent<Player>())
            DoSomething();
    }

    void DoSomething()
    {
        _doSomething.Invoke();
    }
}
