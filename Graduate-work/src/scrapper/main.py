import requests
import string
import random
import re
import multiprocessing
import threading
import time
import os
import datetime
from bs4 import BeautifulSoup

# id, название, возрастной рейтинг, длительность, год, страна, студия, бюджет, imdb популярность, сезон выхода, день выхода
# imdb рейтинг, сборы в 1 уикенд, сборы в мире,

headers = {'accept': '*/*',
           'user-agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36'}
date_now = datetime.datetime.now()
collected_imdb_counter = 0


# def mpaa_convert(c):
#     if c == 'M':
#         return 1000
#     elif c == 'D':
#         return 500
#     elif c == 'C':
#         return 100
#     elif c == 'L':
#         return 50
#     elif c == 'X':
#         return 10
#     elif c == 'V':
#         return 5
#     elif c == 'I':
#         return 1
#     return -1

def saving(data):
    for item in data:
        if item['name'] == 'skip':
            pass
        else:
            with open(f'imdb_set.txt', 'a', encoding='utf-8') as imdb_file:
                imdb_file.write(f'{item["id"]};{item["name"]};{item["year"]};{item["url"]}\n')


def scraping_imdb():
    counter = 1
    # counter = 2198

    # s = time.time()
    imdb_set = []

    while counter <= 10872600:
        if len(str(counter)) < 7:
            imdb_set.append(collect_movies('0' * (7 - len(str(counter))) + str(counter)))
        else:
            imdb_set.append(collect_movies(counter))

        if counter % 100 == 0:
            print('\n\t\t\t\t\033[31m\033[40m\033[6m\033[4m       SAVING DATA       \033[0m\n')
            saving(imdb_set)
            imdb_set.clear()

        counter += 1

    # print(time.time() - s)


def skip(arg):
    print(f'\t\t\033[31mskip\033[0m ({arg})')
    imdb_set = {'name': 'skip'}
    return imdb_set


def collect_movies(movie_id):
    global collected_imdb_counter
    hour, minute, year, mpaa = '', '', '', ''

    title_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/'
    request = requests.get(title_url, headers=headers)
    soup = BeautifulSoup(request.text, 'html.parser')

    success = soup.find('li', class_='ipc-inline-list__item')
    error_404_1 = soup.find('div', class_='error_code')
    error_404_2 = soup.find('div', class_='_error__QuoteBubbleText-sc-ql15x0-5')

    title = soup.find('h1', class_='TitleHeader__TitleText-sc-1wu6n3d-0')

    if title is not None:
        print(f'\033[33m {title.get_text()} \033[0m ({title_url}) '
              f'[0{movie_id} out of 10872600] \033[32m{collected_imdb_counter}\033[0m found')
    else:
        print(f'\033[31m No title \033[0m ({title_url}) '
              f'[0{movie_id} out of 10872600] \033[32m{collected_imdb_counter}\033[0m found')

    if error_404_1 is not None:
        if error_404_1.get_text() == '404':
            return skip('ERROR 404')

    if error_404_2 is not None:
        if error_404_2.get_text() == '404 Error':
            return skip('ERROR 404')

    if success.get_text() in ('TV Series', 'Video'):
        return skip(success.get_text())
    elif success.get_text()[:7] == 'Episode':
        return skip('EP of TV Series')
    elif success is None:
        return skip('No data')

    name = soup.find('div', class_='OriginalTitle__OriginalTitleText-sc-jz9bzr-0')
    if name is None:
        return skip('No original title')

    name = soup.find('div', class_='OriginalTitle__OriginalTitleText-sc-jz9bzr-0').get_text().replace(
        'Original title: ', '')

    items_list = soup.find('ul', class_='ipc-inline-list').find_all('li', class_='ipc-inline-list__item')
    for item in items_list:
        hour = re.findall(r'\b\w{1,2}[h]\b', item.get_text()) if not hour else hour
        minute = re.findall(r'\b\w{1,2}[m]\b', item.get_text()) if not minute else minute
        if item.find('a', class_='ipc-link') is not None:
            year = re.findall(r'\d{4}', item.get_text()) if not year else year
            mpaa = re.findall(r'\b[0-9]{1,2}\+', item.get_text()) if not mpaa else mpaa

    if year in ('', None):
        return skip('No year')

    hour = hour[0].replace('h', '')
    minute = minute[0].replace('m', '')
    year = year[0]
    mpaa = mpaa[0] if mpaa else 'Not rated'

    print(hour, minute, year, mpaa)

    duration = int(hour) * 60 + int(minute)
    if int(duration) <= 40:
        return skip(f'duration < 40 [{duration}min]')

    print(f'\t\t\033[32m{movie_id} {name} {year} {mpaa} {duration}min\033[0m')
    imdb_set = {'url': title_url, 'id': movie_id, 'name': name, 'year': year}
    collected_imdb_counter = collected_imdb_counter + 1
    return imdb_set


def take_year(url):
    regex = r'^\d{4}$'


def take_mpaa(url):
    regex = r'\b[0-9]{1,2}\+'


def take_duration(url):
    regex_h = r'\b\w{1,2}[h]\b'
    regex_m = r'\b\w{1,2}[m]\b'


def Scraping(movie_id):
    title_url = 'https://www.imdb.com/title/tt' + str(movie_id) + '/'
    request = requests.get(title_url)
    soup = BeautifulSoup(request.text, 'html.parser')

    movie_info = {'url': title_url}
    return movie_info


if __name__ == '__main__':
    print(f' \033[47m\033[30m\033[4m dataset-{date_now.day}-{date_now.month}-{date_now.year} \033[0m')

    if not os.path.exists('datasets'):
        os.mkdir('datasets')

    if os.path.exists('imdb_set.txt'):
        scraping_imdb()
        if int(time.time()) - os.path.getmtime('imdb_set.txt') >= 1209600:
            scraping_imdb()
    elif not os.path.exists('imdb_set.txt'):
        open('imdb_set.txt', 'w').close()
        scraping_imdb()

    pred = ['39150', '9882985', '1246399', '2846632', '1987018', '0108778', '5013056']
    rand = [random.randint(1000000, 10872600) for n in range(100)]
    # print(rand)

    for i in pred:
        if len(str(i)) < 7:
            movie_list = Scraping('0' * (7 - len(str(i))) + str(i))
        else:
            movie_list = Scraping(i)

        if movie_list['name'] == 'skip':
            pass
        else:
            with open(f'{date_now.day}-{date_now.month}-{date_now.year}-dataset.txt', 'w+', encoding='utf-8') as file:
                file.write(f'text\n')
