using UnityEngine;


[RequireComponent(typeof(EntityMovement), typeof(Animator))]
public class EnemyAIController : MonoBehaviour
{
    public EntityMovement MovementComponent { get; private set; }
    public Animator Animator { get; private set;}
    public ObstacleDetector ObstaclesDetector { get; private set; }
    public PlayerDetector PlayerDetector { get; private set; }


    void Start()
    {
        MovementComponent = GetComponent<EntityMovement>();
        Animator = GetComponent<Animator>();
        ObstaclesDetector = GetComponent<ObstacleDetector>();
        PlayerDetector = GetComponent<PlayerDetector>();
    }

    void Update()
    {
        
    }
}
