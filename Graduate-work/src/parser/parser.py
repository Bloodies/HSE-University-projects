import datetime     # Библиотека для работы с датами
import lxml         # Библиотека для работы со структурой веб страницы
import os           # Библиотека для работы с файлами компьютера
import re           # Библиотека для поиска регулярных значений
import requests     # Библиотека для формирования запросов к сайтам
import time         # Библиотека для работы со временем
from bs4 import BeautifulSoup           # Библиотека для парсинга данных
from multiprocessing.dummy import Pool  # Библиотека для мультипоточности
from selenium import webdriver          # Библиотека для парсинга, глубже и дольше чем beautifulsoup
from selenium.common.exceptions import NoSuchElementException
from selenium.common.exceptions import StaleElementReferenceException
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options

wiki_info = [0, 0, 0]   # Инициализация переменной для хранения данных с википедии
checked_counter = 0     # Счетчик количества обработаных ссылок
headers = {'accept': '*/*',
           'user-agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36'}


# Функция проверки на вещественное число, не путать с .isdigit (проверкой на целое число)
def is_number(v):   # проверяем цифра или нет
    try:
        float(v)    # проверка для всех вещественных чисел
        return True
    except ValueError:
        return False


# Функция замены мусора на пустоту
def replace_scrap(data):    # Вызывается нужно много символов заменять
    clean_data = (data      # замена символов
                  .replace(' ', '')
                  .replace('U', '')
                  .replace('долл.', '')
                  .replace('долларов', '')
                  .replace('(в США)', '')
                  .replace('.', '')
                  .replace('(estimated)', '')
                  .replace('~', ''))
    return clean_data


# Функция конвертации валют в доллары
def convert_currency(num):
    # Заменяем иконки символов на курс к доллару со знаком умножения
    temp_num = (num
                .replace('$', '1*')
                .replace('£', '1.26*')
                .replace('€', '1.07*')
                .replace('₩', '0.00079*')
                .replace('¥', '0.0078*')
                .replace('₹', '0.013*')
                .replace('£', '1.26*')
                .replace('DEM', '0.5414*')
                .replace('FRF', '1.07*')
                .replace('R', '0.064*'))
    temp_num = temp_num.split('*')      # Разделяем строковое значение на 2 значения по знаку умножения
    if len(temp_num) > 1:               # Если список содержит больше 1 значения (операция успешна)
        try:                            # Пробуем выполнить умножение
            # Умножаем значения переводя их в вещественные и округляем до целого
            currency = round(float(temp_num[0]) * float(temp_num[1]))
            return currency
        except Exception as ex:
            print(ex)                   # Выводим ошибку если попался необработанный символ
            return 0
    else:                               # Иначе возвращаем что получили
        return num


