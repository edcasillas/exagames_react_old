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
	private bool gettingDamage = false;
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

	#region Animations state names
	private readonly string IDLE_STATE_NAME = "Idle1";
	private readonly string WALK_STATE_NAME = "Walk";
	private readonly string DEATH_STATE_NAME = "Death1";
	private readonly string GET_HIT_STATE_NAME = "GetHit2";
	private readonly string NORMAL_ATTACK_STATE_NAME = "Attack3";
	private readonly string FIRE_ATTACK_STATE_NAME = "Attack2";
	#endregion

	#region Animation Parameters
	private readonly string DEATH_TRIGGER = "Death1";
	private readonly string SPECIAL_ATTACK_TRIGGER = "Attack2";
	private readonly string ATTACK_TRIGGER = "Attack3";
	private readonly string GET_HIT_TRIGGER = "GetHit2";
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
	}

	public void SetPlayer(PlayerController _player) {
		player = _player;
	}

	private void TriggerSpecialAttack()
	{

	}
}
