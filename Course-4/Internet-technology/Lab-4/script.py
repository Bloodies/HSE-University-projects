
import requests
from bs4 import BeautifulSoup
import spacy
from pprint import pprint
from nltk.classify import NaiveBayesClassifier
from nltk.corpus import stopwords
import nltk
import random
from nltk.corpus import movie_reviews
from nltk.tokenize import word_tokenize
from spacy.util import minibatch, compounding
import string

#nltk.download(['names', 'punkt', 'stopwords', 'movie_reviews', 'state_union', 'averaged_perceptron_tagger', 'vader_lexicon',  'NaiveBayesClassifier',])
spec_chars = string.punctuation + '\n\xa0«»\t—…-...]['
classifier = NaiveBayesClassifier


def remove_chars(word, chars):
    return "".join([ch for ch in word if ch not in chars])

def unigram_features(dict_words):
    return dict((word, True) for word in dict_words)


def extract_features(corpus, file_ids, cls, feature_extractor=unigram_features):
    return [(feature_extractor(corpus.words(i)), cls) for i in file_ids]


def Scraping(movie_id):
    title_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/'
    rating_request = requests.get(title_url)
    title = BeautifulSoup(rating_request.text, 'html.parser')
    rating = title.find('span', class_='AggregateRatingButton__RatingScore-sc-1ll29m0-1 iTLWoV').get_text()

    review_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/reviews?ref_=tt_ov_rt'
    request = requests.get(review_url)
    soup = BeautifulSoup(request.text, 'html.parser')

    movie = soup.find('div', class_='subpage_title_block__right-column')

    name = movie.find('div', class_='parent').find('a').get_text()
    year = movie.find('div', class_='parent').find('span').get_text().replace(' ', '').replace('\n', '')

    review = []

    items_list = soup.find('div', class_='lister').find('div', class_='lister-list')
    review_list = items_list.find_all('div', class_='lister-item-content')
    for item in review_list:
        parent = item.find('span', class_='rating-other-user-rating')
        if parent is not None:
            review_rating = parent.find('span').get_text()
        else:
            review_rating = 'not rated'
        review_title = item.find('a', class_='title').get_text().replace(' ', '').replace('\n', '')
        username = item.find('div', class_='display-name-date').find('a').get_text()
        posted_date = item.find('span', class_='review-date').get_text()
        review_text = item.find('div', class_='content').find('div', class_='text show-more__control').get_text()

        review_item = {'username': username,
                       'rating': review_rating,
                       'review_title': review_title,
                       'posted_date': posted_date,
                       'review': review_text}

        review.append(review_item)

    movie_info = {'name': name, 'year': year, 'url': review_url, 'rating': float(rating), 'reviews': review}
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
    global classifier
    input_text = remove_chars(input_text, spec_chars)
    input_text = remove_chars(input_text, string.digits)

    tokens = word_tokenize(input_text)
    print(f"{classifier.classify(unigram_features(tokens))}")
    classifier.show_most_informative_features()

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


if __name__ == '__main__':
    Santiment_training()

    top5 = ['0111161', '0068646', '0071562', '0468569', '0050083']
    bottom5 = ['1213644', '0270846', '4458206', '0060666', '4009460']
    rand5 = [random.randint(1, 10872600) for n in range(5)]

    movie_list = Scraping('0111161')
    # for i in rand5:
    #     if len(str(i)) < 7:
    #         movie_list = Scrapping('0' * (7 - len(str(i))) + str(i))
    #     else:
    #         movie_list = Scrapping(i)
    print(f"{movie_list['url']}\n{movie_list['name']} {movie_list['year']} {movie_list['rating']}/10")
    for item in movie_list['reviews']:
        vis_review = item['review'].replace('. ', '.\n||')
        print("-----------------------------------------------")
        print(f"{item['username']} {item['posted_date']}")
        print(f"{item['review_title']} ({item['rating']}/10)")
        print(f"||{vis_review}")
        Santiment_analysis(item['review'])
        #print("-----------------------------------------------")
        #pprint(movie_list['reviews'])


