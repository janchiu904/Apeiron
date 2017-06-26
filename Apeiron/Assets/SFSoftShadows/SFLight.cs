// Super Fast Soft Lighting. Copyright 2015 Howling Moon Software, LLP

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class SFLight : MonoBehaviour, _SFCullable {
	[Tooltip("The radius of the light source. Larger lights cast softer shadows.")]
	public float _radius = 0.5f;

	[Tooltip("The brightness of the light. (Ignored when using non-linear light blending.) Allows for colors brighter than 1.0 in HDR lighting situations.")]
	public float _intensity = 1.0f;

	[Tooltip("The color of the light.")]
	public Color _color = Color.white;

	[Tooltip("The shape of the light.")]
	public Texture2D _cookieTexture;
	
	[Tooltip("Which layers cast shadows.")]
	public LayerMask _shadowLayers = ~0;

	public float radius {get {return _radius;} set {_radius = value;}}
	public float intensity {get {return _intensity;} set {_intensity = value;}}
	public Color color {get {return _color;} set {_color = value;}}
	public Texture2D cookieTexture {get {return _cookieTexture;} set {_cookieTexture = value;}}
	public LayerMask shadowLayers {get {return _shadowLayers;} set {_shadowLayers = value;}}

	private RectTransform _rt;

	public static List<SFLight> _lights = new List<SFLight>();
	private void OnEnable(){_lights.Add(this);}
	private void OnDisable(){_lights.Remove(this);}

	public Vector2 _position {
		get {
			if (!_rt) _rt = GetComponent<RectTransform>();
			return _rt.position;
		}
	}

	public Matrix4x4 _lightMatrix {
		get {
			if (!_rt) _rt = GetComponent<RectTransform> ();
			
			Vector2 r = _rt.sizeDelta / 2.0f;
			Vector2 p = Vector2.one - 2.0f*_rt.pivot;
			return Matrix4x4.TRS(new Vector3(p.x*r.x, p.y*r.y, 0.0f), Quaternion.identity, new Vector3(r.x, r.y, 1.0f));
		}
	}

	private Rect _worldBounds;
	public Rect _GetWorldBounds(){return _worldBounds;}

	public void _CacheWorldBounds(){
		var matrix = this._lightMatrix;
		_worldBounds = SFRenderer._TransformRect(_rt.localToWorldMatrix*matrix, new Rect(-1, -1, 2, 2));
	}

	private T[] AllocateArray<T>(T[] arr, int size){
		return (arr != null && arr.Length == size ? arr : new T[size]);
	}

	private Mesh _mesh;
	private Vector3[] _verts = null;
	private Vector4[] _tangents = null;
	private Vector2[] _uvs = null;
	private int[] _tris = null;

    public Mesh _GetShadowMesh(List<SFPolygon> polys, float minLightPenetration){
    	// Skip drawing the mesh if there are no shadow polygons.
    	if(polys.Count == 0) return null;
    	
		if (!_rt) _rt = GetComponent<RectTransform> ();
		
		var toLight = this.transform.worldToLocalMatrix;

		var segments = 0;
		for(int i = 0; i < polys.Count; i++){
			var poly = polys[i];
			segments += poly.verts.Length - (poly.looped ? 0 : 1);
		}

		if(!_mesh){
			_mesh = new Mesh();
			_mesh.hideFlags = HideFlags.HideAndDontSave;
		}

		_verts = AllocateArray(_verts, segments*4);
		_tangents = AllocateArray(_tangents, segments*4);
		_uvs = AllocateArray(_uvs, segments*4);
		_tris = AllocateArray(_tris, segments*6);

		var j = 0;
		for(int c = 0; c < polys.Count; c++){
			var poly = polys[c];
			var t = toLight*poly.transform.localToWorldMatrix;

			var lightPenetration = Mathf.Max(poly._lightPenetration, minLightPenetration);
			var properties = new Vector3(_radius, lightPenetration, poly._opacity);

			/* foreach path? */{
				var path = poly.verts;

				int startIndex;
				Vector2 a;

				if(poly.looped){
					startIndex = 0;
					a = Transform(t, path[path.Length - 1]);
				} else {
					startIndex = 1;
					a = Transform(t, path[0]);
				}

				for(int i = startIndex; i < path.Length; i++){
					var b = Transform(t, path[i]);

					var ab = new Vector4(a.x, a.y, b.x, b.y);
					_verts[j*4 + 0] = properties; _tangents[j*4 + 0] = ab; _uvs[j*4 + 0] = new Vector2(0.0f, 0.0f);
					_verts[j*4 + 1] = properties; _tangents[j*4 + 1] = ab; _uvs[j*4 + 1] = new Vector2(1.0f, 0.0f);
					_verts[j*4 + 2] = properties; _tangents[j*4 + 2] = ab; _uvs[j*4 + 2] = new Vector2(0.0f, 1.0f);
					_verts[j*4 + 3] = properties; _tangents[j*4 + 3] = ab; _uvs[j*4 + 3] = new Vector2(1.0f, 1.0f);

					_tris[j*6 + 0] = j*4 + 0; _tris[j*6 + 1] = j*4 + 1; _tris[j*6 + 2] = j*4 + 2;
					_tris[j*6 + 3] = j*4 + 1; _tris[j*6 + 4] = j*4 + 3; _tris[j*6 + 5] = j*4 + 2;
					
					j++;
					a = b;
				}
			}
		}

		// Need to reset the triangles before changing the vertexes.
		// Yay for false positives in overzealous error checking code. :-\
		_mesh.triangles = null;

		_mesh.vertices = _verts;
		_mesh.tangents = _tangents;
		_mesh.uv = _uvs;
		_mesh.triangles = _tris;

		return _mesh;
	}

	private void OnDestroy(){
		if(_mesh) Destroy(_mesh);
	}

	private static Vector2 Transform(Matrix4x4 m, Vector2 p){
		return new Vector2(p.x*m[0] + p.y*m[4] + m[12], p.x*m[1] + p.y*m[5] + m[13]);
	}

}
