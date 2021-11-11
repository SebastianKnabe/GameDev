public class SaveFile
{
    //Player
    //public Transform PlayerTransform
    //public int currentScene

    //GameSettings
    public float TotalVolume = 0.2f;
    public float BGMVolume = 0.2f;
    public float SFXVolume = 0.2f;

    //Inventory
    public ItemSlot[] itemSlots;

    public void SetGameSettings(GameSettings settings)
    {
        TotalVolume = settings.TotalVolume;
        BGMVolume = settings.BGMVolume;
        SFXVolume = settings.SFXVolume;
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
