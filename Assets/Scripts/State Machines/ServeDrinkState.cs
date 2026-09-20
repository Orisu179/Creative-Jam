using System.Collections.Generic;
using System.Linq;
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
        }

        public void Enter()
        {
            // 1. Move customer to the right
            _customerSpriteManager.MoveRight();

            // calculate satisfaction
            int score = _manager.CalculateScore(_manager.CurDay);
            Day curDay = _manager.CurDay;
            curDay.SatisfactionLevel += score;

            // calc dialogue using day.current cust sat
            // setText(DialogueGenerator.GenerateResponse(score, ), callback);

            // add drink to list
            // _manager.AddDrink(CurrentDrink); 
            // call toggle
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
            // disable dialogue. ie. call dialogue.toggle
            // dialogueBox.Toggle();
            // sprite.fade out 
            _customerSpriteManager.FadeOut();
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
                _manager._stateMachine.ChangeState(_manager._dayStartState);
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
            for (int drinkIndex = 0; drinkIndex < _manager.CurDay.OrderList.Count; drinkIndex++)
            {
                Drink d = _manager.CurDay.OrderList[drinkIndex];
                foreach (Ingredient i in RecipeDatabase.GetRecipeIngredients(d))
                {
                    if (i == _manager.PoisonedIngredient)
                    {
                        isPoisoned = true;
                        poisonedList.Add(drinkIndex);
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
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}