# Функция получения данных с википедии, если не было на imdb
def parse_wiki(child_id):
    global wiki_info                        # Ссылаемся на глобальную переменную
    wiki_info = [0, 0, 0]                   # Обнуляем глобальную переменную

    def calculate(cal):                     # получаем цифру и меняем млн на 1000000 и тд
        reg = re.findall(r'\[\d*]', cal)    # Вводим в замену регулярное выражение
        temp = (replace_scrap(cal)          # Так как данные с википедии не возможно конвертировать
                .replace(''.join(reg), '')  # То просто их удаляем
                .replace('£', '')           # Тем более в 90% случаев показатели указываются в долларах
                .replace('€', '')
                .replace('₩', '')
                .replace('¥', '')
                .replace('₹', '')
                .replace('£', '')
                .replace('DEM', '')
                .replace('FRF', '')
                .replace('R', ''))
        temp = (temp.replace('тыс', '*тыс')  # Добавляем знак умножения, чтобы понять где разделять
                .replace('млн', '*млн')
                .replace('млрд', '*млрд'))
        temp = temp.split('*')              # разделяем вход на число и (млн, млрд, тыс)
        temp[0] = temp[0].replace(',', '.').split('–')[0]  # если данные в формате 100-150 то выбираем нижний порог
        if len(temp) > 1:                   # если в списке больше 1 значения
            if len(temp[0]) > 4:            # если длина цифры больше 4 символов (1 234 567)
                return ''.join(temp[0]).replace(' ', '').replace('\xa0', '')
            else:                           # иначе если (1,234)
                temp[1] = (temp[1]          # заменяем второе значение на численное
                           .replace('тыс', '1000')
                           .replace('млн', '1000000')
                           .replace('млрд', '1000000000')
                           .replace(',', '.'))
                if is_number(temp[0]) is True and is_number(temp[1]):   # если оба значения цифры
                    return round((float(temp[0]) * float(temp[1])))     # возвращаем перемноженный вариант
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

    try:
        # вводим строку поиска id на сайте imdb
        driver.get(f'https://ru.wikipedia.org/w/index.php?search=IMDb%09ID+{str(child_id)}&ns0=1')
        time.sleep(2)  # ждем загрузки страницы с результатами поиска

        search_result = driver.find_element(By.XPATH, '//div[@class="mw-search-result-heading"]//a')
        if search_result is not None:                               # проверка на пустоту
            search_result = search_result.get_attribute('href')     # Находим ссылку на страницу

        url = str(search_result)
        session = requests.Session()
        request = session.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'lxml')  # Загружаем найденную страницу в более быстрый bs4

        # region Франшиза
        franchise1 = soup.find('span', attrs={'data-wikidata-property-id': 'P155'})  # узнаем есть ли предыдущие фильмы
        if franchise1 is not None:  # Если значение есть, то есть и франшиза
            wiki_info[2] = 1
        else:                       # Если нет то и франшизы нет
            wiki_info[2] = 0
        # endregion

        # region Бюджет
        temp_budget = soup.find('span', attrs={'data-wikidata-property-id': 'P2130'})  # Ищем данные бюджета
        if temp_budget is not None:                 # Проверка на пустоту
            temp_budget = temp_budget.get_text()    # Получаем текст
            wiki_info[0] = calculate(temp_budget)   # Переводим в числовой формат
        else:
            wiki_info[0] = 0
        # endregion
        # region Касса
        temp_box = soup.find('span', attrs={'data-wikidata-property-id': 'P2142'})  # Ищем кассовые сборы
        if temp_box is not None:                    # Проверка на пустоту
            temp_box = temp_box.get_text()          # Получаем текст
            wiki_info[1] = calculate(temp_box)      # Переводим в числовой формат
        else:
            wiki_info[1] = 0
        # endregion


    except (NoSuchElementException, StaleElementReferenceException):
        wiki_info = [0, 0, 0]                       # Если вылетела ошибка возвращаем нули
        pass


# Функция получения бюджета (не актуально)
# Перенесено в основную функцию, иначе много вызовов и увеличивается время обработки
def parse_budget(request):
    budget = request.find('li', attrs={'data-testid': 'title-boxoffice-budget'})
    if budget is not None:
        budget = budget.find('span', class_='ipc-metadata-list-item__list-content-item')
        if budget is not None:
            budget = convert_currency(budget.get_text())
            budget = replace_scrap(budget).replace(',', '').replace('\xa0', '')
        else:
            budget = wiki_info[0]
    else:
        budget = wiki_info[0]


# Функция получения длительности
def parse_duration(hour, minute):
    hour = hour[0].replace('h', '') if hour else 0          # Часы длительности
    minute = minute[0].replace('m', '') if minute else 0    # Минуты длительности
    duration = int(hour) * 60 + int(minute)                 # Час * на 60 + минуты
    return duration


# Функция получения страны (не актуально)
def parse_country():
    smth = 0
    return smth


# Функция получения жанра
def parse_genre(request):
    # Список кодировки жанров
    genre_format = {'Action': 1, 'Adventure': 2, 'Drama': 3, 'Romance': 3, 'Comedy': 4,
                    'Crime': 5, 'Mystery': 6, 'Horror': 7, 'Western': 8, 'History': 9, 'Documentary': 9,
                    'Biography': 10, 'Animation': 11, 'Fantasy': 12, 'Sci-Fi': 12,
                    'Thriller': 13, 'Music': 14, 'Musical': 14, 'Film-Noir': 15, 'War': 16,
                    'Family': 17, 'Short': 18, 'Sport': 19, 'Reality-TV': 20,
                    'Game-Show': 21, 'Talk-Show': 22, 'News': 23, 'Adult': 24}

    genre = request.find('div', attrs={'data-testid': 'genres'})    # Поиск поля по аттрибуту
    if genre is not None:                                           # Проверка на пустоту
        genre = genre.find('li', class_='ipc-chip__text')
        if genre is not None:
            genre = genre_format[genre.get_text()]                  # Получение текста

    return genre


