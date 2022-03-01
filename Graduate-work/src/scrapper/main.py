import requests
import string
import random
import re
import multiprocessing
from multiprocessing.dummy import Pool as ThreadPool
import threading
import time
import urllib3
import os
import datetime
from bs4 import BeautifulSoup
from selenium import webdriver

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
                imdb_file.write(f'{item["id"]}\t|\t{item["name"]}\t|\t{item["year"]}\t|\t{item["url"]}\n')


def scraping_imdb():
    pool = ThreadPool()

    counter = 40001
    # counter = 10001

    # t = time.time()
    imdb_set = []
    urls = []

    while counter <= 10872600:
        if len(str(counter)) < 7:
            for k in range(counter, counter + 10000, 1):
                urls.append(f'https://www.imdb.com/title/tt{str("0" * (7 - len(str(k))) + str(k))}/')
        else:
            for k in range(counter, counter + 10000, 1):
                urls.append(f'https://www.imdb.com/title/tt{str(k)}/')

        try:
            imdb_set = pool.map(collect_movies, urls)
        except (TimeoutError,
                urllib3.exceptions.NewConnectionError,
                urllib3.exceptions.MaxRetryError,
                requests.exceptions.ConnectionError) as e:
            print('\n\t\t\t\t\033[41m\033[30m\033[6m       SLEEP TIME       \033[0m\n')
            time.sleep(10)
            continue

        if int(len(imdb_set)) % 10000 == 0:
            print('\n\t\t\t\t\033[31m\033[40m\033[6m\033[4m       SAVING DATA       \033[0m\n')

            for item in imdb_set:
                if item['name'] == 'skip':
                    pass
                else:
                    with open(f'imdb_set.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{item["id"]}\t|\t{item["name"]}\t|\t{item["year"]}\t|\t{item["url"]}\n')

            imdb_set.clear()
            urls.clear()

        counter += 10000

    # while counter <= 10872600:
    #     if len(str(counter)) < 7:
    #         imdb_set.append(collect_movies(f'https://www.imdb.com/title/tt{str("0" * (7 - len(str(counter))) + str(counter))}/'))
    #     else:
    #         imdb_set.append(collect_movies(f'https://www.imdb.com/title/tt{str(counter)}/'))
    #
    #     if counter % 1 == 0:
    #         print('\n\t\t\t\t\033[31m\033[40m\033[6m\033[4m       SAVING DATA       \033[0m\n')
    #         saving(imdb_set)
    #         imdb_set.clear()
    #
    #     counter += 1

    # print('Elapsed {:.3f} secs'.format(time.time() - t))


def skip(arg):
    print(f'\t\t\033[31mskip\033[0m ({arg})')
    imdb_set = {'name': 'skip'}
    return imdb_set


def collect_movies(url):
    global collected_imdb_counter
    hour, minute, year, mpaa = '', '', '', ''

    # title_url = f'https://www.imdb.com/title/tt{str(movie_id)}/'
    session = requests.Session()
    request = session.get(url, headers=headers)
    soup = BeautifulSoup(request.content, 'lxml')


    success = soup.find('li', class_='ipc-inline-list__item')
    error_404_1 = soup.find('div', class_='error_code')
    error_404_2 = soup.find('div', class_='_error__QuoteBubbleText-sc-ql15x0-5')

    movie_id = url.replace('https://www.imdb.com/title/tt', '').replace('/', '')
    title = soup.find('h1', class_='TitleHeader__TitleText-sc-1wu6n3d-0')

    if title is not None:
        print(f'\033[33m {title.get_text()} \033[0m ({url}) '
              f'[0{movie_id} out of 10872600] \033[32m{collected_imdb_counter}\033[0m found')
    else:
        print(f'\033[31m No title \033[0m ({url}) '
              f'[0{movie_id} out of 10872600] \033[32m{collected_imdb_counter}\033[0m found')

    if error_404_1 is not None:
        if error_404_1.get_text() == '404':
            return skip('ERROR 404')

    if error_404_2 is not None:
        if error_404_2.get_text() == '404 Error':
            return skip('ERROR 404')

    if success is not None:
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

    # box_office_items = soup.find('ul', class_='ipc-metadata-list').find_all('li', class_='ipc-metadata-list__item BoxOffice__MetaDataListItemBoxOffice-sc-40s2pl-2')
    # print(box_office_items)
    # print(soup.find('div', id_='__next'))
    # for item in box_office_items:
    #     print(1, item)

    if year in ('', None):
        return skip('No year')

    if mpaa in ('', None):
        return skip('No mpaa')

    hour = hour[0].replace('h', '') if hour else 0
    minute = minute[0].replace('m', '') if minute else 0
    year = year[0]
    mpaa = mpaa[0] if mpaa else 'Not rated'

    duration = int(hour) * 60 + int(minute)
    if int(duration) <= 40:
        return skip(f'duration < 40 [{duration}min]')

    print(f'\t\t\033[32m{url} {name} {year} {mpaa} {duration}min\033[0m')
    imdb_set = {'url': url, 'id': movie_id, 'name': name, 'year': year}
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
