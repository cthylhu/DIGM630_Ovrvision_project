@script ExecuteInEditMode

var clipTexture : Texture;

function OnRenderObject() {
	Shader.SetGlobalTexture("_ClipTexture", clipTexture);

	var clipNear = -transform.localScale.x*0.5;
	var scaleOffset = Matrix4x4.TRS(Vector3(0,0,0), Quaternion.identity, Vector3(1,1,1));
	
	Shader.SetGlobalMatrix("_ClipTextureMatrix_Portal_2", renderer.worldToLocalMatrix*scaleOffset);
}
