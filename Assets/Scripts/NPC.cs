using UnityEngine.AI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    public event Action<NPC> OnFollowingStateChange;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private NPCState idlingState, roamingState, followingState;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject leader;
    [SerializeField] private Transform leaderTail;
    [SerializeField] private NPCState currentState;
    [SerializeField] private bool followed;

    private Animator animator;
    private Quaternion rotation;  
    private Vector3 startPosition;    
    private float speed;

    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }

    public NavMeshAgent Agent => agent;
    public float Speed { get => speed; set => speed = value; }
    public bool IsRoaming => currentState == roamingState;
     public bool IsFollowing => currentState == followingState;
    public GameObject NPCLeader { get => leader; set => this.leader = value; }
    public Quaternion Rotation { get => rotation; set => rotation = value; }
    public Rigidbody Rb { get => rb; set => this.rb = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public NPCState IdlingState { get => idlingState; set => idlingState = value; }
    public bool Followed { get => followed; set => followed = value; }

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        this.leader = null;       
        currentState = roamingState;
        currentState.OnStateStart(this);
        followed = false;
        Leader.OnAddedNPC += FollowLeader;
    }

    void OnDisable() 
    {
        Leader.OnAddedNPC -= FollowLeader;
    }

    public void FollowLeader(GameObject newLeader, Transform newLeaderTail)
    {
        if (followed)
        {
            leader = newLeader;
            leaderTail = newLeaderTail;
            currentState = followingState;
            currentState.OnStateStart(this);
            OnFollowingStateChange?.Invoke(this);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }

    private void CheckState()
    {  
        if (currentState == roamingState)
            currentState.Execute(this, null);
        if (currentState == followingState)
            currentState.Execute(this, leaderTail);
    }
}