# Функция получения возрастного ограничения
def parse_age_limit(mpaa):
    # Список кодировки возрастного ограничения
    agelimit_format = {'G': 1, 'PG': 2, 'PG-13': 3, 'NC-17': 4, 'R': 5, 'X': 6,
                       '0': 1, '6': 2, '12': 3, '14': 3, '16': 4, '18': 5}

    # Поиск регулярного выражения
    if re.search(r'\b([0-9]{1,2}\+)|(PG-13|NC-17|PG|R|G)', str(mpaa)) is not None:
        if str(mpaa).replace('+', '') in agelimit_format:
            age_limit = agelimit_format[str(mpaa).replace('+', '')]
        else:
            age_limit = 1
    else:
        age_limit = 1

    return age_limit


# Функция конвертации даты в сезон выхода и нагруженности на кинозалы
def parse_date(child_id):
    url = f'https://www.imdb.com/title/tt{str(child_id)}/releaseinfo'
    session = requests.Session()
    request = session.get(url, headers=headers)
    soup = BeautifulSoup(request.content, 'lxml')  # Создаем сессию

    parsed_date = [0, 0]  # Инициализация списка возврощаемых значений
    date = None

    # Список кодировки месяцев
    month_format = {'December': 12, 'January': 1, 'February': 2,
                    'March': 3, 'April': 4, 'May': 5,
                    'June': 6, 'July': 7, 'August': 8,
                    'September': 9, 'October': 10, 'November': 11}

    data = []
    dates_list = soup.find('table', class_='release-dates-table-test-only')
    dates_list = dates_list.find_all('tr', class_='ipl-zebra-list__item release-date-item')
    for item in dates_list:  # Проверка всех дат выхода
        data.append(item.find('td', class_='release-date-item__date').get_text())  # Получения списка дат

    qty_most_common = 0
    for j in set(data):
        qty = data.count(j)
        if qty > qty_most_common:
            qty_most_common = qty  # Поиск самой часто появляющейся даты
            date = j

    date = date.split(' ')  # Делим дату на день месяц и год

    day = date[0]
    if len(str(date[1])) > 3 and not date[1].isdigit():
        month = month_format[str(date[1])]
    else:
        parsed_date = [0, 0]
        return parsed_date

    temp_season = datetime.datetime(2020, int(month), int(day))
    if datetime.datetime(2019, 11, 20) < temp_season < datetime.datetime(2020, 2, 20):
        parsed_date[0] = 4  # с 20 ноября до 20 февраля зимний сезон или 4
    elif datetime.datetime(2020, 2, 20) < temp_season < datetime.datetime(2020, 5, 20):
        parsed_date[0] = 1  # аналогично для всех ниже
    elif datetime.datetime(2020, 5, 20) < temp_season < datetime.datetime(2020, 8, 20):
        parsed_date[0] = 2
    elif datetime.datetime(2020, 8, 20) < temp_season < datetime.datetime(2020, 11, 20):
        parsed_date[0] = 3
    elif datetime.datetime(2020, 11, 20) < temp_season < datetime.datetime(2021, 2, 20):
        parsed_date[0] = 4
    else:
        parsed_date[0] = 4

    if datetime.datetime(2019, 12, 15) < temp_season < datetime.datetime(2020, 1, 15):
        parsed_date[1] = 1  # если вышел в зимние каникулы
    elif datetime.datetime(2020, 12, 15) < temp_season < datetime.datetime(2021, 1, 15):
        parsed_date[1] = 1
    elif datetime.datetime(2020, 6, 15) < temp_season < datetime.datetime(2020, 9, 10):
        parsed_date[1] = 1  # если вышел в летние каникулы
    else:
        parsed_date[1] = 0

    return parsed_date


