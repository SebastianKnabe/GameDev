using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject textPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    [SerializeField] private string interactionText;
    [SerializeField] private CurrentScene currentScene;

    private bool playerInRange = false;
    private GameObject instanceOfTextObject;
    private Animator animator;

    void Start()
    {
        playerInRange = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange)
        {
            Debug.Log("Fly to Space");
            StartFlyingAnimation();
        }
    }

    private void StartFlyingAnimation()
    {
        if (camera != null)
        {
            camera.GetComponent<CameraFollow>().SetCameraTarget(this.gameObject);
            camera.GetComponent<CameraFollow>().smoothFactor = 10f;
            player.SetActive(false);

            if (currentScene == CurrentScene.TestScene)
            {
                animator.SetTrigger("LaunchTestScene");
            }
            else if (currentScene == CurrentScene.Ice)
            {
                animator.SetTrigger("LaunchIce");
            }
            else if (currentScene == CurrentScene.Earth)
            {
                animator.SetTrigger("LaunchEarth");
            }
        }
    }

    private void switchScene()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            textObject.GetComponent<TextMesh>().text = "Press [" + KeyCode.E + "] to \n" + interactionText;
            instanceOfTextObject = Instantiate(textObject, textPosition.transform.position, Quaternion.identity, textPosition.transform);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(instanceOfTextObject);
            playerInRange = false;
        }
    }
}

enum CurrentScene
{
    Earth,
    Ice,
    TestScene
}
