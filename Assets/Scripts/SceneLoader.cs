using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] public int currentLevelTargetCount;
    [SerializeField] AudioClip success;
    public GameObject[] boxes;
    public GameObject[] targets;
    public GameObject walls;
    public GameObject detectors;
    AudioSource audioSource;
    ArrayList indexOfDisabledWalls = new ArrayList();
    private CanvasMemory canvasMemory;
    [SerializeField] GameObject canvasMemoryPrefab;
    [SerializeField] public GameObject canvas;

    [SerializeField] private float levelLoadDelay = 1.0f;
    [SerializeField] private float wallDisappearanceDelay = 0.05f;
    [SerializeField] private float boxLandingDelay = 1.0f;
    [SerializeField] private float boxLandingTime = 1.0f;
    [SerializeField] private float levelEndDealy = 1.0f;
    [SerializeField] private float boxXiRuTime = 0.5f;
    [SerializeField] private float boxYOffset = 15f;

    public bool gameEnded = false;

    private void Awake()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        targets = GameObject.FindGameObjectsWithTag("Target");
        audioSource = GetComponent<AudioSource>();
        walls = GameObject.Find("Walls");
        detectors = GameObject.Find("InputDetector");

        //For debug only, if you don't enter the level from the main screen, instantiate the clone of canvas memory
        if (GameObject.Find("CanvasMemory") == null)
        {
            Instantiate(canvasMemoryPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            canvasMemory = GameObject.Find("CanvasMemory(Clone)").GetComponent<CanvasMemory>();
        }
        else
        {
            canvasMemory = GameObject.Find("CanvasMemory").GetComponent<CanvasMemory>();
        }
        

        if (canvasMemory.isCanvasEnabled)
        {
            canvas.SetActive(true);
            canvas.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UserLevelSwitch();
        if (CheckBoxNotMoving())
        {
            if (CheckLevelClear())
            {
                if (!gameEnded)
                {
                    gameEnded = true;
                    audioSource.PlayOneShot(success);
                    //Invoke("LoadNextScene", 1f);
                    OnLevelCompleted();
                }
                //LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == SceneManager.sceneCountInBuildSettings - 1) return;
        SceneManager.LoadScene(currentLevel + 1);
    }

    private void LoadFormerScene()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 0) return;
        SceneManager.LoadScene(currentLevel - 1);
    }

    private bool CheckLevelClear()
    {
        int goalCount = 0;
        foreach (GameObject target in targets)
        {
            foreach (GameObject box in boxes)
            {
                if ((Mathf.Abs(target.transform.localPosition.x - box.transform.localPosition.x) <= Mathf.Epsilon) &&
                    (Mathf.Abs(target.transform.localPosition.z - box.transform.localPosition.z) <= Mathf.Epsilon))
                {
                    goalCount++;
                    break;
                }
            }
        }
        return (goalCount == currentLevelTargetCount);
    }

    private bool CheckBoxNotMoving()
    {
        foreach (GameObject box in boxes)
        {
            if (Mathf.Abs(box.GetComponent<BoxMovement>().moveProgress - 1) > Mathf.Epsilon)
            {
                return false;
            }
        }
        return true;
    }

    private void UserLevelSwitch()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            LoadFormerScene();
        }
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);

        //Disable input listener
        detectors.SetActive(false);

        //Move boxes above
        foreach (GameObject box in boxes)
        {
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y + boxYOffset, box.transform.position.z);
        }

        //Get Disabled walls' indexes
        for (int i = 0; i < walls.transform.childCount; i++)
        {
            if (!walls.transform.GetChild(i).gameObject.activeSelf)
            {
                indexOfDisabledWalls.Add(i);
            }
        }

        //Enable those walls
        foreach (int i in indexOfDisabledWalls)
        {
            walls.transform.GetChild(i).gameObject.SetActive(true);
        }

        //Disable those walls
        StartCoroutine(DisableWall());

        //Landing all boxes
        foreach (GameObject box in boxes)
        {
            StartCoroutine(LandingBox(box, boxLandingTime));
        }

    }

    private void OnLevelCompleted()
    {
        Debug.Log("LEVEL COMPLETE");

        //Disable input listener
        detectors.SetActive(false);

        //foreach (GameObject box in boxes)
        //{
        //    box.GetComponent<Animator>().SetTrigger("StartXiRu");
        //}

        StartCoroutine(EndLevel());

        //Enable all walls
        StartCoroutine(EnableWall());

    }

    private IEnumerator DisableWall()
    {

        yield return new WaitForSeconds(levelLoadDelay);

        foreach (int i in indexOfDisabledWalls)
        {
            walls.transform.GetChild(i).gameObject.SetActive(false);
            yield return new WaitForSeconds(wallDisappearanceDelay);
        }
    }

    private IEnumerator EnableWall()
    {
        float timeBeforeWallBeganToAppear = levelEndDealy + boxLandingDelay + boxXiRuTime;
        yield return new WaitForSeconds(timeBeforeWallBeganToAppear);

        foreach (int i in indexOfDisabledWalls)
        {
            walls.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(wallDisappearanceDelay);
        }

        //After all walls have been enabled, load next level
        LoadNextScene();
    }

    public IEnumerator LandingBox(GameObject box, float aTime)
    {

        float timeBeforeBoxBeginToLand = levelLoadDelay + indexOfDisabledWalls.Count * wallDisappearanceDelay + boxLandingDelay;
        yield return new WaitForSeconds(timeBeforeBoxBeginToLand);

        float currentY = box.transform.position.y;
        for (float moveProgress = 0.0f; moveProgress < 1.0f; moveProgress += Time.deltaTime / aTime)
        {
            float fixedMoveProgress = Mathf.Sin(Mathf.PI / 2 * moveProgress);
            float newY = Mathf.Lerp(currentY, 0.5f, fixedMoveProgress);
            box.transform.position = new Vector3(box.transform.position.x, newY, box.transform.position.z);
            yield return null;
        }
        box.transform.position = new Vector3(box.transform.position.x, 0.5f, box.transform.position.z);
        box.GetComponent<BoxMovement>().enabled = true;

        //Enable all detectors
        detectors.SetActive(true);
    }

    //public IEnumerator FlyingBox(GameObject box, float aTime)
    //{
    //    detectors.SetActive(false);
    //    yield return new WaitForSeconds(levelEndDealy);

    //    float currentY = box.transform.position.y;
    //    for (float moveProgress = 0.0f; moveProgress < 1.0f; moveProgress += Time.deltaTime / aTime)
    //    {
    //        float fixedMoveProgress = Mathf.Sin(Mathf.PI / 2 * moveProgress);
    //        float newY = Mathf.Lerp(currentY, 0.5f + boxYOffset, fixedMoveProgress);
    //        box.transform.position = new Vector3(box.transform.position.x, newY, box.transform.position.z);
    //        yield return null;
    //    }

    //    box.transform.position = new Vector3(box.transform.position.x, 0.5f + boxYOffset, box.transform.position.z);
    //    box.GetComponent<BoxMovement>().enabled = false;
    //}

    public IEnumerator EndLevel()
    {
        detectors.SetActive(false);
        yield return new WaitForSeconds(levelEndDealy);

        foreach (GameObject box in boxes)
        {
            box.GetComponent<BoxMovement>().enabled = false;
            box.GetComponent<Animator>().SetTrigger("StartXiRu");
        }
    }

}
