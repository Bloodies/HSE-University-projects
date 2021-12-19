# nltk.download(['names', 'punkt', 'stopwords', 'movie_reviews', 'state_union', 'twitter_samples', 'averaged_perceptron_tagger', 'vader_lexicon',])
import requests
from bs4 import BeautifulSoup
from pprint import pprint
import nltk
import random
from nltk.corpus import movie_reviews


# url = 'https://www.imdb.com/india/top-rated-indian-movies/'

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
    pprint(movie_info)
    return movie_info


if __name__ == '__main__':
    top5 = ['0111161', '0068646', '0071562', '0468569', '0050083']
    bottom5 = ['1213644', '0270846', '4458206', '0060666', '4009460']
    rand5 = [random.randint(1, 10872600) for n in range(5)]

    movie_list = Scraping('0111161')
    # for i in rand5:
    #     if len(str(i)) < 7:
    #         movie_list = Scrapping('0' * (7 - len(str(i))) + str(i))
    #     else:
    #         movie_list = Scrapping(i)
