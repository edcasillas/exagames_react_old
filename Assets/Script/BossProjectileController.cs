using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	private float projectileForce;

    // Start is called before the first frame update
    void Start()
    {
		addForce();
    }

	public void SetLocalRotation(Quaternion _newRotation) 
	{
		transform.localRotation = _newRotation;
	}

	private void addForce() 
	{
		Vector3 pos = new Vector3(0, 0, -transform.localPosition.z);
		rb.AddRelativeForce(pos * projectileForce, ForceMode.Impulse);
	}

	//TODO: Make damage to the player in a trigger or collision enter
}
