using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Defines the basis for a functional entity, including movement and event generation in the maze.
/// 
/// Controller can interact with the Agent using <c>SetMove(Direction move)</c> to send the next motion command.
/// 
/// Subclasses implement <c>IsMoveValid()</c> to restrict the possible moves, and <c>UpdateAnimitation()</c> to change the animation state.
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Agent : MonoBehaviour
{
    new protected Rigidbody2D rigidbody;
    protected Animator animator;

    protected Maze maze
    {
        get;
        private set;
    }

    protected GameMode game
    {
        get;
        private set;
    }


    [SerializeField]
    float moveSpeed = 5;

    /// <summary>
    /// The currently active move (might be invalid / impossible).
    /// </summary>
    /// <value>The current move.</value>
    public Direction currentMove { get; private set; }

    protected Direction nextMove {
        get;
        private set;
    }

    Vector2 targetTile;

    public Vector2 currentTile
    {
        get
        {
            return rigidbody.position.ToTileCoordinates();
        }
    }

    /// <summary>
    /// Tells the agent to move in direction <paramref name="move"/> at the next possible opportunity, e.g. at tile center or an intersection.
    /// If <paramref name="Immediate"/> is <c>true</c>, the agent changes direction immediately, e.g. to turn Ms. PacMan around.
    /// </summary>
    /// <returns>The move.</returns>
    /// <param name="move">Move.</param>
    /// <param name="Immediate">If set to <c>true</c> immediate.</param>
    public void Move(Direction move, bool Immediate = false)
    {
        nextMove = move;

        if (Immediate)
            UpdateDirection();
    }

    protected virtual void UpdateAnimation()
    {
        animator.SetTrigger(currentMove.ToString());
    }

    protected abstract bool IsMoveValid(Direction move);

    protected bool IsMovePossible(Direction newMove)
    {
        return maze.IsTileWalkable(currentTile + newMove.ToVector2());
    }

    /// <summary>
    /// Immediately move to <paramref name="position"/> and reset the Moves.
    /// </summary>
    /// <returns>The reset.</returns>
    /// <param name="position">Position.</param>
    public void Reset(Vector2 position)
	{
        rigidbody.position = position;
        targetTile = currentTile;
        currentMove = Direction.NONE;
        nextMove = Direction.NONE;
	}

	void Awake()
    {
        currentMove = Direction.NONE;
        nextMove = Direction.NONE;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        game = GameObject.Find("GameMode").GetComponent<GameMode>();
        maze = GameObject.Find("Maze").GetComponent<Maze>();
    }

    void Start()
    {
        Reset(rigidbody.position);
    }

    void Update()
    {
        // Agent is exactly at tile center (works due to Vector2.MoveTowards)
        if (targetTile == rigidbody.position)
        {
            // Controller can update the next move
            SendMessage("OnTileReached", SendMessageOptions.DontRequireReceiver);

            UpdateDirection();
        }

        UpdateAnimation();
    }

    void UpdateDirection()
    {

        // Next move is valid & possible (based on maze and agent)
        if (IsMovePossible(nextMove) && IsMoveValid(nextMove))
        {
            currentMove = nextMove;
        }

        // Only execute move if it is actually possible, but remember move
        Direction actualMove = IsMovePossible(currentMove) ? currentMove : Direction.NONE;

        // Update target (in world coordinates)
        targetTile = currentTile + actualMove.ToVector2();
    }

	void LateUpdate()
	{
        // A motion has to be executed
        if (currentMove != Direction.NONE)
        {
            // Using move towards, we ensure that the tile is hit precisely at the center, eventually
            Vector2 newPosition = Vector2.MoveTowards(rigidbody.position, targetTile, Time.fixedDeltaTime * moveSpeed);

            // This will catch overlap/collision events ("Interpolate")
            rigidbody.MovePosition(newPosition);
        }
    }

    void OnTileReached()
    {
        var possibleMoves = maze.GetPossibleDirectionsOfTile(currentTile);

        // Notify at intersections, when facing a wall, or when no move is set
        if(possibleMoves.Count() > 2 || !IsMovePossible(currentMove) || currentMove == Direction.NONE){
            SendMessage("OnDecisionRequired", SendMessageOptions.DontRequireReceiver);
        }
    }
    
    public void TeleportTo(Vector2 position) {
        rigidbody.position = position;
        targetTile = currentTile + currentMove.ToVector2();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        game.OnAgentCollision(this, collision);
    }


}
