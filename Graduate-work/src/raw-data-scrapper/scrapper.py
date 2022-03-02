import requests
import string
import random
import re
import multiprocessing
from multiprocessing.dummy import Pool
import threading
import time
import lxml
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
open('info.txt', 'w').close()


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


def skip(arg):
    print(f'\t\t\033[31mskip\033[0m ({arg})')
    callback = {'name': 'skip'}
    return callback


def collect_movies(url):
    global collected_imdb_counter, checked_imdb_counter
    name, year, mpaa, duration = '', '', '', ''
    hour, minute = '', ''

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
              f'[0{movie_id} out of 10872600] '
              f'\033[32m{collected_imdb_counter}\033[0m found ({checked_imdb_counter}/5000)')
    else:
        print(f'\033[31m No title \033[0m ({url}) '
              f'[0{movie_id} out of 10872600] '
              f'\033[32m{collected_imdb_counter}\033[0m found ({checked_imdb_counter}/5000)')

    checked_imdb_counter = checked_imdb_counter + 1

    if error_404_1 is not None:
        if error_404_1.get_text() == '404':
            print(f'\t\t\033[31mempty\033[0m (ERROR)')
            data = {'var': 'empty',
                    'id': movie_id,
                    'reason': 'error_404'}
            return data

    if error_404_2 is not None:
        if error_404_2.get_text() == '404 Error':
            print(f'\t\t\033[31mempty\033[0m (ERROR)')
            data = {'var': 'empty',
                    'id': movie_id,
                    'reason': 'error_404'}
            return data

    if success is not None:
        name = soup.find('div', class_='OriginalTitle__OriginalTitleText-sc-jz9bzr-0')
        if name is None:
            name = title.get_text()
        else:
            name = soup.find('div', class_='OriginalTitle__OriginalTitleText-sc-jz9bzr-0').get_text().replace(
                'Original title: ', '')

        items_list = soup.find('ul', class_='ipc-inline-list').find_all('li', class_='ipc-inline-list__item')
        for item in items_list:
            hour = re.findall(r'\b\w{1,2}[h]\b', item.get_text()) if not hour else hour
            minute = re.findall(r'\b\w{1,2}[m]\b', item.get_text()) if not minute else minute
            if item.find('a', class_='ipc-link') is not None:
                year = re.findall(r'\d{4}', item.get_text()) if not year else year
                mpaa = re.findall(r'\b([0-9]{1,2}\+)|(PG-13|NC-17|PG|R|G)|(TV-(Y7|Y|PG|G|14|MA))\b',
                                  item.get_text()) if not mpaa else mpaa

        if year in ('', None):
            print(f'\t\t\033[31mempty\033[0m (ERROR)')
            data = {'var': 'unsorted',
                    'id': movie_id,
                    'reason': 'no_year'}
            return data

        hour = hour[0].replace('h', '') if hour else 0
        minute = minute[0].replace('m', '') if minute else 0
        year = year[0] if year else 'No year'
        mpaa = mpaa[0] if mpaa else 'Not rated'

        duration = int(hour) * 60 + int(minute)

        if success.get_text() in ('TV Series', 'TV Mini Series'):
            print(f'\t\t\033[32mTV Series: {url} {name} {year} {mpaa} {duration}min\033[0m')
            data = {'var': 'tvseries',
                    'id': movie_id,
                    'name': name,
                    'year': year}
            return data
        elif success.get_text() in 'Video':
            print(f'\t\t\033[32mVideo: {url} {name} {year} {mpaa} {duration}min\033[0m')
            data = {'var': 'video',
                    'id': movie_id,
                    'name': name,
                    'year': year}
            return data
        elif success.get_text() in 'TV Movie':
            print(f'\t\t\033[32mTV Movie: {url} {name} {year} {mpaa} {duration}min\033[0m')
            data = {'var': 'tvmovie',
                    'id': movie_id,
                    'name': name,
                    'year': year}
            return data
        elif success.get_text() in 'TV Special':
            print(f'\t\t\033[32mTV Special: {url} {name} {year} {mpaa} {duration}min\033[0m')
            data = {'var': 'tvspecial',
                    'id': movie_id,
                    'name': name,
                    'year': year}
            return data
        elif success.get_text()[:7] == 'Episode':
            parent = soup.find('div', class_='TitleBlock__SeriesParentLinkWrapper-sc-1nlhx7j-3 itQvtY').find('a', class_='ipc-link')
            if parent is not None:
                parentserial = parent.get_text()
                parentserial_link = parent.get('href').replace('/title/tt', '').replace('/?ref_=tt_ov_inf', '')
                print(f'\t\t\033[32mEpisode: {url} {name} {year} {parentserial}min\033[0m')
                data = {'var': 'episode',
                        'id': movie_id,
                        'name': name,
                        'episode': success.get_text(),
                        'parentserial': parentserial,
                        'parentserial_id': parentserial_link}
                return data
        else:
            if int(duration) <= 40:
                print(f'\t\t\033[32mShort: {url} {name} {year} {mpaa} {duration}min < 40min\033[0m')
                data = {'var': 'short',
                        'url': url,
                        'id': movie_id,
                        'name': name,
                        'year': year}
                return data
            else:
                print(f'\t\t\033[32mFilm: {url} {name} {year} {mpaa} {duration}min\033[0m')
                data = {'var': 'film',
                        'url': url,
                        'id': movie_id,
                        'name': name,
                        'year': year}
                collected_imdb_counter = collected_imdb_counter + 1
                return data
    elif success is None:
        print(f'\t\t\033[31mempty\033[0m (ERROR)')
        data = {'var': 'unsorted',
                'id': movie_id,
                'reason': 'no_data'}
        return data
    else:
        print(f'\t\t\033[31mempty\033[0m (No data)')
        data = {'var': 'unsorted',
                'id': movie_id,
                'reason': 'no_data'}
        return data


