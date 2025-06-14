using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector3 respawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetRespawnPoint(Vector3 point)
    {
        respawnPoint = point;
    }

    public Vector3 GetRespawnPoint()
    {
        return respawnPoint;
    }
}

