import requests
import string
import time
import os
import datetime
from bs4 import BeautifulSoup

# id, название, возрастной рейтинг, длительность, год, страна, студия, бюджет, imdb популярность,
# imdb рейтинг, сборы в 1 уикенд, сборы в мире,

date_now = datetime.datetime.now()


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


if __name__ == '__main__':
    if not os.path.exists('datasets'):
        os.mkdir('datasets')

    print(f"dataset-{date_now.day}-{date_now.month}-{date_now.year}")
    with open(f"dataset-{date_now.day}-{date_now.month}-{date_now.year}", 'w+', encoding='utf-8') as file:
        file.write(f"text\n")
