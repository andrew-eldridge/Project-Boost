using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource audioSource;

    struct Audio_Clips {
        [SerializeField] AudioClip mainEngine;
        [SerializeField] AudioClip death;
        [SerializeField] AudioClip success;
    }

    struct Particle_Effects {
        [SerializeField] ParticleSystem mainEngine;
        [SerializeField] ParticleSystem death;
        [SerializeField] ParticleSystem success;
    }

    Audio_Clips audioClips;
    Particle_Effects particleEffects;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    //[SerializeField] AudioClip mainEngine;
    //[SerializeField] AudioClip deathExplosion;
    //[SerializeField] AudioClip levelComplete;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive) {
            Thrust();
            Rotate();
        }
	}

    void OnCollisionEnter(Collision collision) {
        if (state != State.Alive) {
            return;
        }
        switch (collision.gameObject.tag) {
            case "Friendly":
                {
                    break;
                }
            case "Deadly":
                {
                    state = State.Dying;
                    audioSource.Stop();
                    audioSource.PlayOneShot(audioClips.mainEngine);
                    Invoke("LoadFirstLevel", 1f);
                    break;
                }
            case "Finish":
                {
                    state = State.Transcending;
                    audioSource.Stop();
                    audioSource.PlayOneShot(levelComplete);
                    Invoke("LoadNextLevel", 1f);
                    break;
                }
        }
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
        audioSource.Stop();
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1);
        audioSource.Stop();
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.W)) {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else {
            audioSource.Stop();
        }
    }

    private void Rotate() {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            if (!Input.GetKey(KeyCode.D)) {
                transform.Rotate(Vector3.forward * rotationThisFrame);
            }
        }

        if (Input.GetKey(KeyCode.D)) {
            if (!Input.GetKey(KeyCode.A)) {
                transform.Rotate(-Vector3.forward * rotationThisFrame);
            }
        }

        rigidBody.freezeRotation = false;
    }
}
