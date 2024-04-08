﻿using System;

namespace RecipeManager
{
    // Class to represent an ingredient in the recipe
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }

    // Class to represent a step in the recipe
    class Step
    {
        public int Number { get; set; }
        public string Description { get; set; }
    }

    class Program
    {
        static Ingredient[] ingredients; // Array to store ingredients
        static Ingredient[] originalQuantities; // Array to store original quantities
        static Step[] steps; // Array to store steps
        static bool recipeEntered = false; // Flag to track if a recipe has been entered

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Recipe Manager!");
            DisplayMenu();

            string choice = Console.ReadLine();
            while (choice != "7")
            {
                switch (choice)
                {
                    case "1":
                        if (!recipeEntered)
                            EnterRecipe();
                        else
                            Console.WriteLine("A recipe already exists. Clear the data first to enter a new recipe.");
                        break;
                    case "2":
                        if (recipeEntered)
                            EditRecipe();
                        else
                            Console.WriteLine("No recipe entered yet.");
                        break;
                    case "3":
                        if (recipeEntered)
                            ScaleRecipe();
                        else
                            Console.WriteLine("No recipe entered yet.");
                        break;
                    case "4":
                        if (recipeEntered)
                            ResetQuantities();
                        else
                            Console.WriteLine("No recipe entered yet.");
                        break;
                    case "5":
                        if (recipeEntered)
                            DisplayRecipe();
                        else
                            Console.WriteLine("No recipe entered yet.");
                        break;
                    case "6":
                        if (recipeEntered && ConfirmClear())
                        {
                            ClearData();
                            Console.WriteLine("Current recipe cleared.");
                        }
                        else
                        {
                            Console.WriteLine("Action canceled.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }

                DisplayMenu();
                choice = Console.ReadLine();
            }

            Console.WriteLine("Goodbye!");
        }

        // Method to display the menu options
        static void DisplayMenu()
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Enter a new recipe");
            Console.WriteLine("2. Edit recipe");
            Console.WriteLine("3. Scale recipe");
            Console.WriteLine("4. Reset quantities");
            Console.WriteLine("5. Display recipe");
            Console.WriteLine("6. Clear current recipe");
            Console.WriteLine("7. Exit");
        }

        // Method to enter a new recipe
        static void EnterRecipe()
        {
            Console.WriteLine("Enter the number of ingredients:");
            int numIngredients = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the number of steps:");
            int numSteps = int.Parse(Console.ReadLine());

            // Initialize arrays based on user input
            ingredients = new Ingredient[numIngredients];
            originalQuantities = new Ingredient[numIngredients];
            steps = new Step[numSteps];

            // Get details for each ingredient
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter details for ingredient {i + 1}:");
                ingredients[i] = new Ingredient();

                Console.Write("Name: ");
                ingredients[i].Name = Console.ReadLine();

                Console.Write("Quantity: ");
                ingredients[i].Quantity = double.Parse(Console.ReadLine());

                Console.Write("Unit: ");
                ingredients[i].Unit = Console.ReadLine();

                // Store original quantities
                originalQuantities[i] = new Ingredient
                {
                    Name = ingredients[i].Name,
                    Quantity = ingredients[i].Quantity,
                    Unit = ingredients[i].Unit
                };
            }

            // Get details for each step
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                steps[i] = new Step();

                Console.Write("Description: ");
                steps[i].Description = Console.ReadLine();
            }

            recipeEntered = true;
            Console.WriteLine("Recipe entered successfully.");
        }

        // Method to edit any part of the recipe
        static void EditRecipe()
        {
            Console.WriteLine("What would you like to edit? (ingredients, steps)");
            string editChoice = Console.ReadLine().ToLower();

            switch (editChoice)
            {
                case "ingredients":
                    EditIngredients();
                    break;
                case "steps":
                    EditSteps();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        // Method to edit ingredients
        static void EditIngredients()
        {
            Console.WriteLine("Enter the ingredient number to edit:");
            int ingredientNumber = int.Parse(Console.ReadLine()) - 1; // Adjust for zero-based index

            if (ingredientNumber >= 0 && ingredientNumber < ingredients.Length)
            {
                Console.WriteLine($"Current details of ingredient {ingredientNumber + 1}:");
                Console.WriteLine($"Name: {ingredients[ingredientNumber].Name}");
                Console.WriteLine($"Quantity: {ingredients[ingredientNumber].Quantity}");
                Console.WriteLine($"Unit: {ingredients[ingredientNumber].Unit}");

                Console.WriteLine("Enter new details:");
                Console.Write("Name: ");
                ingredients[ingredientNumber].Name = Console.ReadLine();

                Console.Write("Quantity: ");
                ingredients[ingredientNumber].Quantity = double.Parse(Console.ReadLine());

                Console.Write("Unit: ");
                ingredients[ingredientNumber].Unit = Console.ReadLine();

                Console.WriteLine("Ingredient updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ingredient number.");
            }
        }

        // Method to edit steps
        static void EditSteps()
        {
            Console.WriteLine("Enter the step number to edit:");
            int stepNumber = int.Parse(Console.ReadLine()) - 1; // Adjust for zero-based index

            if (stepNumber >= 0 && stepNumber < steps.Length)
            {
                Console.WriteLine($"Current description of step {stepNumber + 1}: {steps[stepNumber].Description}");
                Console.WriteLine("Enter new description:");
                steps[stepNumber].Description = Console.ReadLine();
                Console.WriteLine("Step updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid step number.");
            }
        }

        // Method to display the full recipe
        static void DisplayRecipe()
        {
            Console.WriteLine("\nRecipe:");
            for (int i = 0; i < ingredients.Length; i++)
            {
                Console.WriteLine($"{ingredients[i].Quantity} {ingredients[i].Unit} of {ingredients[i].Name}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine($"Step {i + 1}: {steps[i].Description}");
            }
        }

        // Method to scale the recipe based on user input
        static void ScaleRecipe()
        {
            Console.WriteLine("\nEnter scaling factor (0.5, 2, or 3):");
            double factor = double.Parse(Console.ReadLine());

            for (int i = 0; i < ingredients.Length; i++)
            {
                // Update quantities based on scaling factor
                ingredients[i].Quantity *= factor;

                // Adjust unit if necessary (example: tablespoons to cups)
                AdjustUnit(ingredients[i]);
            }

            Console.WriteLine("Recipe scaled successfully.");
        }

        // Method to adjust unit of measurement based on quantity
        static void AdjustUnit(Ingredient ingredient)
        {
            if (ingredient.Quantity >= 16 && ingredient.Unit == "tablespoon")
            {
                ingredient.Quantity /= 16;
                ingredient.Unit = "cup";
            }
            // Add more conversions as needed
        }

        // Method to reset ingredient quantities to original values
        static void ResetQuantities()
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                // Reset quantities to original values
                ingredients[i].Quantity = originalQuantities[i].Quantity;
            }

            Console.WriteLine("Quantities reset to original values.");
        }

        // Method to confirm before clearing data
        static bool ConfirmClear()
        {
            Console.WriteLine("Are you sure you want to clear the current recipe? (yes/no)");
            string response = Console.ReadLine().ToLower();
            return response == "yes";
        }

        // Method to clear all data
        static void ClearData()
        {
            ingredients = null; // Clear ingredients array
            originalQuantities = null; // Clear originalQuantities array
            steps = null; // Clear steps array
            recipeEntered = false;
        }
    }
}