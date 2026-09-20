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
		fadeInSequence.Append(_groupTransform.DOMoveX(3.2f, 1f));
		return fadeInSequence;
	}


	public Sequence FadeOut()
	{
		Sequence fadeOutSequence = DOTween.Sequence();
		fadeOutSequence.Append(_groupTransform.DOMoveX(16f, 1f));
		return fadeOutSequence;
	}
}
