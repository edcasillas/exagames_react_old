using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GolemsEnums;

[RequireComponent(typeof(HealthBarController))]
public abstract class _GolemController : MonoBehaviour
{
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float walkSpeed;
	[SerializeField]
	private float maxCloseDistance;
	[SerializeField]
	private float maxChasingRange;
	[SerializeField]
	private float attackCooldownTime;
	private bool canAttack = true;
	private bool takingDamage = false;
	private bool specialAttackTriggered;

	private HealthBarController healthBarController;

	[SerializeField]
	private PlayerController player;

	private bool isDead = false;
	private bool animDeadPlayed = false;

	#region Boss Stats
	[SerializeField]
	private int maxLife;

	private int _health;
	public int Health {
		get { return _health; }
		private set {
			_health = value;
			healthBarController.UpdateBar(_health);
		}
	}

	[SerializeField]
	private float damage;
	[SerializeField]
	private float cooldownGetDamage;
	#endregion

	#region Shot / Special Ability
	[SerializeField]
	private GameObject startProjectilePosition;
	[SerializeField]
	private GameObject projectilePrefab;
	#endregion

	//Esta parte seguramente se daba poner en el script del golem y no en el padre
	#region Animations state names
	private readonly string IDLE_STATE_NAME = "Idle1";
	private readonly string WALK_STATE_NAME = "Walk";
	private readonly string DEATH_STATE_NAME = "Death1";
	private readonly string TAKE_DAMAGE_STATE_NAME = "GetHit2";
	private readonly string NORMAL_ATTACK_STATE_NAME = "Attack3";
	private readonly string FIRE_ATTACK_STATE_NAME = "Attack2";
	#endregion
	
	#region Animation Parameters
	private readonly string DEATH_TRIGGER = "Death1";
	private readonly string SPECIAL_ATTACK_TRIGGER = "Attack2";
	private readonly string ATTACK_TRIGGER = "Attack3";
	private readonly string TAKE_DAMAGE_TRIGGER = "GetHit2";
	private readonly string WALKING_BOOL = "Walking";
	#endregion

	private GolemStates lastState = GolemStates.Idle;
	private GolemStates actualState = GolemStates.Idle;

	void Awake()
    {
		healthBarController = GetComponent<HealthBarController>();
		Health = maxLife;
	}

    void Update()
    {
		if (Health <= 0)
			isDead = true;

		if(player.Health > 0 && Vector3.Distance(player.transform.position, transform.position) < maxChasingRange && (actualState == GolemStates.Idle || actualState == GolemStates.Walking))
		{
			Walk();
		} else 
		{

		}
	}

	public void SetPlayer(PlayerController _player) {
		player = _player;
	}

	private void ChangeState(GolemStates _newState) 
	{
		lastState = actualState;
		actualState = _newState;
	}

	private void TriggerSpecialAttack()//Esto lo debe de desarrolar el golem hijo de esta clase
	{

	}
	/// <summary>
	/// Used to play animations with a animator trigger
	/// </summary>
	/// <param name="_animationName"></param>
	private void PlayAnimation(string _animationName) 
	{
		animator.SetTrigger(_animationName);
	}

	/// <summary>
	/// Used to play animations with a animator boolean
	/// </summary>
	/// <param name="_animationName"></param>
	/// <param name="_makingSomething"></param>
	private void PlayAnimation(string _animationName, bool _makingSomething) 
	{
		animator.SetBool(_animationName, _makingSomething);
		Debug.Log("Lenght: " + animator.GetCurrentAnimatorStateInfo(0).length);
		Debug.Log("Normalized Time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}

	public void Walk() {
		Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		if (Vector3.Distance(playerPos, transform.position) > maxCloseDistance && (!specialAttackTriggered || !canAttack) && (actualState == GolemStates.Walking || actualState == GolemStates.Idle)) 
		{
			ChangeState(GolemStates.Walking);
			transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
			transform.position = Vector3.MoveTowards(transform.position, playerPos, walkSpeed * Time.deltaTime);
			PlayAnimation(WALK_STATE_NAME, true);
		}
	}

	public void TakeDamage(int _damage) {
		if (!takingDamage) {
			takingDamage = true;
			StartCoroutine(TakeDamageWithCoroutine(_damage));
		}
		Debug.Log("Lenght: " + animator.GetCurrentAnimatorStateInfo(0).length);
		Debug.Log("Normalized Time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}

	//private IEnumerator TakeDamageWithCoroutine(int _damage) {
	//	yield return new WaitForSeconds(cooldownGetDamage);
	//	if (!isDead) {
	//		Health -= _damage;
	//		//animator.SetTrigger(GET_HIT_TRIGGER);
	//		PlayAnimation(GET_HIT_TRIGGER);
	//	}
	//	Debug.Log("Lenght: " + animator.GetCurrentAnimatorStateInfo(0).length);
	//	Debug.Log("Normalized Time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

	//	takingDamage = false;
	//}

	private IEnumerator TakeDamageWithCoroutine(int _damage) {
		yield return new WaitForSeconds(cooldownGetDamage);
		if (!isDead) {
			Health -= _damage;
			PlayAnimation(TAKE_DAMAGE_STATE_NAME);
			while(animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE_NAME))
			{
				yield return null;
			}
		}

		takingDamage = false;
		ChangeState(GolemStates.Idle);
	}
}
