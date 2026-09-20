using UnityEngine;
using DG.Tweening;

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

    public Sequence FadeIn()
    {
        _owlSpriteRenderer.color = Color.black;
        _accessorySpriteRenderer.color = Color.black;
        Sequence fadeInSequence = DOTween.Sequence();
        fadeInSequence.Insert(0, _owlSpriteRenderer.DOFade(1f, 1f).From(0f));
        fadeInSequence.Insert(0, _accessorySpriteRenderer.DOFade(1f, 1f).From(0f));

        fadeInSequence.Insert(0.2f, _owlSpriteRenderer.DOColor(Color.white, 0.5f));
        fadeInSequence.Insert(0.2f, _accessorySpriteRenderer.DOColor(Color.white, 0.5f));

        return fadeInSequence;
    }


    public Sequence FadeOut()
    {
        Sequence fadeOutSequence = DOTween.Sequence();
        fadeOutSequence.Insert(0, _owlSpriteRenderer.DOColor(Color.black, 0.5f));
        fadeOutSequence.Insert(0, _accessorySpriteRenderer.DOColor(Color.black, 0.5f));
        fadeOutSequence.Insert(0.5f, _owlSpriteRenderer.DOFade(0f, 1f));
        fadeOutSequence.Insert(0.5f, _accessorySpriteRenderer.DOFade(0f, 1f));

        return fadeOutSequence;
    }

    public Sequence MoveLeft()
    {
        Sequence moveLeftSequence = DOTween.Sequence();
        moveLeftSequence.Append(transform.DOMoveX(-5, 1));
        return moveLeftSequence;
    }

    public Sequence MoveRight()
    {
        Sequence moveRightSequence = DOTween.Sequence();
        moveRightSequence.Append(transform.DOMoveX(0, 1));
        return moveRightSequence;
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
