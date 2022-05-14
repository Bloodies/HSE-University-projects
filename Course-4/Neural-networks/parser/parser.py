import datetime
import lxml
import numpy
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

files = ['all',
         '0-10',
         '10-40',
         '40-inf']


# region Функция сбора первичных ссылок
def scrapper(url):
    data = []
    start = 0
    iteration = 1

    while start < 1000:
        request = requests.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'html.parser')

        # находим список фильмов на странице
        title_block = soup.find('div', class_='lister-list').find_all('div', class_='lister-item mode-advanced')
        for it in title_block:
            title = it.find('h3', class_='lister-item-header').find('a')
            title_id = title.get('href').replace('/title/tt', '').replace('/?ref_=adv_li_tt', '')
            data.append(title_id)  # записываем id фильма в список
            print(
                f'{url.replace("https://www.imdb.com/search/title/?groups=", "").replace("&start=", "/").replace("&ref_=adv_nxt", "")}: '
                f'{it.find("span", class_="lister-item-index unbold text-primary").get_text()} {title_id}')

        if start + 50 <= len(data):  # переходим на след страницу
            if iteration == 1:
                url = url + f'&start={str(start + 50 + 1)}&ref_=adv_nxt'
            else:
                url = url.replace(f'&start={str(start + 1)}', f'&start={str(start + 50 + 1)}')
            iteration += 1
            start += 50
        else:
            break

    return data  # возвращаем список id фильмов
# endregion


