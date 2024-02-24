using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float FallSpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.up * FallSpeed * Time.deltaTime);
    }
}
