import pandas as pd
import nltk
from nltk.tokenize import word_tokenize
from nltk.corpus import stopwords
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.linear_model import LogisticRegression

# Завантаження даних
data = pd.read_csv('library.csv', encoding='cp1251')

# Обробка пропущених значень
data = data.fillna(value={'description': ''})

# Обробка текстових ознак
nltk.download('punkt')
nltk.download('stopwords')
stop_words = set(stopwords.words('russian'))

def preprocess_text(text):
    tokens = word_tokenize(text.lower())
    tokens = [token for token in tokens if token.isalpha() and token not in stop_words]
    return ' '.join(tokens)

data['description_processed'] = data['description'].apply(preprocess_text)

# Об'єднання ознак
data['combined_features'] = data['description_processed'] + ' ' + \
                            data['title'] + ' ' + \
                            data['authors'] + ' ' + \
                            data['genre'] + ' ' + \
                            data['type']

# Розділення на тренувальні та тестові дані
X_train = data['combined_features']
y_train = data['bookID']

# Векторизація ознак
vectorizer = TfidfVectorizer()
X_train_transformed = vectorizer.fit_transform(X_train)

# Модель класифікації
model = LogisticRegression(max_iter=500)
model.fit(X_train_transformed, y_train)

# Перетворення індексів книг на назви
book_id_to_title = dict(data[['bookID', 'title']].values)

def GetBookRecommendations(query):
    # Розбиття запиту на окремі слова
    query_words = query.lower().split()

    # Задання кількості рекомендацій залежно від запиту
    num_recommendations = 1  # За замовчуванням

    if any(word.isdigit() for word in query_words):
        num_recommendations = int(next((word for word in query_words if word.isdigit()), 1))

    # Пошук відповідних книг для кожного слова запиту
    matching_books = pd.DataFrame(columns=data.columns)

    for word in query_words:
        filtered_books = data[data['combined_features'].str.contains(word)]
        matching_books = matching_books._append(filtered_books)

    if matching_books.empty:
        return []  # Повернути порожній список, якщо немає підходящих книг
    else:
        # Обмеження кількості рекомендацій
        matching_books = matching_books.head(num_recommendations)

        # Конвертація DataFrame у список словників
        recommendations = matching_books.to_dict('records')

        return recommendations

