import datetime
import lxml
import os
import pandas
import re
import requests
import time
from bs4 import BeautifulSoup
from multiprocessing.dummy import Pool
from openpyxl import Workbook
from selenium import webdriver
from selenium.common.exceptions import NoSuchElementException
from selenium.common.exceptions import StaleElementReferenceException
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options

headers = {'accept': '*/*',
           'user-agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36'}

top_1000 = 'https://www.imdb.com/search/title/?groups=top_1000'
bottom_1000 = 'https://www.imdb.com/search/title/?groups=bottom_1000'


checked_counter = 0

open('./raw_data.xlsx', 'w').close()
open('./dataset.xlsx', 'w').close()
open('./datasetD1.xlsx', 'w').close()
open('./datasetD2.xlsx', 'w').close()


def scrapper(url):
    data = []
    start = 0
    iteration = 1

    while start < 1000:
        request = requests.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'html.parser')

        title_block = soup.find('div', class_='lister-list').find_all('div', class_='lister-item mode-advanced')
        for it in title_block:
            title = it.find('h3', class_='lister-item-header').find('a')
            title_id = title.get('href').replace('/title/tt', '').replace('/?ref_=adv_li_tt', '')
            data.append(title_id)
            print(
                f'{url.replace("https://www.imdb.com/search/title/?groups=", "").replace("&start=", "/").replace("&ref_=adv_nxt", "")}: '
                f'{it.find("span", class_="lister-item-index unbold text-primary").get_text()} {title_id}')

        if start + 50 <= len(data):
            if iteration == 1:
                url = url + f'&start={str(start + 50 + 1)}&ref_=adv_nxt'
            else:
                url = url.replace(f'&start={str(start + 1)}', f'&start={str(start + 50 + 1)}')
            iteration += 1
            start += 50
        else:
            break

    return data


def convert_date(child_id):
    data = []
    release_date = [0, 0, 0]
    date = None
    qty_most_common = 0
    month_format = {'January': 1,
                    'February': 2,
                    'March': 3,
                    'April': 4,
                    'May': 5,
                    'June': 6,
                    'July': 7,
                    'August': 8,
                    'September': 9,
                    'October': 10,
                    'November': 11,
                    'December': 12}
    url = f'https://www.imdb.com/title/tt{str(child_id)}/releaseinfo'
    session = requests.Session()
    request = session.get(url, headers=headers)
    soup = BeautifulSoup(request.content, 'lxml')

    dates_list = soup.find('table', class_='release-dates-table-test-only') \
        .find_all('tr', class_='ipl-zebra-list__item release-date-item')
    for item in dates_list:
        data.append(item.find('td', class_='release-date-item__date').get_text())

    set_data = set(data)

    for j in set_data:
        qty = data.count(j)
        if qty > qty_most_common:
            qty_most_common = qty
            date = j

    date = date.split(' ')

    day = date[0]
    if len(str(date[1])) > 3 and not date[1].isdigit():
        month = month_format[str(date[1])]
    else:
        release_date[0] = 0
        release_date[1] = 0
        return release_date
    year = date[2]

    temp_season = datetime.datetime(2020, int(month), int(day))
    if datetime.datetime(2019, 11, 20) < temp_season < datetime.datetime(2020, 2, 20):
        release_date[0] = 4
    elif datetime.datetime(2020, 2, 20) < temp_season < datetime.datetime(2020, 5, 20):
        release_date[0] = 1
    elif datetime.datetime(2020, 5, 20) < temp_season < datetime.datetime(2020, 8, 20):
        release_date[0] = 2
    elif datetime.datetime(2020, 8, 20) < temp_season < datetime.datetime(2020, 11, 20):
        release_date[0] = 3
    elif datetime.datetime(2020, 11, 20) < temp_season < datetime.datetime(2021, 2, 20):
        release_date[0] = 4
    else:
        release_date[0] = 0

    release_date[1] = datetime.datetime.strptime(f'{year}-{month}-{day}', '%Y-%m-%d').isoweekday()

    if datetime.datetime(2019, 12, 15) < temp_season < datetime.datetime(2020, 1, 15):
        release_date[2] = 1
    elif datetime.datetime(2020, 12, 15) < temp_season < datetime.datetime(2021, 1, 15):
        release_date[2] = 1
    elif datetime.datetime(2020, 6, 15) < temp_season < datetime.datetime(2020, 9, 10):
        release_date[2] = 1
    else:
        release_date[2] = 0

    return release_date


