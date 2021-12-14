using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Weapon{
    public WeaponEntity weapon;
    public GameObject slot;
    public KeyCode KeyCode;
}

public class WeaponManagerScript : MonoBehaviour
{
 
    
    
    public Weapon[] weapons;
    public GameObject Player;
    public GameObject crosshair;
    public GameObject bulletStart;
    public AudioSource audioSource;

    private Weapon currentWeapon;
    private Dictionary<KeyCode, Weapon> keyBindings = new Dictionary<KeyCode, Weapon>();


    
    void Start()
    {
        currentWeapon = weapons.Length >= 1  ? weapons[0] : null;

        foreach(Weapon weaponEntry in weapons){


            weaponEntry.weapon.Player = Player;
            weaponEntry.weapon.crosshair = crosshair;
            weaponEntry.weapon.bulletStart = bulletStart;
            weaponEntry.weapon.audioSource = audioSource;
            weaponEntry.weapon.Start();

            weaponEntry.slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite = weaponEntry.weapon.bulletPrefab.GetComponent<SpriteRenderer>().sprite;
            weaponEntry.weapon.weaponImage = weaponEntry.slot.transform.Find("Image").gameObject.GetComponent<Image>();
            weaponEntry.weapon.cooldownImage =  weaponEntry.slot.transform.Find("Cooldown").gameObject.GetComponent<Image>();
            



            keyBindings.Add(weaponEntry.KeyCode, weaponEntry);
        }

        if (currentWeapon != null){
            alterScaling();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon == null){
            return;
        }

        // cooldown timer for all weapons

        updateCooldown();

        foreach(var keyBinding in keyBindings){
            if(Input.GetKeyDown(keyBinding.Key)){
                resetScaling();
                currentWeapon.weapon.onSwitch();
                currentWeapon = keyBinding.Value;
                alterScaling();
                break;
            }
        }
       
        if(!currentWeapon.weapon.switched){
            currentWeapon.weapon.Start();
        }
        currentWeapon.weapon.Run();

    }

    public void updateCooldown(){

        foreach (var weaponEntity in weapons){
            weaponEntity.weapon.updateCooldown();
        }

    }


    public void resetScaling(){
        currentWeapon.weapon.weaponImage.transform.localScale = new Vector3(1f,1f,1f);
        currentWeapon.weapon.cooldownImage.transform.localScale = new Vector3(1f,1f,1f);

    }

    public void alterScaling(){
        currentWeapon.weapon.weaponImage.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        currentWeapon.weapon.cooldownImage.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
    }
}
