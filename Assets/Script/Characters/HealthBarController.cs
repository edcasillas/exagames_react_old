using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
	public Image Health_FillImage;

	public float ColorTransitionSpeed = 1f;

	[System.Serializable]
	public class TweenColor {
		[Range(0f,1f)]
		public float ValueMoreThan;
		public Color Color;
	}

	public TweenColor[] HealthColors;

	private bool initialized = false;

	private int initHealth;
	private float currentFillAmountValue;

	public void UpdateBar(int newHealth) {
		if (!initialized) {
			initHealth = newHealth;
			initialized = true;
		}

		currentFillAmountValue = (float)newHealth / (float)initHealth;
	}

	private void Update()
    {
		if (!Health_FillImage)
			return;

		Health_FillImage.fillAmount = Mathf.Lerp(Health_FillImage.fillAmount, currentFillAmountValue, Time.deltaTime * ColorTransitionSpeed);

		if (!HealthColors.Any())
			return;

		var newColor = HealthColors.Where(c => c.ValueMoreThan < Health_FillImage.fillAmount).OrderByDescending(o => o.ValueMoreThan).FirstOrDefault();

		if (newColor == null)
			return;

		Health_FillImage.color = Color.Lerp(Health_FillImage.color, newColor.Color, Time.deltaTime * ColorTransitionSpeed);
	}
}
