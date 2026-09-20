using UnityEngine;
using DG.Tweening;

public class BrewingSpriteManager : MonoBehaviour
{
	private Transform _groupTransform;
	void Awake()
	{
		_groupTransform = GetComponent<Transform>();
	}


	public Sequence FadeIn()
	{
		Sequence fadeInSequence = DOTween.Sequence();
		fadeInSequence.Insert(0, _groupTransform.DOMoveX(0f, 1f));
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
