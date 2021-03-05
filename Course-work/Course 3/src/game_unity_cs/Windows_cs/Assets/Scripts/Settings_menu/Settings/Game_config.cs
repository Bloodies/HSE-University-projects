using UnityEngine;

public class GameConfig
{

	// VIDEO \\
	public int displayMode = 0;
	public int targetDisplay = 0;
	public int resulationId = 0;
	public int graphicsQuality = 0;
	public int antialiasing = 0;
	public int vsync = 0;
	public bool toggleTest = false;

	// AUDIO \\
	public float masterVolume = 1f;
	public float musicVolume = 0.5f;
	public float effectsVolume = 0.5f;
	public float voiceVolume = 0.5f;
	public float micVolume = 0.5f;
	public bool soundBackground = true;

	// GAME \\
	public float horizontalSensitivy = 1f;
	public float verticalSensitivy = 1f;
	public int difficuly = 0;
	public int language = 0;
	public bool tips = true;

	// INPUT \\
	public string forward = "W";
	public string back = "S";
	public string left = "A";
	public string right = "D";
	public string crouch = "LeftControl";
	public string jump = "Space";
}