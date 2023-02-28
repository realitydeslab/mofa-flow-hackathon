using UnityEngine;
using System.Collections;

/// http://www.mikedoesweb.com/2012/camera-shake-in-unity/

public class ObjectShake : MonoBehaviour
{

	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay = 0.002f;
	public float shake_intensity = .3f;

	private float temp_shake_intensity = 0;



	[SerializeField]
	float _rotationFixer = 0.2f;

	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 40, 80, 20), "Shake"))
		{
			Shake();
		}
	}

	void Update()
	{
		if (temp_shake_intensity > 0)
		{
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;

			transform.rotation = new Quaternion(
				originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * _rotationFixer,
				originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * _rotationFixer,
				originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * _rotationFixer,
				originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * _rotationFixer);

			temp_shake_intensity -= shake_decay;
		}
	}

	void Shake()
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;

	}

	public void StartCorutine()
    {
		StartCoroutine(StartShakeForThreeTimes());
	}


	IEnumerator StartShakeForThreeTimes()
    {
		Shake();
		yield return new WaitForSeconds(1f);
        //Shake();
        //yield return new WaitForSeconds(1f);
        //Shake();
    }
}