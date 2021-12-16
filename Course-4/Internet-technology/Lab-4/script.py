# nltk.download(['names', 'punkt', 'stopwords', 'movie_reviews', 'state_union', 'twitter_samples', 'averaged_perceptron_tagger', 'vader_lexicon',])
import requests
from bs4 import BeautifulSoup
from pprint import pprint
import nltk
from nltk.corpus import movie_reviews

url='https://www.imdb.com/india/top-rated-indian-movies/'
movie=requests.get(url)
soup=BeautifulSoup(movie.text,"html.parser")

def Scrapping():
    movie_div = soup.find("div", class_='lister')
    list = movie_div.find("div", class_='lister-list')
    items_list = list.find_all("div", class_='lister-item mode-detail imdb-user-review  collapsable')
    All_movie_list = []
    for item in items_list:
        name = item.find("td", class_='titleColumn')
        film_name = name.get_text().strip().split(".")
        # return film_name
        movie_names = name.find("a").get_text()
        # print(movie_names)
        movie_year = name.find("span").get_text()
        # print(movie_year)
        movie_position = film_name[0]
        # print(movie_position)
        movie_url = name.find("a").get("href")
        movie_link = "https://www.imdb.com" + movie_url

        rating_in_movie = item.find("td", class_="ratingColumn imdbRating").strong.get_text()

        movie_details = {'name': '', 'url': '', 'position': '', 'rating': '', 'year': ''}

        movie_details['name'] = movie_names
        movie_details['url'] = movie_link
        movie_details['position'] = int(movie_position)
        movie_details['rating'] = float(rating_in_movie)
        movie_details['year'] = int(movie_year.strip("()"))
        All_movie_list.append(movie_details)
    return All_movie_list


if __name__ == '__main__':
    Scrapping()