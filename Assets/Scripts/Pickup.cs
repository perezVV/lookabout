using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    public GameObject itemButton;
    


    private FlashlightPower flashlight;

    [Header("SFX")] 
    [SerializeField] private AudioClip pickupSound;
    
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<FlashlightPower>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Battery"))
            {
                if (!flashlight.CanPickupBattery())
                {
                    return;
                }
                Debug.Log("battery pickup");
                SFXController.instance.PlaySFX(pickupSound, transform, 0.1f);
                flashlight.PickupBattery();
                Destroy(gameObject);
                return;
            }
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //check if item can be add into inventory or not
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    inventory.slots[i].tag = "Key";
                    SFXController.instance.PlaySFX(pickupSound, transform, 0.5f);
                    // Get the key's name and display it as text on the inventory button.
                    // GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnController>().UnassignLocation(transform.position);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
