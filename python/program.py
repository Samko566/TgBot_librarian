from library import GetBookRecommendations

def process_query(query):
    # Виклик GetBookRecommendations з library.py
    recommendations = GetBookRecommendations(query)

    if not recommendations:
        return "На жаль, я не знайшов жодної підходящої книги. Будь ласка, уточніть свій запит."
    else:
        result = ""
        for book in recommendations:
            book_info = (
                "Рекомендована книга:\n"
                f"Назва: {book['title']}\n"
                f"Автор: {book['authors']}\n"
                f"Тема книги: {book['genre']}\n"
                f"Жанр: {book['type']}\n"
                f"Опис: {book['description']}\n"
                "------------------------------------------------"
            )
            result += book_info
        return result