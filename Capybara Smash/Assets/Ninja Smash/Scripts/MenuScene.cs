using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace NinjaSmash {

public class MenuScene : MonoBehaviour {

	// Boolean to check Music and Sound On/Off states
	public static bool soundOn = true;
	public static bool musicOn = true;

	// Menu scene music clip
	public AudioClip musicClip;
	public static AudioSource musicSource;

	public GameObject MusicButton;
	public GameObject SoundButton;

	public Sprite[] SoundIcons;

	public Ninja ninja1, ninja2;

	public Sprite[] ninaImages;


	// Variable to store current Game Mode
	public static int modeNum = 0;

	// Ninja Sprites
	// private GameObject ninja1, ninja2;

	private static int musicOnIcon = 0;
	private static int musicOffIcon = 1;
	private static int soundOnIcon = 2;
	private static int soundOffIcon = 3;

	// Boolean for paused and game over states
	public static bool paused;
	public static bool isGameOver;

	// Variable to store current score in each game mode
	public static int scoreNum;
	

	// Use this for initialization
	void Start () {

		// Get ninjas sprites from scene
		// ninja1 = GameObject.Find("HoleL");
		// ninja2 = GameObject.Find("HoleR");

		// Loop ninjas show/hide
		StartCoroutine(loopNinjas());

		// Initialize music
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.playOnAwake = false;
		musicSource.rolloffMode = AudioRolloffMode.Linear;
		musicSource.loop = true;
		musicSource.clip = musicClip;

		Time.timeScale = 1; // Use normal game speed. If previously paused, it would be 0


		int mOn = PlayerPrefs.GetInt("mOn", 1);
		int sOn = PlayerPrefs.GetInt("sOn", 1);
		
		if(mOn == 1) {
			musicOn = true;
		}else {
			musicOn = false;
		}

		if(sOn == 1) {
			soundOn = true;
		}else {
			soundOn = false;
		}

		musicSource.volume = 2;


		if(musicOn){
			musicSource.Play(); // Play music if sound is on
			MusicButton.GetComponent<Image>().sprite = SoundIcons[musicOnIcon];
		}else {
			MusicButton.GetComponent<Image>().sprite = SoundIcons[musicOffIcon];
		}

		if(soundOn) {
			SoundButton.GetComponent<Image>().sprite = SoundIcons[soundOnIcon];
		}else {
			SoundButton.GetComponent<Image>().sprite = SoundIcons[soundOffIcon];
		}

	}
	
	// Update is called once per frame


	

	// Method to loop ninjas
	IEnumerator loopNinjas(){
    int a = 1;
    int randomNinjaType;
    while(true){
        yield return new WaitForSeconds(Random.Range(1, 4));
        if(a == 1){
            a = 0;
            randomNinjaType = Random.Range(0, ninaImages.Length); // Исправлено
            Ninja ninjaScript = ninja1.GetComponent<Ninja>();
            Image ninjaMain = ninjaScript.gameObject.GetComponentInChildren<Image>();
            ninjaMain.sprite = ninaImages[randomNinjaType];
            ninjaScript.pop(1, false);
        }else{
            a = 1;
            randomNinjaType = Random.Range(0, ninaImages.Length); // Исправлено
            Ninja ninjaScript = ninja2.GetComponent<Ninja>();
            Image ninjaMain = ninjaScript.gameObject.GetComponentInChildren<Image>();
            ninjaMain.sprite = ninaImages[randomNinjaType];
            ninjaScript.pop(1, false);
        }
    }
}


	public void OnPlayButtonClick() {
		// SceneManager.LoadScene(Levels.GameScene);
		SceneManager.LoadScene("ModeScene");
	}

	

	public void OnSoundButtonClick() {
		if(soundOn){
			SoundButton.GetComponent<Image>().sprite = SoundIcons[soundOffIcon];
			soundOn = false;
			PlayerPrefs.SetInt("sOn", 0);
		}else{
			SoundButton.GetComponent<Image>().sprite = SoundIcons[soundOnIcon];
			soundOn = true;
			PlayerPrefs.SetInt("sOn", 1);
		}
	}

	public void OnMusicButtonClick() {
		if(musicOn){
			MusicButton.GetComponent<Image>().sprite = SoundIcons[musicOffIcon];
			musicOn = false;
			musicSource.Stop();
			PlayerPrefs.SetInt("mOn", 0);
		}else{
			MusicButton.GetComponent<Image>().sprite = SoundIcons[musicOnIcon];
			musicOn = true;
			musicSource.Play();
			PlayerPrefs.SetInt("mOn", 1);
		}
	}

}
}






