using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TookItem : MonoBehaviour {
    public Image _sprite;

	// Update is called once per frame
	void Update () {
        var color = _sprite.color;

        color.a -= Time.deltaTime / 7f;
        color.a = Mathf.Clamp(color.a, 0, 1);

        _sprite.color = color;

        if (_sprite.color.a <= 0)
            Destroy(gameObject);
    }
}
