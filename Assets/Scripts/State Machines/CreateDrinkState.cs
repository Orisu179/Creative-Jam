using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using Gameplay;

namespace State_Machines
{
    public class CreateDrinkState : IState
    {
        private CreateDrink _createDrink;
        private Drink? _currentDrink;
        private GameManager _manager;
        private BrewingSpriteManager _brewingSpriteManager;

        public CreateDrinkState(GameManager manager, CreateDrink drink, BrewingSpriteManager brewingSpriteManager)
        {
            _manager = manager;
            _createDrink = drink;
            _brewingSpriteManager = brewingSpriteManager;
        }

        public async void Enter()
        {
            Debug.Log("entering create drink state");
            // Make the drinks interactable
            GlobalFields.Instance.MouseInteractable = true;
            _currentDrink = null;
            // 1. Fade in the create drink UI
            Tween fadeIn = _brewingSpriteManager.FadeIn();
            await fadeIn.AsyncWaitForCompletion();

            // 2. Enable ingredients listeners
            _createDrink.OnDrinkCreated = HandleDrinkCreated;
            // 3. Enable the menu button

            // enable button to open/close menu
            _manager.DrinkMenu.EnableButton();

            MixingCupArea.OnAnyItemDropped -= _createDrink.HandleIngredientDropped;
            MixingCupArea.OnAnyItemDropped += _createDrink.HandleIngredientDropped;

            // Serving: dragging the finished cup onto a customer fires this.
            CustomerDropArea.OnAnyCupServed += HandleCupServed;

            // Test create drink
            _createDrink.drinkManager.ResetMix();
        }

        public async Task Exit()
        {
            GlobalFields.Instance.MouseInteractable = false;
            _createDrink.RemoveDrink();
            // close and disable menu
            _manager.DrinkMenu.CloseAndDisable();

            MixingCupArea.OnAnyItemDropped -= _createDrink.HandleIngredientDropped;
            CustomerDropArea.OnAnyCupServed -= HandleCupServed;
            // TODO: Disable ingredients listeners
            // Ease out the create drink UI
            Tween fadeOut = _brewingSpriteManager.FadeOut();
            await fadeOut.AsyncWaitForCompletion();
            // Customer move is handled by the next state
            Debug.Log("exiting create drink state");

        }

        public void HandleDrinkCreated(Drink? drink)
        {
            if (drink == null)
            {
                return;
            }
            SoundManager.Instance.PostEvent(SoundManager.SoundEvent.PlayDrinkReval);
            _currentDrink = drink;
            Debug.Log($"Drink created: {drink.Value}");
        }

        private async void HandleCupServed(CustomerDropArea area, Drink drink)
        {
            await HandleFeedDrink();
        }

        private async Task HandleFeedDrink() // save current drink
        {
            if (_currentDrink == null)
            {
                Debug.LogWarning("HandleFeedDrink called with no drink prepared.");
                return;
            }

            Debug.Log($"served the following drink: {_currentDrink.Value} to move to the next state");
            _manager.SetCurrentDrink(_currentDrink.Value);
            _manager.AddDrink(_currentDrink.Value);
            await _createDrink.RemoveDrink();
            _manager._stateMachine.ChangeState(_manager._serveDrinkState);
        }
    }
}