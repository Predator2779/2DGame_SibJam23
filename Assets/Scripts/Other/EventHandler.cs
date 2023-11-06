using UnityEngine.Events;

public static class EventHandler
{
    public static UnityEvent OnPlayerDeath = new();
    public static UnityEvent OnEnemyKilled = new();
    public static UnityEvent<string> OnItemPick = new();
    public static UnityEvent<string> OnItemPut = new();
    public static UnityEvent<string> OnItemUse = new();
    public static UnityEvent<string> OnItemSetUsable = new();
    public static UnityEvent<string> OnPlayerKilled = new();
    public static UnityEvent OnTearCounterEnable = new();
    public static UnityEvent OnBossKilled = new();
}