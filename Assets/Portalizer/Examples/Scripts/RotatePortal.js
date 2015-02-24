public var SpeedRotation = 40;

function Update () {
	transform.Rotate(Vector3.up * Time.deltaTime * SpeedRotation);
}