# Функция получения данных о съемочной группе
def parse_crew(child_id, director):
    def parse_director(work_id):
        sub_url = f'https://www.imdb.com/title/tt{str(work_id)}/?ref_=nm_flmg_dr_1'
        sub_session = requests.Session()
        sub_request = sub_session.get(sub_url, headers=headers)
        sub_soup = BeautifulSoup(sub_request.content, 'lxml')  # Создаем сессию

        # Находим рейтинг фильма
        rating = sub_soup.find('div', attrs={'data-testid': 'hero-rating-bar__aggregate-rating__score'})
        if rating is not None:  # Проверка на пустоту
            rating = rating.find('span', class_='sc-7ab21ed2-1').get_text()  # Получаем текст
            if rating == 0:
                rating = -1  # Возвращаем -1 если данных нет или рейтинг равен 0
        else:
            rating = -1

        return rating

    score, temp = 0, 0
    output = [0, 0, 0, 0]  # Инициализация списка с возвращаемыми значениями

    for item in child_id:
        url = f'https://www.imdb.com/name/nm{str(item)}/'
        session = requests.Session()
        request = session.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'lxml')   # Создаем сессию

        works_urls, works_num, works_sum = [], 0, 0
        if director == 1:                               # Если получали данные о директорах
            # Поиск атрибутов по регулярному выражению
            works = soup.find_all('div', attrs={'id': re.compile("director-tt")})
            if works is not None:                       # Проверка на пустоту
                for work in works:                      # Для каждого фильма в списке
                    # Находим фильм и получаем его id
                    work = (work.find('b').find('a').get('href')
                            .replace('/title/tt', '').replace('/?ref_=nm_flmg_dr_1', ''))
                    works_urls.append(work)
            for url in works_urls:                      # для каждого id  в списке
                info = parse_director(url)
                if info != -1:                          # Если вернулся не -1 то учитываем это значение
                    works_num += float(info)            # Суммируем рейтинги
                    works_sum += 1                      # Суммируем количество фильмов
            output[3] = round(works_num / works_sum)    # Делим сумму рейтингов фильмов на количество фильмов

        temp = soup.find('a', attrs={'id': 'meterRank'})
        if temp is not None:            # Проверка на пустоту
            temp = temp.get_text()      # Если не пусто то страница существует
        else:
            output = [0, 0, 0, 0]       # Иначе возвращаем нули
            return output

        awards = soup.find('div', class_='article highlighted')         # Поиск поля с наградами
        if awards is not None:          # Проверяем на пустоту
            awards = awards.find_all('span', class_='awards-blurb')     # Поиск наград в поле
            for span_a in awards:       # Для каждой награды в списке
                if awards is not None:  # Проверка на пустоту
                    awards = span_a.get_text().replace('.', '').replace('\n', '')       # заменяем ненужные символы
                    if awards.find('Oscar') != -1 or awards[0].find('Oscars') != -1:    # если есть информация о оскарах
                        if awards.find('Nominated') != -1:      # если номинирован
                            temp_aw = (awards.replace(' ', '')  # Удаляем ненужные слова
                                       .replace('Nominated', '')
                                       .replace('for', '')
                                       .replace('Oscar', '')
                                       .replace('s', ''))
                            temp_aw = int(temp_aw) if str(temp_aw).isdigit() else 1  # заменяем числом
                            if temp_aw > 2:
                                output[0] += 1  # если номинаций больше 2 считаем за получение 1 оскара
                            else:
                                output[0] += 0
                        else:
                            temp_aw = (awards.replace(' ', '')
                                       .replace('Won', '')
                                       .replace('Oscar', '')
                                       .replace('s', ''))
                            # считаем количество полученных оскаров
                            output[0] += int(temp_aw) if str(temp_aw).isdigit() else 1
                    else:
                        output[0] += 0  # иначе записываем на возврат ноль

                    if awards.find('wins') != -1:
                        output[1] = 1   # 1-если были полученные награды
                    else:
                        output[1] = 0   # 0-если полученных наград небыло

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
            output = [0, 0, 0, 0]
            return output

        score += int(awards) if str(awards).isdigit() else 1  # прибавляем количество наград
        # score += ((100 - int(temp)) / 2) + 30 / 2

    num = len(child_id) if len(child_id) != 0 else 1
    output[2] = round(score / num)  # делим количество наград на количество человек из списка
    return output


