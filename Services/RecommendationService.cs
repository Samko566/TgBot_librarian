using Python.Runtime;
using TgBot_librarian.Services.Interfaces;

namespace TgBot_librarian.Services
{
    public class RecommendationService : IRecommendationService
    {
        public string GetRecommendationsFromPython(string query)
        {
            try
            {
                Console.WriteLine("Імпорт модулів...");
                using (Py.GIL())
                {
                    dynamic pandas = Py.Import("pandas");
                    dynamic numpy = Py.Import("numpy");
                    dynamic program = Py.Import("python.program");

                    // Виклик функції process_query з програми Python
                    dynamic result = program.process_query(query);
                    return result.ToString();
                }
            }
            catch (PythonException ex)
            {
                Console.WriteLine($"Python error: {ex.Message}");
                return "Помилка при отриманні рекомендацій.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return "Помилка при отриманні рекомендацій.";
            }
        }
    }
}