def take_person_score(child_id):
    score, temp = 0, 0
    total = [0, 0, 0]

    for item in child_id:
        url = f'https://www.imdb.com/name/nm{str(item)}/'
        session = requests.Session()
        request = session.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'lxml')

        temp = soup.find('a', attrs={'id': 'meterRank'})
        if temp is not None:
            temp = temp.get_text()
        else:
            return 0

        awards = soup.find('div', class_='article highlighted')
        if awards is not None:
            awards = awards.find_all('span', class_='awards-blurb')
            for span_a in awards:
                if awards is not None:
                    awards = span_a.get_text().replace('.', '').replace('\n', '')
                    if awards.find('Oscar') != -1 or awards[0].find('Oscars') != -1:
                        if awards.find('Nominated') != -1:
                            temp_aw = (awards.replace(' ', '')
                                       .replace('Nominated', '')
                                       .replace('for', '')
                                       .replace('Oscar', '')
                                       .replace('s', ''))
                            temp_aw = int(temp_aw) if str(temp_aw).isdigit() else 1
                            if temp_aw > 2:
                                total[0] += 1
                            else:
                                total[0] += 0
                        else:
                            temp_aw = (awards.replace(' ', '')
                                       .replace('Won', '')
                                       .replace('Oscar', '')
                                       .replace('s', ''))
                            total[0] += int(temp_aw) if str(temp_aw).isdigit() else 1
                    else:
                        total[0] += 0

                    if awards.find('wins') != -1:
                        total[1] = 1
                    else:
                        total[1] = 0

                    if awards.find('&') != -1:
                        awards = (awards.split('&')[0]
                                  .replace(' ', '')
                                  .replace('Another', '')
                                  .replace('wins', ''))
                    else:
                        awards = (awards.replace(' ', '')
                                  .replace('Another', '')
                                  .replace('wins', ''))

        if temp == 'SEE RANK':
            score += 10 / 2
        elif temp in ('Top 5000', 'Top 2500', 'Top 1000'):
            score += 20 / 2
        elif temp == 'Top 500':
            score += 30 / 2
        elif 50 < int(temp) <= 100:
            score += 40 / 2
        elif 0 < int(temp) <= 50:
            score += 50 / 2
        else:
            return 0

        score += int(awards) if str(awards).isdigit() else 1
            # score += ((100 - int(temp)) / 2) + 30 / 2

    num = len(child_id) if len(child_id) != 0 else 1
    total[2] = round(score / num)
    return total


def take_box_wiki(child_id):
    def is_number(v):
        try:
            float(v)
            return True
        except ValueError:
            return False

    def calculate(cal):
        reg = re.findall(r'\[\d*]', cal)
        temp = (cal.replace('$', '')
                .replace('£', '')
                .replace('€', '')
                .replace('₩', '')
                .replace('¥', '')
                .replace('₹', '')
                .replace('DEM', '')
                .replace('FRF', '')
                .replace('R', '')
                .replace('U', '')
                .replace(''.join(reg), '')
                .replace(' долл.', '')
                .replace(' долларов', '')
                .replace('  (в США)', '')
                .replace('.', '')
                .replace(' ', ''))
        temp = temp.replace(''.join(reg), '').replace('тыс', '*тыс').replace('млн', '*млн').replace('млрд', '*млрд')
        temp = temp.split('*')
        temp[0] = temp[0].replace(',', '.').split('–')[0]
        if len(temp) > 1:
            if len(temp[0]) > 4:
                return ''.join(temp[0]).replace(' ', '').replace('\xa0', '')
            else:
                temp[1] = temp[1].replace('тыс', '1000').replace('млн', '1000000').replace('млрд', '1000000000').replace(',', '.')
                if is_number(temp[0]) is True and is_number(temp[1]):
                    return round((float(temp[0]) * float(temp[1])))
                else:
                    return 0
        else:
            return 0

    budget = [0, 0, 0]
    search_result = ''

    try:
        driver.get(f'https://ru.wikipedia.org/w/index.php?search=IMDb%09ID+{str(child_id)}&ns0=1')
        time.sleep(2)
        search_result = driver.find_element(By.XPATH, '//div[@class="mw-search-result-heading"]//a')
        if search_result is not None:
            search_result = search_result.get_attribute('href')

        url = str(search_result)
        session = requests.Session()
        request = session.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'lxml')

        temp_budget = soup.find('span', attrs={'data-wikidata-property-id': 'P2130'})
        if temp_budget is not None:
            temp_budget = temp_budget.get_text()
            budget[0] = calculate(temp_budget)
        else:
            budget[0] = 0

        temp_box = soup.find('span', attrs={'data-wikidata-property-id': 'P2142'})
        if temp_box is not None:
            temp_box = temp_box.get_text()
            budget[1] = calculate(temp_box)
        else:
            budget[1] = 0

        franchise1 = soup.find('span', attrs={'data-wikidata-property-id': 'P155'})
        franchise2 = soup.find('span', attrs={'data-wikidata-property-id': 'P156'})
        if franchise1 is not None or franchise2 is not None:
            budget[2] = 1
        else:
            budget[2] = 0
    except (NoSuchElementException, StaleElementReferenceException):
        budget[0] = 0
        budget[1] = 0
        budget[2] = 0
        return budget
        pass

    return budget


