using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float fallSpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.up * fallSpeed * Time.deltaTime);
    }
}