if __name__ == '__main__':
    checked_imdb_counter = 0
    pool = Pool()

    counter = 5000
    imdb_set = []
    urls = []

    print(f' \033[47m\033[30m\033[4m dataset-{date_now.day}-{date_now.month}-{date_now.year} \033[0m')
    with open(f'info.txt', 'a', encoding='utf-8') as info_file:
        info_file.write(f'dataset-{date_now.day}-{date_now.month}-{date_now.year} error sets:\n')

    if not os.path.exists('datasets'):
        os.mkdir('datasets')

    if not os.path.exists('empty_imdb_raw-data.txt'):
        open('empty_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/film_imdb_raw-data.txt'):
        open('datasets/film_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/shorts_imdb_raw-data.txt'):
        open('datasets/shorts_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/tvmovie_imdb_raw-data.txt'):
        open('datasets/tvmovie_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/tvspecial_imdb_raw-data.txt'):
        open('datasets/tvspecial_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/tvseries_imdb_raw-data.txt'):
        open('datasets/tvseries_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/episodes_imdb_raw-data.txt'):
        open('datasets/episodes_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/videos_imdb_raw-data.txt'):
        open('datasets/videos_imdb_raw-data.txt', 'w').close()

    if not os.path.exists('datasets/unsorted_imdb_raw-data.txt'):
        open('datasets/unsorted_imdb_raw-data.txt', 'w').close()

    # t = time.time()
    while counter <= 10872600:
        if len(str(counter)) < 7:
            for k in range(counter, counter + 5000, 1):
                urls.append(f'https://www.imdb.com/title/tt{str("0" * (7 - len(str(k))) + str(k))}/')
        else:
            for k in range(counter, counter + 5000, 1):
                urls.append(f'https://www.imdb.com/title/tt{str(k)}/')

        try:
            imdb_set = pool.map(collect_movies, urls)
        except (TimeoutError,
                urllib3.exceptions.NewConnectionError,
                urllib3.exceptions.MaxRetryError,
                requests.exceptions.ConnectionError) as e:
            print('\n\t\t\t\t\033[41m\033[30m\033[6m       SLEEP TIME       \033[0m\n')
            with open(f'info.txt', 'a', encoding='utf-8') as info_file:
                info_file.write(f'{counter}\n')
            time.sleep(30)
            pass

        if int(len(imdb_set)) % 5000 == 0:
            print('\n\t\t\t\t\033[31m\033[40m\033[6m\033[4m       SAVING DATA       \033[0m\n')

            for row in imdb_set:
                if row['var'] == 'empty':
                    with open(f'empty_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["reason"]}\n')
                elif row['var'] == 'film':
                    with open(f'datasets/film_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["name"]}   |   {row["year"]}   |   {row["url"]}\n')
                elif row['var'] == 'short':
                    with open(f'datasets/shorts_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["name"]}   |   {row["year"]}   |   {row["url"]}\n')
                elif row['var'] == 'tvmovie':
                    with open(f'datasets/tvmovie_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["name"]}   |   {row["year"]}   |   {row["url"]}\n')
                elif row['var'] == 'tvspecial':
                    with open(f'datasets/tvspecial_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["name"]}   |   {row["year"]}   |   {row["url"]}\n')
                elif row['var'] == 'tvserial':
                    with open(f'datasets/tvseries_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["name"]}   |   {row["year"]}   |   {row["url"]}\n')
                elif row['var'] == 'episode':
                    with open(f'datasets/episodes_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["name"]}   |   {row["episode"]}   |   {row["parentserial"]}   |   {row["parentserial_id"]}\n')
                elif row['var'] == 'video':
                    with open(f'datasets/videos_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["name"]}   |   {row["year"]}   |   {row["url"]}\n')
                elif row['var'] == 'unsorted':
                    with open(f'datasets/unsorted_imdb_raw-data.txt', 'a', encoding='utf-8') as imdb_file:
                        imdb_file.write(f'{row["id"]}   |   {row["reason"]}\n')
                else:
                    pass

            imdb_set.clear()
            urls.clear()
            checked_imdb_counter = 0

        counter += 5000

    imdb_file.close()
    info_file.close()

    # print('Elapsed {:.3f} secs'.format(time.time() - t))
