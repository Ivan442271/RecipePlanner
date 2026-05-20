using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.ConsoleUI.Menus;

namespace RecipePlanner.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();

            mainMenu.Show();
        }
    }
}
