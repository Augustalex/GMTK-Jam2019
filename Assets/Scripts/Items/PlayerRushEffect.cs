using UnityEngine;

public class PlayerRushEffect : MonoBehaviour
{
    private bool _started;
    private float _time;
    private const double _length = 5;

    private void Update()
    {
        if (_started)
        {
            _time += Time.deltaTime;
            if (_time > _length)
            {
                _started = false;
                GetComponent<PlayerMovement>().Rushing = false;
            }
        }
    }


    public void Activate()
    {
        GetComponent<PlayerMovement>().Rushing = true;

        _started = true;
        _time = 0;
    }
}