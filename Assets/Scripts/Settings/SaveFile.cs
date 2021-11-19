public class SaveFile
{
    //Player
    //public Transform PlayerTransform
    public int lastPlayerScene = 2;

    //GameSettings
    public float TotalVolume = 1f;
    public float BGMVolume = 0.2f;
    public float SFXVolume = 0.2f;

    //Inventory
    public ItemSlot[] itemSlots = new ItemSlot[20];

    //Misc
    public bool gameJustStarted = true;

    public void SetGameSettings(GameSettings settings)
    {
        TotalVolume = settings.TotalVolume;
        BGMVolume = settings.BGMVolume;
        SFXVolume = settings.SFXVolume;
        gameJustStarted = settings.gameJustStarted;
    }

    public void SetPlayerInventory(Inventory playerInventory)
    {
        int containerSize = playerInventory.ItemContainer.ContainerSize();
        itemSlots = new ItemSlot[containerSize];
        for (int i = 0; i < containerSize; i++)
        {
            ItemSlot itemSlot = playerInventory.ItemContainer.getItemAtIndex(i);
            if (itemSlot.quantity > 0)
            {
                itemSlots[i] = itemSlot;
            }
        }
    }
}
