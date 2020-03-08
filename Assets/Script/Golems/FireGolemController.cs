using System.Collections;
using GolemsEnums;
using UnityEngine;

namespace Golems
{
	public class FireGolemController : GolemController
	{
		#region Shot / Special Ability

		[SerializeField] private GameObject startProjectilePosition;
		[SerializeField] private GameObject projectilePrefab;

		#endregion

		public override void TriggerSpecialAttack()
		{
			if (IsInoffensiveGolem) return;

			if (!isDead && canAttack && !takingDamage)
			{
				isSpecialAttackTriggered = true;
				ChangeState(GolemStates.SpecialAttacking);
				PlayAnimationWithTrigger(SPECIAL_ATTACK_TRIGGER);
				StartCoroutine(SpawnProjectilWithDelay(1.5f));
				StartCoroutine(CooldownAttack());
				StartCoroutine(ChangeStateWhenAnimationIsOver(SPECIAL_ATTACK_STATE_NAME, GolemStates.Idle));
			}
		}

		private IEnumerator SpawnProjectilWithDelay(float _timeToDelay)
		{
			yield return new WaitForSeconds(_timeToDelay);
			if (takingDamage || !canAttack)
			{
				yield break;
			}

			SpawnProjectil();
		}

		private void SpawnProjectil()
		{
			Instantiate(projectilePrefab, startProjectilePosition.transform.position,
				startProjectilePosition.transform.rotation);
			isSpecialAttackTriggered = false;
			canAttack = false;
		}
	}
}