def parser(title_id):
    global checked_counter
    directors, writers, stars = [], [], []
    directors_awards, writers_awards, stars_awards = '', '', ''
    year, mpaa, duration, genre, season, popularity, budget, boxoffice, franchise = '', '', '', '', '', '', '', '', ''
    hour, minute, day, age_limit, oscars, holiday = '', '', '', '', '', ''

    agelimit_format = {'PG-13': 3, 'NC-17': 5, 'PG': 2, 'R': 4, 'G': 1, 'X': 6, '0': 1, '6': 2, '12': 3, '14': 4, '16': 5, '18': 6}
    genre_format = {'Action': 1, 'Adventure': 2, 'Drama': 3, 'Comedy': 4, 'Crime': 5,
                    'Romance': 6, 'Mystery': 7, 'Horror': 8, 'History': 9, 'Western': 10,
                    'Music': 11, 'Biography': 12, 'Musical': 13, 'Film-Noir': 14, 'Animation': 15,
                    'Fantasy': 16, 'Sci-Fi': 17, 'Thriller': 18, 'Family': 19, 'Short': 20,
                    'Sport': 21, 'War': 22, 'Reality-TV': 23, 'Game-Show': 24, 'Documentary': 25,
                    'Talk-Show': 26, 'News': 27, 'Adult': 28}

    url = f'https://www.imdb.com/title/tt{str(title_id)}/'
    session = requests.Session()
    request = session.get(url, headers=headers)
    soup = BeautifulSoup(request.content, 'lxml')

    boxoffice_wiki = take_box_wiki(title_id)

    success = soup.find('li', class_='ipc-inline-list__item')
    title = soup.find('h1', attrs={'data-testid': 'hero-title-block__title'})
    if title is not None:
        title = title.get_text()

    if success is not None:
        items_list = soup.find('ul', class_='ipc-inline-list').find_all('li', class_='ipc-inline-list__item')
        for item in items_list:
            hour = re.findall(r'\b\w{1,2}[h]\b', item.get_text()) if not hour else hour
            minute = re.findall(r'\b\w{1,2}[m]\b', item.get_text()) if not minute else minute
            if item.find('a', class_='ipc-link') is not None:
                year = re.findall(r'\d{4}', item.get_text()) if not year else year
                mpaa = re.findall(r'\b([0-9]{1,2}\+)|(PG-13|NC-17|PG|R|G)|(TV-(Y7|Y|PG|G|14|MA))\b',
                                  item.get_text()) if not mpaa else mpaa

        hour = hour[0].replace('h', '') if hour else 0
        minute = minute[0].replace('m', '') if minute else 0
        duration = int(hour) * 60 + int(minute)

        while type(mpaa) not in (str, int):
            mpaa = mpaa[0] if mpaa else 0
        if re.search(r'\b([0-9]{1,2}\+)|(PG-13|NC-17|PG|R|G)', str(mpaa)) is not None:
            if str(mpaa).replace('+', '') in agelimit_format:
                age_limit = agelimit_format[str(mpaa).replace('+', '')]
            else:
                age_limit = 0
        else:
            age_limit = 0

        genre = soup.find('div', attrs={'data-testid': 'genres'})
        if genre is not None:
            genre = genre.find('span', class_='ipc-chip__text')
            if genre is not None:
                genre = genre_format[genre.get_text()]

        release_date = convert_date(title_id)
        season = release_date[0]
        day = release_date[1]
        holiday = release_date[2]

        temp = 1
        credits_list = soup.find('div', attrs={'data-testid': 'title-pc-expanded-section'})
        if credits_list is not None:
            credits_list = credits_list.find_all('li', attrs={'data-testid': 'title-pc-principal-credit'})
        else:
            credits_list = soup.find('div', attrs={'data-testid': 'title-pc-wide-screen'})
            if credits_list is not None:
                credits_list = credits_list.find_all('li', attrs={'data-testid': 'title-pc-principal-credit'})
            else:
                title_id = 0

        for credits_item in credits_list:
            list_items = credits_item.find_all('li', class_='ipc-inline-list__item')
            for li in list_items:
                if li.find('a', class_='ipc-metadata-list-item__list-content-item') is not None:
                    if temp == 1:
                        directors.append(li.find('a', class_='ipc-metadata-list-item__list-content-item')
                                         .get('href')
                                         .replace('/name/nm', '')
                                         .replace('/?ref_=tt_ov_dr', ''))
                    elif temp == 3:
                        writers.append(li.find('a', class_='ipc-metadata-list-item__list-content-item')
                                       .get('href')
                                       .replace('/name/nm', '')
                                       .replace('/?ref_=tt_ov_wr', ''))
                    else:
                        stars.append(li.find('a', class_='ipc-metadata-list-item__list-content-item')
                                     .get('href')
                                     .replace('/name/nm', '')
                                     .replace('/?ref_=tt_ov_st', ''))
            temp += 1

        directors = take_person_score(directors)
        writers = take_person_score(writers)
        stars = take_person_score(stars)

        oscars = directors[0] + writers[0] + stars[0]

        directors_awards = directors[1]
        writers_awards = writers[1]
        stars_awards = stars[1]

        directors = directors[2]
        writers = writers[2]
        stars = stars[2]

        popularity = soup.find('div', attrs={'data-testid': 'hero-rating-bar__popularity__score'})
        if popularity is not None:
            popularity = popularity.get_text().replace(',', '')
        else:
            popularity = 5000

        boxoffice_section = soup.find('div', attrs={'data-testid': 'title-boxoffice-section'})
        if boxoffice_section is not None:
            budget = boxoffice_section.find('li', attrs={'data-testid': 'title-boxoffice-budget'})
            if budget is not None:
                budget = budget.find('span', class_='ipc-metadata-list-item__list-content-item')
                if budget is not None:
                    budget = (budget.get_text()
                              .replace('$', '')
                              .replace('£', '')
                              .replace('€', '')
                              .replace('₩', '')
                              .replace('¥', '')
                              .replace('₹', '')
                              .replace('DEM', '')
                              .replace('FRF', '')
                              .replace('R', '')
                              .replace('U', '')
                              .replace(',', '')
                              .replace('.', '')
                              .replace('~', '')
                              .replace('\xa0', '')
                              .replace(' (estimated)', '')
                              .replace(' долл.', ''))
                else:
                    budget = boxoffice_wiki[0]
            else:
                budget = boxoffice_wiki[0]

            boxoffice = boxoffice_section.find('li', attrs={'data-testid': 'title-boxoffice-cumulativeworldwidegross'})
            if boxoffice is not None:
                boxoffice = boxoffice.find('span', class_='ipc-metadata-list-item__list-content-item')
                if boxoffice is not None:
                    boxoffice = (boxoffice.get_text()
                                 .replace('$', '')
                                 .replace('£', '')
                                 .replace('€', '')
                                 .replace('₩', '')
                                 .replace('¥', '')
                                 .replace('₹', '')
                                 .replace('DEM', '')
                                 .replace('FRF', '')
                                 .replace('R', '')
                                 .replace('U', '')
                                 .replace(',', '')
                                 .replace('.', '')
                                 .replace('~', '')
                                 .replace('\xa0', '')
                                 .replace(' (estimated)', '')
                                 .replace(' долл.', ''))
                else:
                    boxoffice = boxoffice_wiki[1]
            else:
                boxoffice = boxoffice_wiki[1]
    else:
        title_id = 0

    franchise = boxoffice_wiki[2]

    print(checked_counter, url)
    checked_counter += 1

    year = int(year[0]) if year else 0
    age_limit = int(age_limit) if age_limit else 0
    duration = int(duration) if duration else 0
    season = int(season) if season else 0
    day = int(day) if str(day).isdigit() else 0
    directors = int(directors) if directors else 0
    writers = int(writers) if writers else 0
    stars = int(stars) if stars else 0
    genre = int(genre) if genre else 0
    franchise = int(franchise) if franchise else 0
    popularity = int(popularity) if popularity else 5000
    budget = int(budget) if str(budget).isdigit() else 0
    boxoffice = int(boxoffice) if str(boxoffice).isdigit() else 0

    if int(duration) <= 40:
        title_id = 0

    if budget in (0, None, '') or boxoffice in (0, None, ''):
        title_id = 0
    if budget < 100000:
        title_id = 0

    if boxoffice < 10000:
        title_id = 0

    if boxoffice >= (budget * 2):
        profitable = 100
    elif boxoffice >= (budget + (budget / 2)):
        profitable = 75
    elif boxoffice >= budget:
        profitable = 50
    else:
        profitable = 25

    print(title_id, title, url)
    data = {'id': title_id,
            'name': title,
            'url': url,
            'year': year,
            'age-limit': age_limit,
            'duration': duration,
            'release-season': season,
            'release-day': day,
            'holiday': holiday,
            'directors': directors,
            'directors-awards': directors_awards,
            'writers': writers,
            'writers-awards': writers_awards,
            'stars': stars,
            'stars-awards': stars_awards,
            'oscars': oscars,
            'genre': genre,
            'franchise': franchise,
            'imdb-popularity': popularity,
            'budget': budget,
            'box-office': boxoffice,
            'profitable': profitable}
    print(data)
    return data


