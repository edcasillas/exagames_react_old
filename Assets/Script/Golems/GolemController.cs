using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GolemsEnums;

[RequireComponent(typeof(HealthBarController))]
public abstract class _GolemController : MonoBehaviour
{
	[SerializeField]
	protected Animator animator;
	[SerializeField]
	protected float walkSpeed;
	[SerializeField]
	protected float maxCloseDistance;
	[SerializeField]
	protected float maxChasingRange;
	[SerializeField]
	protected float attackCooldownTime;
	protected bool canAttack = true;
	protected bool takingDamage = false;
	protected bool specialAttackTriggered;

	protected bool isChasing = false;
	protected bool IsChasing 
	{
		get 
		{
			return Vector3.Distance(player.transform.position, transform.position) < maxChasingRange;
		}
	}

	protected HealthBarController healthBarController;

	[SerializeField]
	protected PlayerController player;

	protected bool isDead = false;
	protected bool animDeadPlayed = false;
	
	//[SerializeField]
	//private Rigidbody rb;
	[SerializeField]
	protected BoxCollider collision;

	#region Boss Stats
	[SerializeField]
	protected int maxLife;

	protected int _health;
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

	//Esta parte seguramente se daba poner en el script del golem y no en el padre
	#region Animations state names
	protected readonly string IDLE_STATE_NAME = "Idle1";
	protected readonly string WALK_STATE_NAME = "Walk";
	protected readonly string DEATH_STATE_NAME = "Death1";
	protected readonly string TAKE_DAMAGE_STATE_NAME = "GetHit2";
	protected readonly string NORMAL_ATTACK_STATE_NAME = "Attack3";
	protected readonly string SPECIAL_ATTACK_STATE_NAME = "Attack2";
	#endregion

	#region Animation Parameters
	protected readonly string DEATH_TRIGGER = "Death1";
	protected readonly string SPECIAL_ATTACK_TRIGGER = "Attack2";
	protected readonly string ATTACK_TRIGGER = "Attack3";
	protected readonly string TAKE_DAMAGE_TRIGGER = "GetHit2";
	protected readonly string WALKING_BOOL = "Walking";
	#endregion

	[SerializeField]
	protected GolemStates lastState = GolemStates.Idle;
	[SerializeField]
	protected GolemStates actualState = GolemStates.Idle;

	protected void Awake()
    {
		healthBarController = GetComponent<HealthBarController>();
		Health = maxLife;
	}

	//private

    protected void Update()
    {
		if (Health <= 0)
			isDead = true;

		if (!isDead) {
			if (Vector3.Distance(player.transform.position, transform.position) < maxCloseDistance && canAttack && (actualState == GolemStates.Idle || actualState == GolemStates.Walking)) {
				if (specialAttackTriggered && canAttack) 
				{
					TriggerSpecialAttack();
				} else if(canAttack)
				{
					if (Debug.isDebugBuild) Debug.Log("Attack");
					Attack();
				}
			} else if (player.Health > 0 && IsChasing && Vector3.Distance(player.transform.position, transform.position) > maxCloseDistance && (!specialAttackTriggered || !canAttack) && (actualState == GolemStates.Idle || actualState == GolemStates.Walking)) 
			{
				Walk();
			}
		} else 
		{
			if(!animDeadPlayed) 
			{
				Die();
			}
		}
	}

	//private bool IsChasing() 
	//{
	//	return Vector3.Distance(player.transform.position, transform.position) < maxChasingRange;
	//}

	public void SetPlayer(PlayerController _player) {
		player = _player;
	}

	protected void ChangeState(GolemStates _newState) 
	{
		lastState = actualState;
		actualState = _newState;
	}

	public abstract void TriggerSpecialAttack();//Esto lo debe de desarrolar el golem hijo de esta clase

	/// <summary>
	/// Used to play animations with a animator trigger
	/// </summary>
	/// <param name="_triggerName"></param>
	protected void PlayAnimationWithTrigger(string _triggerName) 
	{
		animator.SetTrigger(_triggerName);

		if(_triggerName == SPECIAL_ATTACK_TRIGGER) 
		{
			SetAnimationBool(WALKING_BOOL, false);
		}else if (_triggerName == ATTACK_TRIGGER) 
		{
			SetAnimationBool(WALKING_BOOL, false);
		}else if(_triggerName == DEATH_TRIGGER) 
		{
			SetAnimationBool(WALKING_BOOL, false);
		}
	}

	/// <summary>
	/// Used to play animations with a animator boolean
	/// </summary>
	/// <param name="_boolName"></param>
	/// <param name="_makingSomething"></param>
	protected void SetAnimationBool(string _boolName, bool _makingSomething) 
	{
		animator.SetBool(_boolName, _makingSomething);
	}

	public void Walk() {
		Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		if(actualState != GolemStates.Walking) ChangeState(GolemStates.Walking);
		transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
		transform.position = Vector3.MoveTowards(transform.position, playerPos, walkSpeed * Time.deltaTime);
		SetAnimationBool(WALKING_BOOL, true);
	}

	public void TakeDamage(int _damage) {
		if (!takingDamage) {
			takingDamage = true;
			StartCoroutine(TakeDamageWithCoroutine(_damage));
		}
		Debug.Log("Lenght: " + animator.GetCurrentAnimatorStateInfo(0).length);
		Debug.Log("Normalized Time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}

	protected IEnumerator TakeDamageWithCoroutine(int _damage) {
		yield return new WaitForSeconds(cooldownGetDamage);
		if (!isDead) {
			Health -= _damage;
			PlayAnimationWithTrigger(TAKE_DAMAGE_TRIGGER);
			while(animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE_NAME))
			{
				yield return null;
			}
		}

		takingDamage = false;
		ChangeState(GolemStates.Idle);
	}

	protected void Attack() {
		if (!takingDamage) 
		{
			canAttack = false;
			ChangeState(GolemStates.Attacking);
			PlayAnimationWithTrigger(ATTACK_TRIGGER);
			StartCoroutine(CooldownAttack());
			StartCoroutine(ChangeStateWhenAnimationIsOver(NORMAL_ATTACK_STATE_NAME, GolemStates.Idle));
		}
	}

	protected IEnumerator ChangeStateWhenAnimationIsOver(string _actualAnimationName, GolemStates _stateToChange) 
	{
		while (!animator.GetCurrentAnimatorStateInfo(0).IsName(_actualAnimationName)) 
		{
			yield return null;
			//Debug.Log("Starting selected animation state");
		}
		//Debug.Log("Checking animation name: " + _actualAnimationName);
		while(animator.GetCurrentAnimatorStateInfo(0).IsName(_actualAnimationName)) 
		{
			yield return null;
		}
		ChangeState(_stateToChange);
	}

	protected IEnumerator CooldownAttack() {
		yield return new WaitForSeconds(attackCooldownTime);
		canAttack = true;
	}

	protected void Die() 
	{
		collision.enabled = false;
		PlayAnimationWithTrigger(DEATH_TRIGGER);
	}
}
