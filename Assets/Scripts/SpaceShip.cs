using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject textPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject saveFileHandler;
    [SerializeField] private string interactionText;
    [SerializeField] private CurrentScene currentScene;
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryItem questItem;
    [SerializeField] private GameSettings settings;

    private bool playerInRange = false;
    private GameObject instanceOfTextObject;
    private Animator animator;
    private int fuel;

    void Start()
    {
        playerInRange = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange)
        {
            if (currentScene == CurrentScene.Earth)
            {
                if (fuel >= 4)
                {
                    Debug.Log("Fly to Space");
                    StartFlyingAnimation();
                }
            }
            else
            {
                Debug.Log("Fly to Space");
                StartFlyingAnimation();
            }

            saveFileHandler.GetComponent<SaveFileHandler>().Save();
        }
    }

    private void StartFlyingAnimation()
    {
        settings.lastPlayerScene = SceneManager.GetActiveScene().buildIndex;
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
        updateFuel();
        if (other.tag == "Player")
        {
            if (currentScene == CurrentScene.Earth && fuel < 4f)
            {
                textObject.GetComponent<TextMesh>().text = "You cant fly away now. You can only \nstart your ship if it is completely filled up.\n Progress: [" + fuel + "/4]";
                instanceOfTextObject = Instantiate(textObject, textPosition.transform.position, Quaternion.identity, textPosition.transform);
                playerInRange = true;
            }
            else if (fuel >= 4f)
            {
                textObject.GetComponent<TextMesh>().text = "Press [" + KeyCode.E + "] to \n" + interactionText;
                instanceOfTextObject = Instantiate(textObject, textPosition.transform.position, Quaternion.identity, textPosition.transform);
                playerInRange = true;
            }
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

    public void updateFuel()
    {
        fuel = inventory.ItemContainer.GetTotalQuantity(questItem);
    }
}

enum CurrentScene
{
    Earth,
    Ice,
    TestScene
}
