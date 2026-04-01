using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController :  MonoBehaviour
{
	[Header("Stat")]
	[SerializeField] private GameObject Player;
	[SerializeField] private float moveSpeed = 5f;
	
	private PlayerInput playerInput;
	private Vector3 moveVector;

	private void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		
	}
	private void Update()
	{
		ReadInput();
		Move();
	}

	private void ReadInput()
	{
		moveVector = playerInput.actions["Move"].ReadValue<Vector2>();
	}

	private void Move()
	{
		Vector3 movement = new Vector3(moveVector.x, 0, 0);
		Player.transform.position += movement * moveSpeed * Time.deltaTime;
	}
}
