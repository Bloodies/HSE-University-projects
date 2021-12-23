import matplotlib.pyplot as plt
import numpy as np
from bs4 import BeautifulSoup
from nltk.classify import NaiveBayesClassifier
from nltk.corpus import movie_reviews
from textblob import TextBlob
import nltk
import random
import requests
import string
import os

# nltk.download(['names',
#                'punkt',
#                'stopwords',
#                'movie_reviews',
#                'state_union',
#                'averaged_perceptron_tagger',
#                'vader_lexicon',
#                'NaiveBayesClassifier'])
spec_chars = string.punctuation + '\n\xa0«»\t—…-...]['
classifier = NaiveBayesClassifier


def remove_chars(word, chars):
    return "".join([ch for ch in word if ch not in chars])


def unigram_features(dict_words):
    return dict((word, True) for word in dict_words)


def extract_features(corpus, file_ids, cls, feature_extractor=unigram_features):
    return [(feature_extractor(corpus.words(j)), cls) for j in file_ids]


def Scraping(movie_id):
    title_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/'
    rating_request = requests.get(title_url)
    title = BeautifulSoup(rating_request.text, 'html.parser')
    movie_rating = title.find('span', class_='AggregateRatingButton__RatingScore-sc-1ll29m0-1 iTLWoV').get_text()

    review_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/reviews?ref_=tt_ov_rt'
    request = requests.get(review_url)
    soup = BeautifulSoup(request.text, 'html.parser')

    movie = soup.find('div', class_='subpage_title_block__right-column')

    name = movie.find('div', class_='parent').find('a').get_text()
    year = movie.find('div', class_='parent').find('span').get_text().replace(' ', '').replace('\n', '')

    review = []

    items_list = soup.find('div', class_='lister').find('div', class_='lister-list')
    review_list = items_list.find_all('div', class_='lister-item-content')
    for list_item in review_list:
        parent = list_item.find('span', class_='rating-other-user-rating')
        if parent is not None:
            review_rating = parent.find('span').get_text()
        else:
            review_rating = 'not rated'
        review_title = list_item.find('a', class_='title').get_text().replace(' ', '').replace('\n', '')
        username = list_item.find('div', class_='display-name-date').find('a').get_text()
        posted_date = list_item.find('span', class_='review-date').get_text()
        review_text = list_item.find('div', class_='content').find('div', class_='text show-more__control').get_text()

        review_item = {'username': username,
                       'rating': review_rating,
                       'review_title': review_title,
                       'posted_date': posted_date,
                       'review': review_text}

        review.append(review_item)

    movie_info = {'name': name, 'year': year, 'url': review_url, 'rating': float(movie_rating), 'reviews': review}
    return movie_info


def Santiment_training():
    global classifier
    data = dict(pos=movie_reviews.fileids('pos'),
                neg=movie_reviews.fileids('neg'))

    neg_training = extract_features(movie_reviews, data['neg'][:900], 'negative')
    pos_training = extract_features(movie_reviews, data['pos'][:900], 'positive')

    neg_test = extract_features(movie_reviews, data['neg'][900:], 'negative')
    pos_test = extract_features(movie_reviews, data['pos'][900:], 'positive')

    train_set = pos_training + neg_training
    test_set = pos_test + neg_test

    classifier = NaiveBayesClassifier.train(train_set)
    accuracy = nltk.classify.util.accuracy(classifier, test_set)
    print(f"Santiment analysis accuracy: {accuracy * 100}%")


def Santiment_analysis(input_text):
    # global classifier
    input_text = remove_chars(input_text, spec_chars)
    input_text = remove_chars(input_text, string.digits)

    # tokens = word_tokenize(input_text)
    # print(f"Тональность текста:\033[31m {classifier.classify(unigram_features(tokens))}\033[0m ")
    #
    # return classifier.classify(unigram_features(tokens))
    blob = TextBlob(input_text)
    if blob.sentiment.polarity < 0:
        print(f"Тональность текста:\033[31m Negative ({blob.sentiment.polarity:.2f}/1)\033[0m ")
    elif blob.sentiment.polarity == 0:
        print(f"Тональность текста:\033[31m Neutral ({blob.sentiment.polarity:.2f}/1)\033[0m ")
    else:
        print(f"Тональность текста:\033[31m Positive ({blob.sentiment.polarity:.2f}/1)\033[0m ")

    return round(blob.sentiment.polarity, 2)