# Основная функция парсера
def parser(title_id):
    global checked_counter, wiki_info  # Определяем глобальные переменные
    # Инициализация переменных
    directors, writers, stars = [], [], []
    director_rating, directors_awards, writers_awards, stars_awards = '', '', '', ''
    year, mpaa, duration, genre, season, popularity, budget, boxoffice, franchise = '', '', '', '', '', '', '', '', ''
    hour, minute, age_limit, oscars, holiday = '', '', '', '', ''

    url = f'https://www.imdb.com/title/tt{str(title_id)}/'  # Передаем ссылку
    session = requests.Session()
    request = session.get(url, headers=headers)     # Создаем сессию
    soup = BeautifulSoup(request.content, 'lxml')   # Читаем страницу

    print(f'{checked_counter}/2000 Progress ({title_id}): \033[31m[processing]\033[0m {url}')
    success = soup.find('li', class_='ipc-inline-list__item')  # Проверяем наличие страницы
    title = soup.find('h1', attrs={'data-testid': 'hero-title-block__title'})
    if title is not None:
        title = title.get_text()  # название фильма

    if success is not None:  # страница не пустая или не принадлежит не фильму
        # print(f'{checked_counter} Progress ({title_id}): [##              ] 1/8 [wiki] {url}')
        # Поиск на вики обязателен так как там информация о франшизе
        parse_wiki(title_id)  # отправляем поиск фильма в вики

        # region С помощью regex выбираем значения длительности и возрастного ограничения
        items_list = soup.find('ul', attrs={'data-testid': 'hero-title-block__metadata'}).find_all('li', class_='ipc-inline-list__item')
        for item in items_list:
            hour = re.findall(r'\b\w{1,2}[h]\b', item.get_text()) if not hour else hour         # поиск часов
            minute = re.findall(r'\b\w{1,2}[m]\b', item.get_text()) if not minute else minute   # поиск минут
            if item.find('a', class_='ipc-link') is not None:
                year = re.findall(r'\d{4}', item.get_text()) if not year else year              # поиск года выхода
                mpaa = re.findall(r'\b([0-9]{1,2}\+)|(PG-13|NC-17|PG|R|G)|(TV-(Y7|Y|PG|G|14|MA))\b',
                                  item.get_text()) if not mpaa else mpaa  # поиск возрастного рейтинга
        # endregion

        while type(year) not in (str, int):
            year = year[0] if year else 0
        # print(f'\r{checked_counter} Progress ({title_id}): [####            ] 2/8 [duration] {url}')
        # region Длительность фильма
        duration = parse_duration(hour, minute)
        # endregion

        # print(f'\r{checked_counter} Progress ({title_id}): [######          ] 3/8 [age limit] {url}')
        # region Если получили возрастной рейтинг в формате MPAA конвертируем в рус вариант
        while type(mpaa) not in (str, int):
            mpaa = mpaa[0] if mpaa else 0

        age_limit = parse_age_limit(mpaa)
        # endregion

        # print(f'\r{checked_counter} Progress ({title_id}): [########        ] 4/8 [genre] {url}')
        # region Жанр фильма
        genre = parse_genre(soup)
        # endregion

        # print(f'\r{checked_counter} Progress ({title_id}): [##########      ] 5/8 [release date] {url}')
        # region Дата выхода фильма
        release_date = parse_date(title_id)  # получаем список
        season = release_date[0]  # 0-сезон выхода
        holiday = release_date[1]  # выход в каникулы да/нет
        # endregion

        # print(f'\r{checked_counter} Progress ({title_id}): [############    ] 6/8 [crew] {url}')
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

        directors = parse_crew(directors, 1)    # получаем наличие наград режиссеров
        writers = parse_crew(writers, 0)        # получаем наличие наград сценаристов
        stars = parse_crew(stars, 0)            # получаем наличие наград 3х звезд в главных ролях

        director_rating = directors[3]
        oscars = directors[0] + writers[0] + stars[0]  # количество оскаров у съемочной группы

        directors_awards = directors[1]         # наличие наград у режиссеров
        writers_awards = writers[1]             # наличие наград у сценаристов
        stars_awards = stars[1]                 # наличие наград у звезд

        directors = directors[2]    # рейтинг режиссеров рейтинг в топ 5000 на imdb + количество полученных наград
        writers = writers[2]        # рейтинг сценаристов рейтинг в топ 5000 на imdb + количество полученных наград
        stars = stars[2]            # рейтинг звезд рейтинг в топ 5000 на imdb + количество полученных наград
        # endregion

        # region Популярность на imdb
        popularity = soup.find('div', attrs={'data-testid': 'hero-rating-bar__popularity__score'})
        if popularity is not None:
            popularity = popularity.get_text().replace(',', '')
        else:
            popularity = 5000
        # endregion

        # print(f'\r{checked_counter} Progress ({title_id}): [##############  ] 7/8 [budget] {url}')
        # region Значения бюджета и кассовых сборов
        boxoffice_section = soup.find('div', attrs={'data-testid': 'title-boxoffice-section'})
        if boxoffice_section is not None:               # Проверка на пустоту секции кассовых сборов
            budget = boxoffice_section.find('li', attrs={'data-testid': 'title-boxoffice-budget'})
            if budget is not None:                      # Проверка на пустоту элемента с бюджетом
                budget = budget.find('span', class_='ipc-metadata-list-item__list-content-item')
                if budget is not None:                  # Дополнительная проверка на пустоту
                    # Очистка значений
                    budget = replace_scrap(budget.get_text()).replace(',', '').replace('\xa0', '')
                    budget = convert_currency(budget)   # Конвертация в доллары
                else:                                   # Если информации нет то присвоение данных из википедии
                    budget = wiki_info[0]
            else:
                budget = wiki_info[0]
            # все то же самое что и сверху но для кассовых сборов
            boxoffice = boxoffice_section.find('li', attrs={'data-testid': 'title-boxoffice-cumulativeworldwidegross'})
            if boxoffice is not None:                           # Проверка на пустоту элемента с кассовыми сборами
                boxoffice = boxoffice.find('span', class_='ipc-metadata-list-item__list-content-item')
                if boxoffice is not None:                       # Дополнительная проверка на пустоту
                    # Очистка значений
                    boxoffice = replace_scrap(boxoffice.get_text()).replace(',', '').replace('\xa0', '')
                    boxoffice = convert_currency(boxoffice)     # Конвертация в доллары
                else:                                           # Если информации нет то присвоение данных из википедии
                    boxoffice = wiki_info[1]
            else:
                boxoffice = wiki_info[1]
        # endregion
    else:
        title_id = 0  # если страницы нет или не фильм сохраняем id как пропуск значения

    franchise = wiki_info[2]

    # region Переводим значения в int
    age_limit = int(age_limit) if age_limit else 0
    duration = int(duration) if duration else 0
    season = int(season) if season else 0
    director_rating = int(director_rating) if str(director_rating).isdigit() else 0
    writers = int(writers) if writers else 0
    stars = int(stars) if stars else 0
    genre = int(genre) if genre else 0
    franchise = int(franchise) if franchise else 0
    budget = int(budget) if str(budget).isdigit() else 0  # сохраняем число если строка может быть числом иначе 0
    boxoffice = int(boxoffice) if str(boxoffice).isdigit() else 0
    # endregion

    if int(duration) <= 50:
        title_id = 0  # если длительность меньше 40 (короткометражка) ставим id как пропуск

    if budget in (0, None, '') or boxoffice in (0, None, ''):
        title_id = 0  # если бюджет = 0 ставим id как пропуск
    else:
        if budget < 100000 or boxoffice < 100000:
            title_id = 0  # если бюджет меньше 100 000 ставим id как пропуск
        else:
            budget = int((str(budget)[0] + str(budget)[1]) + ("0" * (len(str(budget)) - 2)))
            budget = int(int(budget) / 100000)
            boxoffice = int((str(boxoffice)[0] + str(boxoffice)[1]) + ("0" * (len(str(boxoffice)) - 2)))
            boxoffice = int(int(boxoffice) / 100000)

    # if (budget * 4) > boxoffice < (budget / 4):
    #     title_id = 0

    # region Приравниваем значения
    # if boxoffice <= 500000:
    #     boxoffice = 0
    # elif 500001 < boxoffice <= 1000000:
    #     boxoffice = 10
    # elif 1000001 < boxoffice <= 2500000:
    #     boxoffice = 20
    # elif 2500001 < boxoffice <= 5000000:
    #     boxoffice = 30
    # elif 5000001 < boxoffice <= 7500000:
    #     boxoffice = 40
    # elif 7500001 < boxoffice <= 10000000:
    #     boxoffice = 50
    # elif 10000001 < boxoffice <= 25000000:
    #     boxoffice = 60
    # elif 25000001 < boxoffice <= 50000000:
    #     boxoffice = 70
    # elif 50000001 < boxoffice <= 75000000:
    #     boxoffice = 80
    # elif 75000001 < boxoffice <= 100000000:
    #     boxoffice = 90
    # elif 100000001 < boxoffice <= 11500000:
    #     boxoffice = 100
    # else:
    #     boxoffice = 100
    # endregion

    print(f'{checked_counter}/2000 Progress ({title_id}): \033[32m[done]\033[0m {url}')
    data = {'id': title_id,
            'name': title,
            'url': url,
            'year': year,
            'budget': budget,
            'duration': duration,
            'genre': genre,
            'age-limit': age_limit,
            'franchise': franchise,
            'release-season': season,
            'holiday': holiday,
            'director-rating': director_rating,
            'directors-awards': directors_awards,
            'writers-awards': writers_awards,
            'stars-awards': stars_awards,
            'oscars': oscars,
            'writers': writers,
            'stars': stars,
            'box-office': boxoffice}

    checked_counter += 1
    return data