def del_copy(obj):
    n = []
    for i in obj:
        if i not in n:
            n.append(i)
    return n


def write_file(dset, title, ver, state):
    sample_frow = (f'Возрастное ограничение (1- 0+, 2- 6+ и тд);'
                   f'Длительность фильма в минутах;'
                   f'Сезон выхода (0-зима, 1-весна и тд);'
                   f'День недели выхода (1-понедельник и тд);'
                   f'Вышел ли фильм в период высокой посещаемости кинотеатров (0-нет, 1-да);'
                   f'Сумма рейтингов режиссеров (рейтинг топ 5000 + количество наград);'
                   f'Сумма рейтингов сценаристов (рейтинг топ 5000 + количество наград);'
                   f'Сумма рейтингов 3-х главных звезд (рейтинг топ 5000 + количество наград);'
                   f'Имеют ли режиссеры награды (0-нет, 1-да);'
                   f'Имеют ли сценаристы награды (0-нет, 1-да);'
                   f'Имеют ли 3-х главные звезды награды (0-нет, 1-да);'
                   f'Количество оскаров у съемочной группы;'
                   f'Основной жанр фильма (1-Action, 2-Adventure, 3-Drama и тд);'
                   f'Является ли фильм частью франшизы;'
                   f'Условная популярность на сайте imdb;'
                   f'Бюджет фильма;')
    files = ['dataset_0-5',
             'dataset_5-10',
             'dataset_10-15',
             'dataset_15-20',
             'dataset_20-30',
             'dataset_30-50',
             'dataset_50-100',
             'dataset_100-inf']

    if title == 'D1':
        header = f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;X12;X13;X14;X15;X16;D1\n'
        frow = (f'{sample_frow}'
                f'Кассовые сборы фильма;\n')
    elif title == 'D2':
        header = f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;X12;X13;X14;X15;X16;D2\n'
        frow = (f'{sample_frow}'
                f'Окупаемость фильма (25-не окупился, 50-собрал бюджет, 75-частичная окупаемость, 100-окупился)\n')
    else:
        header = f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;X12;X13;X14;X15;X16;D1;D2\n'
        frow = (f'{sample_frow}'
                f'Кассовые сборы фильма;'
                f'Окупаемость фильма (25-не окупился, 50-собрал бюджет, 75-частичная окупаемость, 100-окупился)\n')

    if state == 1:
        with open(f'./dataset_all.txt', 'w', encoding='utf-8') as f:
            f.write(header)
            f.write(frow)
        for ictr in files:
            with open(f'./{ictr}.txt', 'w', encoding='utf-8') as f:
                f.write(header)
            with open(f'./{ictr}_test.txt', 'w', encoding='utf-8') as f:
                f.write(header)
            with open(f'./{ictr}_check.txt', 'w', encoding='utf-8') as f:
                f.write(header)

    if ver == 'D1':
        with open(f'./dataset_{title}.txt', 'a', encoding='utf-8') as f:
            for span in dset:
                f.write(f'{span["age-limit"]};{span["duration"]};'
                        f'{span["release-season"]};{span["release-day"]};{span["holiday"]};'
                        f'{span["directors"]};{span["writers"]};{span["stars"]};'
                        f'{span["directors-awards"]};{span["writers-awards"]};{span["stars-awards"]};'
                        f'{span["oscars"]};{span["genre"]};{span["franchise"]};'
                        f'{span["imdb-popularity"]};{span["budget"]};'
                        f'{span["box-office"]}\n')
    elif ver == 'D2':
        with open(f'./dataset_{title}.txt', 'a', encoding='utf-8') as f:
            for span in dset:
                f.write(f'{span["age-limit"]};{span["duration"]};'
                        f'{span["release-season"]};{span["release-day"]};{span["holiday"]};'
                        f'{span["directors"]};{span["writers"]};{span["stars"]};'
                        f'{span["directors-awards"]};{span["writers-awards"]};{span["stars-awards"]};'
                        f'{span["oscars"]};{span["genre"]};{span["franchise"]};'
                        f'{span["imdb-popularity"]};{span["budget"]};'
                        f'{span["profitable"]}\n')
    else:
        with open(f'./dataset_{title}.txt', 'a', encoding='utf-8') as f:
            for span in dset:
                f.write(f'{span["age-limit"]};{span["duration"]};'
                        f'{span["release-season"]};{span["release-day"]};{span["holiday"]};'
                        f'{span["directors"]};{span["writers"]};{span["stars"]};'
                        f'{span["directors-awards"]};{span["writers-awards"]};{span["stars-awards"]};'
                        f'{span["oscars"]};{span["genre"]};{span["franchise"]};'
                        f'{span["imdb-popularity"]};{span["budget"]};'
                        f'{span["box-office"]};{span["profitable"]}\n')

    for ctr in files:
        lrow_counter = 0
        temp_dataset, dataset_test, dataset_check = [], [], []
        with open(f'./{ctr}.txt', 'r', encoding='utf-8') as lfile:
            for lrow in lfile:
                if lrow_counter != 0:
                    if lrow_counter % 20 == 0:
                        dataset_test.append(lrow)
                    elif lrow_counter % 47 == 0:
                        dataset_check.append(lrow)
                    else:
                        temp_dataset.append(lrow)
                lrow_counter += 1
        with open(f'./{ctr}_test.txt', 'a', encoding='utf-8') as mfile:
            for mrow in dataset_test:
                mfile.write(mrow)
        with open(f'./{ctr}_check.txt', 'a', encoding='utf-8') as nfile:
            for nrow in dataset_check:
                nfile.write(nrow)
        open(f'./{ctr}.txt', 'w').close()
        with open(f'./{ctr}.txt', 'a', encoding='utf-8') as ofile:
            ofile.write(header)
            for orow in temp_dataset:
                ofile.write(orow)

    if state == 2:
        f.close()

        group0 = pandas.read_csv(f'./dataset_all.txt', sep=';')
        group1 = pandas.read_csv(f'./dataset_0-5.txt', sep=';')
        group1_test = pandas.read_csv(f'./dataset_0-5_test.txt', sep=';')
        group1_check = pandas.read_csv(f'./dataset_0-5_check.txt', sep=';')
        group2 = pandas.read_csv(f'./dataset_5-10.txt', sep=';')
        group2_test = pandas.read_csv(f'./dataset_5-10_test.txt', sep=';')
        group2_check = pandas.read_csv(f'./dataset_5-10_check.txt', sep=';')
        group3 = pandas.read_csv(f'./dataset_10-15.txt', sep=';')
        group3_test = pandas.read_csv(f'./dataset_10-15_test.txt', sep=';')
        group3_check = pandas.read_csv(f'./dataset_10-15_check.txt', sep=';')
        group4 = pandas.read_csv(f'./dataset_15-20.txt', sep=';')
        group4_test = pandas.read_csv(f'./dataset_15-20_test.txt', sep=';')
        group4_check = pandas.read_csv(f'./dataset_15-20_check.txt', sep=';')
        group5 = pandas.read_csv(f'./dataset_20-30.txt', sep=';')
        group5_test = pandas.read_csv(f'./dataset_20-30_test.txt', sep=';')
        group5_check = pandas.read_csv(f'./dataset_20-30_check.txt', sep=';')
        group6 = pandas.read_csv(f'./dataset_30-50.txt', sep=';')
        group6_test = pandas.read_csv(f'./dataset_30-50_test.txt', sep=';')
        group6_check = pandas.read_csv(f'./dataset_30-50_check.txt', sep=';')
        group7 = pandas.read_csv(f'./dataset_50-100.txt', sep=';')
        group7_test = pandas.read_csv(f'./dataset_50-100_test.txt', sep=';')
        group7_check = pandas.read_csv(f'./dataset_50-100_check.txt', sep=';')
        group8 = pandas.read_csv(f'./dataset_100-inf.txt', sep=';')
        group8_test = pandas.read_csv(f'./dataset_100-inf_test.txt', sep=';')
        group8_check = pandas.read_csv(f'./dataset_100-inf_check.txt', sep=';')

        sheets = {'DATA': group0,
                  'DATA_0-5': group1, 'TEST_0-5': group1_test, 'CHECK_0-5': group1_check,
                  'DATA_5-10': group2, 'TEST_5-10': group2_test, 'CHECK_5-10': group2_check,
                  'DATA_10-15': group3, 'TEST_10-15': group3_test, 'CHECK_10-15': group3_check,
                  'DATA_15-20': group4, 'TEST_15-20': group4_test, 'CHECK_15-20': group4_check,
                  'DATA_20-30': group5, 'TEST_20-30': group5_test, 'CHECK_20-30': group5_check,
                  'DATA_30-50': group6, 'TEST_30-50': group6_test, 'CHECK_30-50': group6_check,
                  'DATA_50-100': group7, 'TEST_50-100': group7_test, 'CHECK_50-100': group7_check,
                  'DATA_100-inf': group8, 'TEST_100-inf': group8_test, 'CHECK_100-inf': group8_check}

        writer = pandas.ExcelWriter(f'./dataset{ver}.xlsx', engine='openpyxl')

        for sheet_name in sheets.keys():
            sheets[sheet_name].to_excel(writer, sheet_name=sheet_name, engine='openpyxl', index=False)

        writer.save()

        os.remove('./dataset_all.txt')
        for delete in files:
            if os.path.exists(f'./{delete}.txt'):
                os.remove(f'./{delete}.txt')
            if os.path.exists(f'./{delete}_test.txt'):
                os.remove(f'./{delete}_test.txt')
            if os.path.exists(f'./{delete}_check.txt'):
                os.remove(f'./{delete}_check.txt')


