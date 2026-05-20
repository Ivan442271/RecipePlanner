using RecipePlanner.UI;

while (true)
{
    try
    {
        Console.Clear();
        ConsoleHelper.WriteHeader("Конструктор рецептів");

        Console.WriteLine("1. Рецепти");
        Console.WriteLine("2. Додати рецепт");
        Console.WriteLine("3. Обране");
        Console.WriteLine("4. Налаштування");
        Console.WriteLine("0. Вихід");
        Console.WriteLine();

        int choice = ConsoleHelper.ReadInt("Оберіть пункт меню", 0, 4);

        switch (choice)
        {
            case 1:
                Console.WriteLine("\nТут буде список рецептів...");
                break;
            case 2:
                // Здесь мы скоро напишем вызов service.AddRecipe
                break;
            case 3:
                Console.WriteLine("\nТут будуть ваші улюблені рецепти...");
                break;
            case 4:
                Console.WriteLine("\nРозділ налаштувань у розробці...");
                break;
            case 0:
                Console.WriteLine("До зустрічі!");
                return;
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        ConsoleHelper.WriteError(ex.Message);
        Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
    }
}