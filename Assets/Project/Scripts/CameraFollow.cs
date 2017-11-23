using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// x irányban mennyit mozdulhat el a játékos karakter a CameraRoot objektumtól
	public float zMargin = 1f;		// z irányban mennyit mozdulhat el a játékos karakter a CameraRoot objektumtól
	public float xSmooth = 8f;		// x irányban milyen "simítással" követi a kamera a játékost
	public float zSmooth = 8f;		// z irányban milyen "simítással" követi a kamera a játékost
	public Vector3 maxXAndZ;		// maximum x és z, ahol a kamera lehet
	public Vector3 minXAndZ;		// minimum x és z, ahol a kamera lehet
	
	
	private Transform player;		// játékos transform komponensére referencia
	
	
	void Awake ()
	{
		// játékos referencia beállítása tag alapján
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	
	bool CheckXMargin()
	{
		// igazzal tér vissza, ha a játékos és a CameraRoot x pozíciójának különbsége nagyobb, mint az xMargin
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}
	
	
	bool CheckZMargin()
	{
		// igazzal tér vissza, ha a játékos és a CameraRoot z pozíciójának különbsége nagyobb, mint a zMargin
		return Mathf.Abs(transform.position.z - player.position.z) > zMargin;
	}
	
	
	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// a CameraRoot pozíciója
		float targetX = transform.position.x;
		float targetZ = transform.position.z;
		
		// Ha x irányban mozgatni kell...
		if(CheckXMargin())
			// ... interpolálunk a CameraRoot mostani pozíciójáról a játékos pozíciójára
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
		
		// Ugyanaz, csak z irányban
		if(CheckZMargin())
			targetZ = Mathf.Lerp(transform.position.z, player.position.z, zSmooth * Time.deltaTime);
		
		// a pozíciót a min és max értékek közé "vágjuk"
		targetX = Mathf.Clamp(targetX, minXAndZ.x, maxXAndZ.x);
		targetZ = Mathf.Clamp(targetZ, minXAndZ.z, maxXAndZ.z);
		
		// beállítjuk az új pozíciót
		transform.position = new Vector3(targetX, transform.position.y, targetZ);
	}
}