# def remove_chars(word, chars):
#         return "".join([ch for ch in word if ch not in chars])
#
# def create_word_features(words):
#     useful_words = [word for word in words if word not in stopwords.words("english")]
#     my_dict = dict([(word, True) for word in useful_words])
#
#     return my_dict
#
# def Santiment_training():
#     neg_reviews = []
#     for fileid in movie_reviews.fileids('neg'):
#         words = movie_reviews.words(fileid)
#         neg_reviews.append((create_word_features(words), "negative"))
#
#     pos_reviews = []
#     for fileid in movie_reviews.fileids('pos'):
#         words = movie_reviews.words(fileid)
#         pos_reviews.append((create_word_features(words), "positive"))
#
#     train_set = neg_reviews[:750] + pos_reviews[:750]
#     test_set = neg_reviews[750:] + pos_reviews[750:]
#
#     global classifier
#     classifier = NaiveBayesClassifier.train(train_set)
#
#     accuracy = nltk.classify.util.accuracy(classifier, test_set)
#     print(accuracy * 100)


def DoPlot(x_input, y_input, x_label, y_label, title):
    x = np.array(x_input)
    y = np.array(y_input)
    args = np.argsort(x)
    x = x[args]
    y = y[args]
    plt.xlabel(x_label)
    plt.ylabel(y_label)
    plt.title(title)
    plt.scatter(x, y)
    fit = np.polyfit(x, y, deg=4)
    p = np.poly1d(fit)
    plt.plot(x, p(x), "r--")


if __name__ == '__main__':
    # Santiment_training()

    if not os.path.exists('Reviews'):
        os.mkdir('Reviews')

    positive = []
    negative = []
    neutral = []
    rating = []

    pred5 = ['0111161', '0068646', '0000005', '1213644', '0060666']
    rand5 = [random.randint(1, 10872600) for n in range(5)]
    # movie_list = Scraping('0060666')
    for i in pred5:
        if len(str(i)) < 7:
            movie_list = Scraping('0' * (7 - len(str(i))) + str(i))
        else:
            movie_list = Scraping(i)

        print(f"{movie_list['url']}\n{movie_list['name']} {movie_list['year']} {movie_list['rating']}/10")
        with open(f"Reviews/{movie_list['name'].replace(':', '')} (reviews).txt", 'w+', encoding="utf-8") as file:
            file.write(f"{movie_list['url']}\n{movie_list['name']} {movie_list['year']} {movie_list['rating']}/10\n")
            for item in movie_list['reviews']:
                vis_review = item['review'].replace('. ', '.\n||')
                print("-----------------------------------------------")
                print(f"{item['username']} {item['posted_date']}")
                print(f"{item['review_title']} ({item['rating']}/10)")
                if item['rating'] == 'not rated':
                    rating.append(int(0))
                else:
                    rating.append(int(item['rating']))
                print(f"||{vis_review}")

                analyze = Santiment_analysis(item['review'])

                file.write("-----------------------------------------------\n" +
                           f"{item['username']} {item['posted_date']}\n" +
                           f"{item['review_title']} ({item['rating']}/10)\n" +
                           f"||{vis_review}\n" +
                           f"Тональность текста: Negative ({analyze}/1)\n")
                if analyze < 0:
                    negative.append(analyze)
                    positive.append(0.0)
                elif analyze > 0:
                    negative.append(0.0)
                    positive.append(analyze)

            DoPlot(positive, rating, "Тональность", "Рейтинг", "Зависимость рейтинга от тональности")
            try:
                DoPlot(negative, rating, "Тональность", "Рейтинг", "Зависимость рейтинга от тональности")
            except Exception as e:
                print(e)

            plt.show()
            # plt.savefig(f"Reviews/{movie_list['name'].replace(':', '')} (plot).png", bbox_inches='tight')