# Функция для проведения теста
def test():
    print(f'\033[31m\033[40m--- TEST PROCESSING ---\033[0m\n')
    # for item_id in ('0468569'): #ids:'10366460',
    test_set = parser('0477348')
    # print(f'{test_set["budget"]};{test_set["duration"]};{test_set["genre"]};{test_set["age-limit"]};'
    #       f'{test_set["franchise"]};{test_set["release-season"]};{test_set["holiday"]};{test_set["director-rating"]};'
    #       f'{test_set["directors-awards"]};{test_set["writers-awards"]};{test_set["stars-awards"]};'
    #       f'{test_set["oscars"]};{test_set["box-office"]}\n')
    print(f'ID:{test_set["id"]}\t{test_set["year"]}\t{test_set["name"]}\n'
          f'\tbudget:           {test_set["budget"]}\n'
          f'\tduration:         {test_set["duration"]}\n'
          f'\tgenre:            {test_set["genre"]}\n'
          f'\tage-limit:        {test_set["age-limit"]}\n'
          f'\tfranchise:        {test_set["franchise"]}\n'
          f'\trelease-season:   {test_set["release-season"]}\n'
          f'\tholiday:          {test_set["holiday"]}\n'
          f'\tdirector-rating:  {test_set["director-rating"]}\n'
          f'\tdirectors-awards: {test_set["directors-awards"]}\n'
          f'\twriters-awards:   {test_set["writers-awards"]}\n'
          f'\tstars-awards:     {test_set["stars-awards"]}\n'
          f'\toscars:           {test_set["oscars"]}\n'
          f'\tbox-office:       {test_set["box-office"]}\n')


