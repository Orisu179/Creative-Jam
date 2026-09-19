using UnityEngine;

public class CustomerSpriteManager : MonoBehaviour
{
    private SpriteRenderer _owlSpriteRenderer;
    private SpriteRenderer _accessorySpriteRenderer;
    private Transform _accessoryTransform;
    [SerializeField] private CustomerObject _customerObject;
    [SerializeField] private TextAsset _customerConstants;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AccessoryData.LoadFromJson(_customerConstants);

        var _childrenSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _owlSpriteRenderer = _childrenSpriteRenderers[0];
        _accessorySpriteRenderer = _childrenSpriteRenderers[1];
        _accessoryTransform = _accessorySpriteRenderer.transform;

        SetSprite(OwlSpecies.GREATER_HORNED_OWL, Accessory.TIE);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSprite(OwlSpecies species, Accessory accessory)
    {
        // Set the owl sprite based on the species
        _owlSpriteRenderer.sprite = _customerObject.customerSprites[(int)species];

        // Set the accessory sprite based on the accessory type
        _accessorySpriteRenderer.sprite = _customerObject.accessorySprites[(int)accessory];

        // Adjust the accessory position based on the accessory type
        if (accessory != Accessory.NONE)
        {
            int yOffset = AccessoryData.AccessoryOffsets[accessory];
            _accessoryTransform.localPosition = new Vector3(0, yOffset, 0);
        }
    }

    public static (OwlSpecies species, Accessory accessory) GetRandomOwlSpeciesAndAccessory()
    {
        var allSpecies = (OwlSpecies[])System.Enum.GetValues(typeof(OwlSpecies));
        var allAccessories = (Accessory[])System.Enum.GetValues(typeof(Accessory));

        var randomSpecies = allSpecies[Random.Range(0, allSpecies.Length)];
        var randomAccessory = allAccessories[Random.Range(0, allAccessories.Length)];

        return (randomSpecies, randomAccessory);
    }
}
