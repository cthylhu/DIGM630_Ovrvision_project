function Update() {
	if (Time.time % 2 < 0.05) {
		if (!transform.rigidbody.isKinematic) {
			transform.position = Vector3(-3, 5, -1.78);
			transform.rotation = Quaternion.identity;
			transform.rigidbody.velocity = Vector3.zero;
		}
	} 
}