if __name__ == '__main__':
    start_time = time.time()
    pool = Pool()  # создаем пул потоков для мультипоточности
    chrome_options = Options()
    chrome_options.add_argument("--disable-blink-features=AutomationControlled")
    # chrome_options.add_argument(r"user-data-dir=C:\\Users\\" + WINUSER + "\\AppData\\Local\\Google\\Chrome\\User Data")
    chrome_options.add_argument("--headless")
    driver = webdriver.Chrome(options=chrome_options)  # задаем значения для selenium

    # очистка предыдущего парсинга если надо
    open(f'./raw_dataset.txt', 'w').close()

    if not os.path.exists('./ids.txt'):
        raise SystemExit

    # test()

    counter = 0  # счетчик с какой строки начинать парсить
    while counter <= 2000:
        output_dataset, temp_dataset, temp_ids = [], [], []

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
            if temp_row["id"] in (0, '0', '', None):  # если id фильма 0 то пропускаем
                pass
            else:  # иначе записываем в dataset
                output_dataset.append(temp_row)

        with open(f'./raw_dataset.txt', 'a', encoding='utf-8') as dataset:
            for subrow in output_dataset:
                dataset.write(f'{subrow}\n')

        counter += 100

    temp_file.close()
    dataset.close()

    print(f'\033[7m--- END IN {round((time.time() - start_time), 3)} sec ---\033[0m')
