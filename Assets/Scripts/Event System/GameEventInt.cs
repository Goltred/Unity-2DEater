using UnityEngine;

// Better to have a specific event with the component we plan to use right away
// to prevent multiple GetComponent calls in different places
[CreateAssetMenu(menuName = "Events/Game Event with Int")]
public class GameEventInt: GameEvent<int>
{
}