using UnityEngine;

public class GameManager : MonoBehaviour
{   public static GameManager Instance;
    public MenuManager MenuManager;
    public UiManager UiManager;
    public ShopManager ShopManager;
    public BikeController BikeController;
    public TimerController TimerController;
    public RewardManager RewardManager;
    public SaveManager SaveManager;
    public AudioManager AudioManager;
    public int Coins;
    [SerializeField] private GameObject _bikePrefab;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        SaveManager.GetWalletData();
        MenuManager.GetLevelData();
        ShopManager.GetShopData();
    }
    public void SpawnBike()
    {
       GameObject bike= Instantiate(_bikePrefab);
        BikeController = bike.GetComponent<BikeController>();
    }
}
