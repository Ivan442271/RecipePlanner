using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using RecipePlanner.Domain.Interfaces;
using RecipePlanner.Domain.Models;

namespace RecipePlanner.Application.Repositories
{
    public class JsonRecipeRepository : IRepository<Recipe>
    {
        private readonly string FilePath;

        public JsonRecipeRepository()
        {
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RecipePlanner");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            FilePath = Path.Combine(appFolder, "recipes.json");
            string settingsPath = Path.Combine(appFolder, "settings.json");
            string assemblyName = "RecipePlanner.ConsoleUI";

            if (!File.Exists(FilePath))
            {
                using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream($"{assemblyName}.recipes.json"))
                {
                    if (stream != null)
                    {
                        using (FileStream fileStream = File.Create(FilePath))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
            }

            if (!File.Exists(settingsPath))
            {
                using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream($"{assemblyName}.settings.json"))
                {
                    if (stream != null)
                    {
                        using (FileStream fileStream = File.Create(settingsPath))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
            }
        }

        public List<Recipe> GetAll()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Recipe>();
            }

            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
            }
            catch (Exception)
            {
                return new List<Recipe>();
            }
        }

        public void SaveAll(List<Recipe> items)
        {
            try
            {
                string json = JsonSerializer.Serialize(items, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception)
            {
            }
        }
    }
}