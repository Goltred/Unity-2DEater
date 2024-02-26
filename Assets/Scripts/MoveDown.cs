using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float fallSpeed = 4f;
    
    void Update()
    {
        transform.Translate(-transform.up * fallSpeed * Time.deltaTime);
    }
}
