using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace State_Machines
{
    public class ServeDrinkState : IState
    {
        private GameManager _manager;
        private CustomerSpriteManager _customerSpriteManager;
        private IState _failedState;
        private IState _nextState;
        public ServeDrinkState(GameManager manager, CustomerSpriteManager customerSpriteManager, IState failedState, IState nextState)
        {
            _manager = manager;
            _customerSpriteManager = customerSpriteManager;
            _nextState = nextState;
            _failedState = failedState;
        }

        public async void Enter()
        {
            // 1. Move customer to the right
            Tween moveRight = _customerSpriteManager.MoveRight();
            await moveRight.AsyncWaitForCompletion();

            // calculate satisfaction
            var score = _manager.CalculateScore(_manager.CurDay);
            _manager.SetSatisfactionLevel(_manager.CurDay.SatisfactionLevel + score);

            // calc dialogue using day.current cust sat
            TextBoxManager.Instance.SetDialogue(DialogueGenerator.GenerateResponse(score, _manager.CurDay.CurrentCustomer), HandleDialogueComplete);
            await TextBoxManager.Instance.SetDisabled(false, 1.0f);

            // call toggle
            Debug.Log($"Customer satisfaction level: {_manager.CurDay.SatisfactionLevel}");
        }

        public async Task Exit()
        {
            Tween fadeOut = _customerSpriteManager.FadeOut();
            await fadeOut.AsyncWaitForCompletion();
            // disable dialogue. ie. call dialogue.toggle
            // dialogueBox.Toggle();
            // sprite.fade out 
        }

        // handle callback from settext - ie. manager . next state
        private void HandleDialogueComplete()
        {
            if (_manager.CurDay.SatisfactionLevel < -10)
            {
                SceneManager.LoadScene("GameOverScene");
                return;
            }
            if (!_manager.IsLastCustomer())
            {
                _manager.IncrementCustomer();
                _manager._stateMachine.ChangeState(_nextState);
                return;
            }

            // if last customer, either win or lose
            if (_manager.PoisonedIngredient == null)
            {
                // sets poisoned ingredient out of set of used ingredients
                HashSet<Ingredient> usedIngredients = new HashSet<Ingredient>();
                foreach (Drink d in _manager.CurDay.OrderList)
                {
                    foreach (Ingredient i in RecipeDatabase.GetRecipeIngredients(d))
                    {
                        usedIngredients.Add(i);
                    }
                }
                _manager.PoisonedIngredient = usedIngredients.ElementAt(Random.Range(0, usedIngredients.Count));
            }

            bool isPoisoned = false;
            List<int> poisonedList = new List<int>();
            _manager.PoisonedDrinks.Clear();
            for (int drinkIndex = 0; drinkIndex < _manager.CurDay.OrderList.Count; drinkIndex++)
            {
                Drink d = _manager.CurDay.OrderList[drinkIndex];
                foreach (Ingredient i in RecipeDatabase.GetRecipeIngredients(d))
                {
                    if (i == _manager.PoisonedIngredient)
                    {
                        isPoisoned = true;
                        poisonedList.Add(drinkIndex);
                        _manager.PoisonedDrinks.Add(d);
                        break;
                    }
                }
            }

            if (isPoisoned)
            {
                // go to loop fail state
                _manager._stateMachine.ChangeState(_failedState);
            }
            else
            {
                // go to win state
                SceneManager.LoadScene("SuccessScene");
            }
        }
    }
}