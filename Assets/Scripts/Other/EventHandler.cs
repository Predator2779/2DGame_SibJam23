using UnityEngine.Events;

public static class EventHandler
{
    public static UnityEvent OnPlayerDeath = new();
    public static UnityEvent OnEnemyKilled = new();
    public static UnityEvent<int> OnEvilLevelChange = new();
}