# region Функция конвертации даты в сезон выхода и нагруженности на кинозалы
def convert_date(child_id):
    data = []
    release_date = [0, 0, 0]
    date = None
    qty_most_common = 0
    month_format = {'January': 1,  # заменяем текстовые значения числовыми
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
        data.append(item.find('td', class_='release-date-item__date').get_text())  # получения списка дат

    set_data = set(data)

    for j in set_data:
        qty = data.count(j)
        if qty > qty_most_common:
            qty_most_common = qty  # поиск самой часто появляющейся даты
            date = j

    date = date.split(' ')  # делим дату на день месяц и  год

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
        release_date[0] = 4  # с 20 ноября до 20 февраля зимний сезон или 4
    elif datetime.datetime(2020, 2, 20) < temp_season < datetime.datetime(2020, 5, 20):
        release_date[0] = 1  # аналогично для всех ниже
    elif datetime.datetime(2020, 5, 20) < temp_season < datetime.datetime(2020, 8, 20):
        release_date[0] = 2
    elif datetime.datetime(2020, 8, 20) < temp_season < datetime.datetime(2020, 11, 20):
        release_date[0] = 3
    elif datetime.datetime(2020, 11, 20) < temp_season < datetime.datetime(2021, 2, 20):
        release_date[0] = 4
    else:
        release_date[0] = 0

    # получаем день недели
    release_date[1] = datetime.datetime.strptime(f'{year}-{month}-{day}', '%Y-%m-%d').isoweekday()

    if datetime.datetime(2019, 12, 15) < temp_season < datetime.datetime(2020, 1, 15):
        release_date[2] = 1  # если вышел в зимние каникулы
    elif datetime.datetime(2020, 12, 15) < temp_season < datetime.datetime(2021, 1, 15):
        release_date[2] = 1  # если вышел в летние каникулы
    elif datetime.datetime(2020, 6, 15) < temp_season < datetime.datetime(2020, 9, 10):
        release_date[2] = 1
    else:
        release_date[2] = 0

    return release_date


# endregion


# region Функция получения данных о съемочной группе
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
        if awards is not None:  # проверяем на пустоту
            awards = awards.find_all('span', class_='awards-blurb')
            for span_a in awards:
                if awards is not None:
                    awards = span_a.get_text().replace('.', '').replace('\n', '')  # заменяем ненужные символы
                    if awards.find('Oscar') != -1 or awards[0].find('Oscars') != -1:  # если есть информация о оскарах
                        if awards.find('Nominated') != -1:  # если номинирован
                            temp_aw = (awards.replace(' ', '')
                                       .replace('Nominated', '')
                                       .replace('for', '')
                                       .replace('Oscar', '')
                                       .replace('s', ''))
                            temp_aw = int(temp_aw) if str(temp_aw).isdigit() else 1  # заменяем числом
                            if temp_aw > 2:
                                total[0] += 1  # если номинация больше 2 считаем за получение 1 оскара
                            else:
                                total[0] += 0
                        else:
                            temp_aw = (awards.replace(' ', '')
                                       .replace('Won', '')
                                       .replace('Oscar', '')
                                       .replace('s', ''))
                            # считаем количество полученных оскаров
                            total[0] += int(temp_aw) if str(temp_aw).isdigit() else 1
                    else:
                        total[0] += 0  # иначе записываем на возврат ноль

                    if awards.find('wins') != -1:
                        total[1] = 1  # 1-если были полученные награды
                    else:
                        total[1] = 0  # 0-если полученных наград небыло

                    if awards.find('&') != -1:
                        awards = (awards.split('&')[0]
                                  .replace(' ', '')
                                  .replace('Another', '')
                                  .replace('wins', ''))
                    else:
                        awards = (awards.replace(' ', '')
                                  .replace('Another', '')
                                  .replace('wins', ''))

        if temp == 'SEE RANK':  # считаем ретинг
            score += 10 / 2  # если не входит в топ 5000
        elif temp in ('Top 5000', 'Top 2500', 'Top 1000'):
            score += 20 / 2  # если входит в топ 5000 но не в топ 500
        elif temp == 'Top 500':
            score += 30 / 2  # если входит в топ 500
        elif 50 < int(temp) <= 100:
            score += 40 / 2  # если входит в топ 100
        elif 0 < int(temp) <= 50:
            score += 50 / 2  # если входит в топ 50
        else:
            return 0

        score += int(awards) if str(awards).isdigit() else 1  # прибавляем количество наград
        # score += ((100 - int(temp)) / 2) + 30 / 2

    num = len(child_id) if len(child_id) != 0 else 1
    total[2] = round(score / num)  # делим количество наград на количество человек из списка
    return total
# endregion


# region Функция получения данных с википедии, если небыло на imdb
def take_box_wiki(child_id):
    def is_number(v):  # проверяем цифра или нет
        try:
            float(v)
            return True
        except ValueError:
            return False

    def calculate(cal):  # получаем цифру и меняем млн на 1000000 и тд
        reg = re.findall(r'\[\d*]', cal)
        temp = (cal.replace('$', '')  # замена символов
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
        temp = temp.split('*')  # разделяем вход на число и млн, млрд, тыс
        temp[0] = temp[0].replace(',', '.').split('–')[0]  # если данные в формате 100-150 то выбераем нижний порог
        if len(temp) > 1:  # если в списке больше 1 значения
            if len(temp[0]) > 4:  # если длина цифры больше 4 символов (1 234 567)
                return ''.join(temp[0]).replace(' ', '').replace('\xa0', '')
            else:  # иначе если (1,234)
                temp[1] = (temp[1]  # заменяем второе значение на численное
                           .replace('тыс', '1000')
                           .replace('млн', '1000000')
                           .replace('млрд', '1000000000')
                           .replace(',', '.'))
                if is_number(temp[0]) is True and is_number(temp[1]):  # если оба значения цифры
                    return round((float(temp[0]) * float(temp[1])))  # возвращаем перемноженный вариант
                    # вхождение: $1,234 млн
                    # выход: 1234000
                    # вхождение: $100-150 млн
                    # выход: 100000000
                    # вхождение: $1 234 000
                    # выход: 1234000
                    # ошибка: вхождение:  много денег и др
                else:
                    return 0
        else:
            return 0

    budget = [0, 0, 0]

    try:
        # вврдим строку поиска id на сайте imdb
        driver.get(f'https://ru.wikipedia.org/w/index.php?search=IMDb%09ID+{str(child_id)}&ns0=1')
        time.sleep(2)  # ждем прогрузки страницы с результатами поиска

        search_result = driver.find_element(By.XPATH, '//div[@class="mw-search-result-heading"]//a')
        if search_result is not None:  # проверка на пустоту
            search_result = search_result.get_attribute('href')

        url = str(search_result)
        session = requests.Session()
        request = session.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'lxml')  # загружаем найденную страницу в более быстрый и bs4

        temp_budget = soup.find('span', attrs={'data-wikidata-property-id': 'P2130'})
        if temp_budget is not None:
            temp_budget = temp_budget.get_text()
            budget[0] = calculate(temp_budget)  # получаем бюджет в числовом формате
        else:
            budget[0] = 0

        temp_box = soup.find('span', attrs={'data-wikidata-property-id': 'P2142'})
        if temp_box is not None:
            temp_box = temp_box.get_text()
            budget[1] = calculate(temp_box)
        else:
            budget[1] = 0

        franchise1 = soup.find('span', attrs={'data-wikidata-property-id': 'P155'})  # узнаем есть ли предыдущие фильмы
        if franchise1 is not None:
            budget[2] = 1
        else:
            budget[2] = 0
    except (NoSuchElementException, StaleElementReferenceException):  # если вылетела ошибка возвращаем нули
        budget[0] = 0
        budget[1] = 0
        budget[2] = 0
        return budget
        pass

    return budget


# endregion


def parser(title_id):
    global checked_counter
    directors, writers, stars = [], [], []
    directors_awards, writers_awards, stars_awards = '', '', ''
    year, mpaa, duration, genre, season, popularity, budget, boxoffice, franchise = '', '', '', '', '', '', '', '', ''
    hour, minute, day, age_limit, oscars, holiday = '', '', '', '', '', ''

    agelimit_format = {'PG-13': 3, 'NC-17': 5, 'PG': 2, 'R': 4, 'G': 1, 'X': 6,
                       '0': 1, '6': 2, '12': 3, '14': 4, '16': 5, '18': 6}
    genre_format = {'Action': 1, 'Adventure': 2, 'Drama': 3, 'Comedy': 4, 'Crime': 5,
                    'Romance': 6, 'Mystery': 7, 'Horror': 8, 'History': 9, 'Western': 10,
                    'Music': 11, 'Biography': 12, 'Musical': 13, 'Film-Noir': 14, 'Animation': 15,
                    'Fantasy': 16, 'Sci-Fi': 17, 'Thriller': 18, 'Family': 19, 'Short': 20,
                    'Sport': 21, 'War': 22, 'Reality-TV': 23, 'Game-Show': 24, 'Documentary': 25,
                    'Talk-Show': 26, 'News': 27, 'Adult': 28}
    '''
    genre_format = {'Action': 1, 'Adventure': 2, 'Drama': 3, 'Romance': 3, 'Comedy': 4,
                    'Crime': 5, 'Mystery': 6, 'Horror': 7, 'Western': 8, 'History': 9, 'Documentary': 9,
                    'Biography': 10, 'Animation': 11, 'Fantasy': 12, 'Sci-Fi': 12,
                    'Thriller': 13, 'Music': 14, 'Musical': 14, 'Film-Noir': 15, 'War': 16, 
                    'Family': 17, 'Short': 18, 'Sport': 19,  'Reality-TV': 20, 
                    'Game-Show': 21, 'Talk-Show': 22, 'News': 23, 'Adult': 24}
    '''

    url = f'https://www.imdb.com/title/tt{str(title_id)}/'
    session = requests.Session()
    request = session.get(url, headers=headers)
    soup = BeautifulSoup(request.content, 'lxml')

    boxoffice_wiki = take_box_wiki(title_id)  # отправляем поиск фильма в вики

    success = soup.find('li', class_='ipc-inline-list__item')
    title = soup.find('h1', attrs={'data-testid': 'hero-title-block__title'})
    if title is not None:
        title = title.get_text()  # название фильма

    if success is not None:  # страница не пустая или не принадлежит не фильму
        # region С помощью regex выбираем значения длительности и возрастного ограничения
        items_list = soup.find('ul', class_='ipc-inline-list').find_all('li', class_='ipc-inline-list__item')
        for item in items_list:
            hour = re.findall(r'\b\w{1,2}[h]\b', item.get_text()) if not hour else hour  # поиск часов
            minute = re.findall(r'\b\w{1,2}[m]\b', item.get_text()) if not minute else minute  # поиск минут
            if item.find('a', class_='ipc-link') is not None:
                year = re.findall(r'\d{4}', item.get_text()) if not year else year  # поиск года выхода
                mpaa = re.findall(r'\b([0-9]{1,2}\+)|(PG-13|NC-17|PG|R|G)|(TV-(Y7|Y|PG|G|14|MA))\b',
                                  item.get_text()) if not mpaa else mpaa  # поиск возрастного рейтинга
        # endregion

        # region Длительность фильма
        hour = hour[0].replace('h', '') if hour else 0  # часы длительности
        minute = minute[0].replace('m', '') if minute else 0  # минуты длительности
        duration = int(hour) * 60 + int(minute)  # час * на 60 + минуты
        # endregion

        # region Если получили возрастной рейтинг в формате MPAA конвертируем в рус вариант
        while type(mpaa) not in (str, int):
            mpaa = mpaa[0] if mpaa else 0
        if re.search(r'\b([0-9]{1,2}\+)|(PG-13|NC-17|PG|R|G)', str(mpaa)) is not None:
            if str(mpaa).replace('+', '') in agelimit_format:
                age_limit = agelimit_format[str(mpaa).replace('+', '')]
            else:
                age_limit = 0
        else:
            age_limit = 0
        # endregion

        # region Жанр фильма
        genre = soup.find('div', attrs={'data-testid': 'genres'})
        if genre is not None:  # проверка наличия на странице
            genre = genre.find('span', class_='ipc-chip__text')
            if genre is not None:
                genre = genre_format[genre.get_text()]
        # endregion

        # region Дата выхода фильма
        release_date = convert_date(title_id)  # получаем список
        season = release_date[0]  # 0-сезон выхода
        day = release_date[1]  # 1-день выхода от 1 до 7
        holiday = release_date[2]  # выход в каникулы да/нет
        # endregion

        # region Данные о съемочной группе
        temp = 1
        # Получаем список съемочной группы
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
                    # добавляем в список id съемочной группы со страницы фильма
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

        directors = take_person_score(directors)  # получаем наличие наград режиссеров
        writers = take_person_score(writers)  # получаем наличие наград сценаристов
        stars = take_person_score(stars)  # получаем наличие наград 3х звезд в главных ролях

        oscars = directors[0] + writers[0] + stars[0]  # количество оскаров у съемочной группы

        directors_awards = directors[1]  # наличие наград у режиссеров
        writers_awards = writers[1]  # наличие наград у сценаристов
        stars_awards = stars[1]  # наличие наград у звезд

        directors = directors[2]  # рейтинг режиссеров рейтинг в топ 5000 на imdb + количество полученных наград
        writers = writers[2]  # рейтинг сценаристов рейтинг в топ 5000 на imdb + количество полученных наград
        stars = stars[2]  # рейтинг звезд рейтинг в топ 5000 на imdb + количество полученных наград
        # endregion

        # region Популярность на imdb
        popularity = soup.find('div', attrs={'data-testid': 'hero-rating-bar__popularity__score'})
        if popularity is not None:
            popularity = popularity.get_text().replace(',', '')
        else:
            popularity = 5000
        # endregion

        # region Значения бюджета и кассовых сборов
        boxoffice_section = soup.find('div', attrs={'data-testid': 'title-boxoffice-section'})
        if boxoffice_section is not None:  # проверка на пустоту
            budget = boxoffice_section.find('li', attrs={'data-testid': 'title-boxoffice-budget'})
            if budget is not None:
                budget = budget.find('span', class_='ipc-metadata-list-item__list-content-item')
                if budget is not None:
                    budget = (budget.get_text()  # удаляем ненужные символы
                              .replace('$', '')  # да, можно сделать через список
                              .replace('£', '')  # иногда через список не работает и вызывает ошибку
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
            # все то же самое что и сверху но для кассовых сборов
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
        # endregion
    else:
        title_id = 0  # если страницы нет или не фильм сохраняем id как пропуск значения

    franchise = boxoffice_wiki[2]

    print(checked_counter, url)
    checked_counter += 1

    # region Переводим значения в int
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
    budget = int(budget) if str(budget).isdigit() else 0  # сохраняем число если строка может быть числом иначе 0
    boxoffice = int(boxoffice) if str(boxoffice).isdigit() else 0
    # endregion

    if int(duration) <= 40:
        title_id = 0  # если длительность меньше 40 (короткометражка) ставим id как пропуск

    if budget in (0, None, '') or boxoffice in (0, None, ''):
        title_id = 0  # если бюджет = 0 ставим id как пропуск
    else:
        if budget < 100000:
            title_id = 0  # если бюджет меньше 100 000 ставим id как пропуск
        else:
            budget = int((str(budget)[0] + str(budget)[1]) + ("0" * (len(str(budget)) - 2)))

    if (budget * 4) > boxoffice < (budget / 4):
        title_id = 0  # если кассовые сборы в 4 раза больше или меньше бюджета считаем ненормальными данными

    if boxoffice < 10000:
        title_id = 0  # если кассовые сборы меньше 10 000 ставим id как пропуск

    if boxoffice >= (budget * 2):
        profitable = 100  # если сборы в 2 раза больше то окупаемость полная
    elif budget <= boxoffice <= (budget * 2):
        profitable = 50  # если сборы больше бюджета то окупаемость частичная
    else:
        profitable = 0  # иначе не окупился

    # if (budget * 4) > boxoffice < (budget / 4):
    #     title_id = 0

    # region Приравниваем значения
    if boxoffice <= 500000:
        boxoffice = 0
    elif 500001 < boxoffice <= 1000000:
        boxoffice = 10
    elif 1000001 < boxoffice <= 2500000:
        boxoffice = 20
    elif 2500001 < boxoffice <= 5000000:
        boxoffice = 30
    elif 5000001 < boxoffice <= 7500000:
        boxoffice = 40
    elif 7500001 < boxoffice <= 10000000:
        boxoffice = 50
    elif 10000001 < boxoffice <= 25000000:
        boxoffice = 60
    elif 25000001 < boxoffice <= 50000000:
        boxoffice = 70
    elif 50000001 < boxoffice <= 75000000:
        boxoffice = 80
    elif 75000001 < boxoffice <= 100000000:
        boxoffice = 90
    elif 100000001 < boxoffice <= 11500000:
        boxoffice = 100
    else:
        boxoffice = 100
    # endregion

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


def del_copy(obj):  # удаляем копии из списка
    n = []
    for i in obj:
        if i not in n:
            n.append(i)
    return n


# region Удаляем выбросы
def del_ejects(filename):
    print(filename)
    df = pandas.read_csv(f'./dataset_{filename}.txt', sep=';')

    for x in ['D1']:
        q75, q25 = numpy.percentile(df.loc[:, x], [75, 25])
        intr_qr = q75 - q25

        max = q75 + (1.5 * intr_qr)
        min = q25 - (1.5 * intr_qr)

        df.loc[df[x] < min, x] = numpy.nan
        df.loc[df[x] > max, x] = numpy.nan

    for x in ['X11']:
        q75, q25 = numpy.percentile(df.loc[:, x], [75, 25])
        intr_qr = q75 - q25

        max = q75 + (1.5 * intr_qr)
        min = q25 - (1.5 * intr_qr)

        df.loc[df[x] < min, x] = numpy.nan
        df.loc[df[x] > max, x] = numpy.nan

    print(df.isnull().sum())
    df.isnull().sum()
    df = df.dropna(axis=0)

    df.to_csv(f'./dataset_{filename}.txt', index=False, sep=';')
# endregion


def write_file(dset, title, ver, state):
    # region Записываем заголовки в зависимости от версии
    sample_frow = (f'Возрастное ограничение (1- 0+, 2- 6+ и тд);'
                   f'Длительность фильма в минутах;'
                   f'Сезон выхода (0-зима, 1-весна и тд);'
                   f'Вышел ли фильм в период высокой посещаемости кинотеатров (0-нет, 1-да);'
                   f'Имеют ли режиссеры награды (0-нет, 1-да);'
                   f'Имеют ли сценаристы награды (0-нет, 1-да);'
                   f'Имеют ли 3-х главные звезды награды (0-нет, 1-да);'
                   f'Количество оскаров у съемочной группы;'
                   f'Основной жанр фильма (1-Action, 2-Adventure, 3-Drama и тд);'
                   f'Является ли фильм частью франшизы;'
                   f'Бюджет фильма;')
    if ver == 'D1':
        header = f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;D1\n'
        frow = (f'{sample_frow}'
                f'Кассовые сборы фильма\n')
    elif ver == 'D2':
        header = f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;D1\n'
        frow = (f'{sample_frow}'
                f'Окупаемость фильма (0-не окупился, 50-собрал бюджет, 100-окупился)\n')
    else:
        header = f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;D1;D2\n'
        frow = (f'{sample_frow}'
                f'Кассовые сборы фильма;'
                f'Окупаемость фильма (0-не окупился, 50-собрал бюджет, 100-окупился)\n')
    # endregion

    if state == 1:
        with open(f'./dataset_sample.txt', 'w', encoding='utf-8') as f:
            f.write(header)
            f.write(frow)
        for files_item_add in files:
            with open(f'./dataset_{files_item_add}.txt', 'w', encoding='utf-8') as f:
                f.write(header)
            with open(f'./dataset_{files_item_add}_test.txt', 'w', encoding='utf-8') as f:
                f.write(header)

    # region Записываем данные в зависимости от версии
    if ver == 'D1':
        with open(f'./dataset_{title}.txt', 'a', encoding='utf-8') as f:
            for span in dset:
                f.write(f'{span["age-limit"]};{span["duration"]};'
                        f'{span["release-season"]};{span["holiday"]};'
                        f'{span["directors-awards"]};{span["writers-awards"]};{span["stars-awards"]};'
                        f'{span["oscars"]};{span["genre"]};{span["franchise"]};{span["budget"]};'
                        f'{span["box-office"]}\n')
    elif ver == 'D2':
        with open(f'./dataset_{title}.txt', 'a', encoding='utf-8') as f:
            for span in dset:
                f.write(f'{span["age-limit"]};{span["duration"]};'
                        f'{span["release-season"]};{span["holiday"]};'
                        f'{span["directors-awards"]};{span["writers-awards"]};{span["stars-awards"]};'
                        f'{span["oscars"]};{span["genre"]};{span["franchise"]};{span["budget"]};'
                        f'{span["profitable"]}\n')
    else:
        with open(f'./dataset_{title}.txt', 'a', encoding='utf-8') as f:
            for span in dset:
                f.write(f'{span["age-limit"]};{span["duration"]};'
                        f'{span["release-season"]};{span["holiday"]};'
                        f'{span["directors-awards"]};{span["writers-awards"]};{span["stars-awards"]};'
                        f'{span["oscars"]};{span["genre"]};{span["franchise"]};{span["budget"]};'
                        f'{span["box-office"]};'
                        f'{span["profitable"]}\n')
    # endregion

    if state == 2:
        # region Разделяем данные на обучающие и тестирующие
        for ctr in files:
            lrow_counter = 0
            temp_dataset, dataset_test = [], []
            with open(f'./dataset_{ctr}.txt', 'r', encoding='utf-8') as lfile:
                for lrow in lfile:
                    if lrow_counter != 0:
                        if lrow_counter % 8 == 0:
                            dataset_test.append(lrow)
                        else:
                            temp_dataset.append(lrow)
                    lrow_counter += 1
            lfile.close()
            with open(f'./dataset_{ctr}_test.txt', 'a', encoding='utf-8') as mfile:
                for mrow in dataset_test:
                    mfile.write(mrow)
            open(f'./dataset_{ctr}.txt', 'w').close()
            with open(f'./dataset_{ctr}.txt', 'a', encoding='utf-8') as nfile:
                nfile.write(header)
                for orow in temp_dataset:
                    nfile.write(orow)
        mfile.close()
        nfile.close()
        f.close()
        # endregion

        for repeat in range(5):  # 5 раз удаляем выбросы
            for item_files in files:
                del_ejects(f'{item_files}')
                del_ejects(f'{item_files}_test')

        # region Делим созданные фалы на группы и записываем в excel
        group0 = pandas.read_csv(f'./dataset_sample.txt', sep=';')
        group1 = pandas.read_csv(f'./dataset_all.txt', sep=';')
        group1_test = pandas.read_csv(f'./dataset_all_test.txt', sep=';')
        group2 = pandas.read_csv(f'./dataset_0-10.txt', sep=';')
        group2_test = pandas.read_csv(f'./dataset_0-10_test.txt', sep=';')
        group3 = pandas.read_csv(f'./dataset_10-40.txt', sep=';')
        group3_test = pandas.read_csv(f'./dataset_10-40_test.txt', sep=';')
        group4 = pandas.read_csv(f'./dataset_40-inf.txt', sep=';')
        group4_test = pandas.read_csv(f'./dataset_40-inf_test.txt', sep=';')

        # Подготавливаем листы к записи
        sheets = {'SAMPLE': group0,
                  'DATA': group1, 'TEST': group1_test,
                  'DATA_0-10': group2, 'TEST_0-10': group2_test,
                  'DATA_10-40': group3, 'TEST_10-40': group3_test,
                  'DATA_40-inf': group4, 'TEST_40-inf': group4_test}

        writer = pandas.ExcelWriter(f'./dataset{ver}.xlsx', engine='openpyxl')

        # Записываем каждый документ в отдельный лист
        for sheet_name in sheets.keys():
            sheets[sheet_name].to_excel(writer, sheet_name=sheet_name, engine='openpyxl', index=False)

        writer.save()
        # endregion

        # region Удаляем временные txt файлы
        os.remove('./dataset_sample.txt')
        for files_item_del in files:
            if os.path.exists(f'./dataset_{files_item_del}.txt'):
                os.remove(f'./dataset_{files_item_del}.txt')
            if os.path.exists(f'./dataset_{files_item_del}_test.txt'):
                os.remove(f'./dataset_{files_item_del}_test.txt')
        # endregion


# region Создаем файл с id фильмов
def first_part():
    raw_dataset.append(scrapper(top_1000))
    raw_dataset.append(scrapper(bottom_1000))

    for k in raw_dataset[0]:
        ids.append(k)  # записываем id топ 1000 лучших фильмов

    for k in raw_dataset[1]:
        ids.append(k)  # записываем id топ 1000 худших фильмов

    print(ids)
    with open(f'./ids.txt', 'w+', encoding='utf-8') as id_file:
        for item in ids:
            id_file.write(item + '\n')  # записываем в файл
# endregion


def second_part():
    counter = 0  # счетчик с какой строки начинать парсить
    # очистка предыдущего парсинга если надо
    open(f'./tempdata.txt', 'w').close()

    # записываем заголовок
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
            # запускаем функцию parser  в несколько потоков с входными данными из файла с id
            temp_dataset = pool.map(parser, temp_ids)
        except (TimeoutError, requests.exceptions.ConnectionError) as e:
            print(e)  # пишем ошибку
            pass

        for temp_row in temp_dataset:
            if temp_row["id"] in (0, '0', '', None):  # если id после парсинга 0 то пропускаем
                pass
            else:
                output_dataset.append(temp_row)  # иначе записываем в dataset

        with open(f'./tempdata.txt', 'a', encoding='utf-8') as temp_data:
            for subrow in output_dataset:
                temp_data.write(f'{subrow}\n')

        counter += 100

    temp_file.close()
    temp_data.close()


def third_part():
    versions = ['D1', 'D2', '']
    dataset = []

    with open(f'./tempdata.txt', 'r', encoding='utf-8') as temp_fdata:
        for irow in temp_fdata:
            dataset.append(eval(irow.replace('\n', '')))

    dataset = del_copy(dataset)  # удаляем копии

    with open(f'./raw_data.txt', 'a', encoding='utf-8') as j_file:
        for jrow in dataset:
            j_file.write(f'{jrow["url"]};{jrow["id"]};'
                         f'{jrow["name"]};{jrow["year"]};{jrow["duration"]};'
                         f'{jrow["imdb-popularity"]};{jrow["budget"]};{jrow["box-office"]}\n')

    dataset0_10, dataset10_40, dataset40_inf = [], [], []

    # сортируем данные по бюджету
    for krow in dataset:
        if krow["budget"] < 10000000:
            dataset0_10.append(krow)
        if 10000001 < krow["budget"] < 40000000:
            dataset10_40.append(krow)
        if 40000001 < krow["budget"]:
            dataset40_inf.append(krow)

    # region Отправляем данные на запись
    write_file(dataset, 'sample', 'D1', 1)
    write_file(dataset, 'all', 'D1', 0)
    write_file(dataset0_10, '0-10', 'D1', 0)
    write_file(dataset10_40, '10-40', 'D1', 0)
    write_file(dataset40_inf, '40-inf', 'D1', 2)

    write_file(dataset, 'sample', 'D2', 1)
    write_file(dataset, 'all', 'D2', 0)
    write_file(dataset0_10, '0-10', 'D2', 0)
    write_file(dataset10_40, '10-40', 'D2', 0)
    write_file(dataset40_inf, '40-inf', 'D2', 2)

    write_file(dataset, 'sample', '', 1)
    write_file(dataset, 'all', '', 0)
    write_file(dataset0_10, '0-10', '', 0)
    write_file(dataset10_40, '10-40', '', 0)
    write_file(dataset40_inf, '40-inf', '', 2)
    # endregion

    temp_fdata.close()
    j_file.close()

    df0 = pandas.read_csv('./raw_data.txt', sep=';')
    df0.to_excel('./raw_data.xlsx', 'DATA', engine='openpyxl', index=False)

    os.remove('./raw_data.txt')


# region Функция для тестирования парсера на одной или нескольких страниц
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
# endregion


if __name__ == '__main__':
    pool = Pool()  # создаем пул потоков для мультипоточности
    chrome_options = Options()
    chrome_options.add_argument("--disable-blink-features=AutomationControlled")
    # chrome_options.add_argument(r"user-data-dir=C:\\Users\\" + WINUSER + "\\AppData\\Local\\Google\\Chrome\\User Data")
    chrome_options.add_argument("--headless")
    driver = webdriver.Chrome(options=chrome_options)  # задаем значения для selenium

    raw_dataset = []
    ids = []

    # test()
    # first_part()
    # second_part()
    third_part()