def first_part():

    raw_dataset.append(scrapper(top_1000))
    raw_dataset.append(scrapper(bottom_1000))

    for k in raw_dataset[0]:
        ids.append(k)

    for k in raw_dataset[1]:
        ids.append(k)

    print(ids)
    with open(f'./ids.txt', 'w+', encoding='utf-8') as id_file:
        for item in ids:
            id_file.write(item + '\n')


def second_part():
    counter = 0
    open(f'./tempdata.txt', 'w').close()

    with open(f'./raw_data.txt', 'w', encoding='utf-8') as set_file:
        set_file.write(f'url;id;name;year;duration;imdb-popularity;budget;box-office\n')

    while counter <= 2000:
        output_dataset, temp_dataset, temp_ids = [], [], []
        print(1, counter)

        with open(f'./ids.txt', 'r', encoding='utf-8') as temp_file:
            for index, row_idx in enumerate(temp_file, 1):
                if counter <= index < counter + 100:
                    temp_ids.append(row_idx.replace('\n', ''))

        try:
            temp_dataset = pool.map(parser, temp_ids)
        except (TimeoutError, requests.exceptions.ConnectionError) as e:
            pass

        for temp_row in temp_dataset:
            if temp_row["id"] in (0, '0', '', None):
                pass
            else:
                output_dataset.append(temp_row)

        with open(f'./tempdata.txt', 'a', encoding='utf-8') as temp_data:
            for subrow in output_dataset:
                temp_data.write(f'{subrow}\n')

        counter += 100

    temp_file.close()
    temp_data.close()


