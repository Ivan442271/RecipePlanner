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
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            MainMenu mainMenu = new MainMenu();

            mainMenu.Show();
        }
    }
}
