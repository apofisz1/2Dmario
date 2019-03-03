using UnityEngine;

public class InputState : MonoBehaviour {
	public bool IsActionButtonPressed;	// éppen le van-e nyomva bármilyen gomb
	public bool IsSpacePressed;
	public bool IsStanding;				// a földön áll-e a játékos
	public float StandingThreshold;		// az a magasság (Y érték), ami alatt a játékos a "földön áll"
	public Vector2 AbsoluteVelocity;	// a játékos sebességének abszolút értéke (X és Y irányban)

	private Rigidbody2D _rigidbody2D;	// 2D merevtest

	private void Awake() {
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {	// lövés
			IsSpacePressed = true;
			IsActionButtonPressed = false;
		} else {	
			IsActionButtonPressed = Input.anyKeyDown;	// ugrás
			IsSpacePressed = false;
		}
	}

	private void FixedUpdate() {
		// a sebességvektornak komponenseinek abszolút értékeiből képezzük az abszolút sebességet
		AbsoluteVelocity = new Vector2(Mathf.Abs(_rigidbody2D.velocity.x), Mathf.Abs(_rigidbody2D.velocity.y));
		// a játékos a "földön áll", ha a küszöbértéknél alacsonyabb az Y koordinátája
		IsStanding = AbsoluteVelocity.y < StandingThreshold;
	}
}
