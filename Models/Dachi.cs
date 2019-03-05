using System;

namespace Dojodachi.Models
{
    public class Dachi
    {
        private int? happiness;
        private int? fullness;
        private int? energy;
        private int? meals;

        private static int feedCost = 1; 
        private static int playCost = 5; 
        private static int workCost = 5; 
        private static int sleepCost = 5; 

        public int Happiness 
        { 
            get 
            { 
                if(happiness != null)
                    return (int)happiness;
                return 0;
            }
            set { happiness = (int?)value; }
        }
        public int Fullness 
        { 
            get 
            { 
                if(fullness != null)
                    return (int)fullness;
                return 0;
            }
            set { fullness = (int?)value; } 
        }
        public int Energy 
        { 
            get 
            { 
                if(energy != null)
                    return (int)energy;
                return 0;
            }
            set { energy = (int?)value; } 
        }
        public int Meals 
        { 
            get 
            { 
                if(meals != null)
                    return (int)meals;
                return 0;
            }
            set 
            { 
                if(value < 0) 
                    meals = 0;
                else
                    meals = (int?)value;
            } 
        }
        public Dachi()
        //this constructor is for new games bc these are default values
        {
            happiness = 20;
            fullness = 20;
            energy = 50;
            meals = 3;
        }
        public Dachi(int? happiness, int? fullness,int? energy, int? meals)
        //this constructor is for when it's already in session
        {
            this.happiness = happiness;
            this.fullness = fullness;
            this.energy = energy;
            this.meals = meals;
        }
        public bool didWin
        {
            get
            { 
                return 
                (
                    energy >= 60 &&
                    fullness >= 60 &&
                    happiness >= 60    
                );
            }
        }
        public bool didLose
        {
            get 
            {
                return 
                (
                    fullness <= 0 ||
                    happiness <= 0 ||
                    energy <= 0
                );
            }
        }
        public bool didLike 
        //this is a 25% chance of dachi not liking something.
        {
            get 
            {
                Random rand = new Random();
                return (rand.Next(100) > 25);
            }
        }
        public string Play(string gameType)
        //these are the actions(game moves) and returns a string
        {
            Random rand = new Random();
            if (gameType == "feed")
            {
                int addedFullness = rand.Next(5,11);
                if (Meals > 0)
                {
                    Meals -= feedCost;
                    if (didLike)
                    {
                        fullness += addedFullness;
                        return $"You fed your dojodachi! \nFullness +{addedFullness}, Meals -{feedCost}";
                    }
                    else
                        return $"You tried to feed your Dojodachi, but it didn't like it! \nMeals -{feedCost}";
                }
                return "You don't have enough meals to feed your Dojodach";
            }
            if (gameType == "play")
            {
                int addedHappiness = rand.Next(5,11);
                Energy -= playCost;
                if(didLike)
                {
                    Happiness += addedHappiness;
                    return $"You played with your Dojodachi! \nHappiness +{addedHappiness}, Energy -{playCost}";
                }
                return $"You tried to play with your Dojodachi, but it didn't like it! \nEnergy -{playCost}";
            }
            else if (gameType == "work")
            {
                int addedMeals = rand.Next(1,4);
                Energy -= workCost;
                Meals += addedMeals;
                return $"Your Dojodachi worked! \nMeals +{addedMeals}, Energy -{workCost}";
            }
            else //if it's not "play", "work", or "feed" then it's the last one which is "sleep"
            {
                Energy += 15;
                Fullness -= sleepCost;
                Happiness -= sleepCost;
                return $"Your Dojodachi slept! \nEnergy +15, Fullness -{sleepCost}, Happiness -{sleepCost}";
            }     
        }
    }
}