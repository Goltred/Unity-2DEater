using UnityEngine;
using UnityEngine.Playables;

public class PlayableDirectorTimedRestart : MonoBehaviour
{
    public PlayableDirector director;
    [Tooltip("Amount of time to wait until the director is played again")]
    public float restartTime = 60;

    private float _timer;

    void Start()
    {
        // Remember to set the initial _timer value so that we don't end up restarting the playaable twice
        _timer = restartTime;
    }
    void Update()
    {
        if (director.state == PlayState.Paused)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                director.Play();
                _timer = restartTime;
            }
        }
    }
}
