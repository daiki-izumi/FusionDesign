using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{
	void Update()
	{
		transform.Translate(0, -0.5f, 0);
		float y_pos = 1737.0f;
		if (transform.position.y <= -1741)
		{
			transform.position = new Vector3(550.0f, y_pos, 0);
		}
	}
}