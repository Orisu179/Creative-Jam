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

            MixingCupArea.OnAnyItemDropped += _createDrink.HandleIngredientDropped;

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
            // TODO: Disable ingredients listeners
            // Ease out the create drink UI
            Tween fadeOut = _brewingSpriteManager.FadeOut();
            await fadeOut.AsyncWaitForCompletion();
            // Customer move is handled by the next state
        }

        public void HandleDrinkCreated(Drink? drink)
        {
            if (drink == null)
            {
                return;
            }
            _currentDrink = drink;
            Debug.Log($"Drink created: {drink.Value}");
        }

        private async Task HandleFeedDrink(Drink? drink)
        {
            _manager.SetCurrentDrink(drink);
            _manager.AddDrink(drink);
            await _createDrink.RemoveDrink();
            _manager._stateMachine.ChangeState(_manager._serveDrinkState);
        }
    }
}