def third_part():
    dataset = []

    with open(f'./tempdata.txt', 'r', encoding='utf-8') as temp_fdata:
        for irow in temp_fdata:
            dataset.append(eval(irow.replace('\n', '')))

    dataset = del_copy(dataset)

    with open(f'./raw_data.txt', 'a', encoding='utf-8') as j_file:
        for jrow in dataset:
            j_file.write(f'{jrow["url"]};{jrow["id"]};'
                         f'{jrow["name"]};{jrow["year"]};{jrow["duration"]};'
                         f'{jrow["imdb-popularity"]};{jrow["budget"]};{jrow["box-office"]}\n')

    dataset0_5, dataset5_10, dataset10_15, dataset15_20 = [], [], [], []
    dataset20_30, dataset30_50, dataset50_100, dataset100_inf = [], [], [], []

    for krow in dataset:
        if krow["budget"] < 5000000:
            dataset0_5.append(krow)
        if 5000001 < krow["budget"] < 10000000:
            dataset5_10.append(krow)
        if 10000001 < krow["budget"] < 15000000:
            dataset10_15.append(krow)
        if 15000001 < krow["budget"] < 20000000:
            dataset15_20.append(krow)
        if 20000001 < krow["budget"] < 30000000:
            dataset20_30.append(krow)
        if 30000001 < krow["budget"] < 50000000:
            dataset30_50.append(krow)
        if 50000000 < krow["budget"] < 100000000:
            dataset50_100.append(krow)
        if 100000000 < krow["budget"]:
            dataset100_inf.append(krow)

    write_file(dataset, 'all', 'D1', 1)
    write_file(dataset0_5, '0-5', 'D1', 0)
    write_file(dataset5_10, '5-10', 'D1', 0)
    write_file(dataset10_15, '10-15', 'D1', 0)
    write_file(dataset15_20, '15-20', 'D1', 0)
    write_file(dataset20_30, '20-30', 'D1', 0)
    write_file(dataset30_50, '30-50', 'D1', 0)
    write_file(dataset50_100, '50-100', 'D1', 0)
    write_file(dataset100_inf, '100-inf', 'D1', 2)

    write_file(dataset, 'all', 'D2', 1)
    write_file(dataset0_5, '0-5', 'D2', 0)
    write_file(dataset5_10, '5-10', 'D2', 0)
    write_file(dataset10_15, '10-15', 'D2', 0)
    write_file(dataset15_20, '15-20', 'D2', 0)
    write_file(dataset20_30, '20-30', 'D2', 0)
    write_file(dataset30_50, '30-50', 'D2', 0)
    write_file(dataset50_100, '50-100', 'D2', 0)
    write_file(dataset100_inf, '100-inf', 'D2', 2)

    write_file(dataset, 'all', '', 1)
    write_file(dataset0_5, '0-5', '', 0)
    write_file(dataset5_10, '5-10', '', 0)
    write_file(dataset10_15, '10-15', '', 0)
    write_file(dataset15_20, '15-20', '', 0)
    write_file(dataset20_30, '20-30', '', 0)
    write_file(dataset30_50, '30-50', '', 0)
    write_file(dataset50_100, '50-100', '', 0)
    write_file(dataset100_inf, '100-inf', '', 2)

    temp_fdata.close()
    j_file.close()

    df0 = pandas.read_csv('./raw_data.txt', sep=';')
    df0.to_excel('./raw_data.xlsx', 'DATA', engine='openpyxl', index=False)

    os.remove('./raw_data.txt')


def test():
    # for item_id in ('0468569'): #ids:'10366460',
    test_set = parser('0468569')
    print(f'{test_set["age-limit"]};{test_set["duration"]};'
          f'{test_set["release-season"]};{test_set["release-day"]};{test_set["holiday"]};'
          f'{test_set["directors"]};{test_set["writers"]};{test_set["stars"]};'
          f'{test_set["directors-awards"]};{test_set["writers-awards"]};{test_set["stars-awards"]};'
          f'{test_set["oscars"]};'
          f'{test_set["genre"]};{test_set["franchise"]};{test_set["imdb-popularity"]};'
          f'{test_set["budget"]};{test_set["profitable"]}\n')


if __name__ == '__main__':
    pool = Pool()
    chrome_options = Options()
    chrome_options.add_argument("--disable-blink-features=AutomationControlled")
    # chrome_options.add_argument(r"user-data-dir=C:\\Users\\" + WINUSER + "\\AppData\\Local\\Google\\Chrome\\User Data")
    chrome_options.add_argument("--headless")
    driver = webdriver.Chrome(options=chrome_options)

    raw_dataset = []
    ids = []

    # test()
    # first_part()
    # second_part()
    third_part()
