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

# open('dataset.txt', 'w').close()
open('dataset.xlsx', 'w').close()
# open('raw_data.txt', 'w').close()
open('raw_data.xlsx', 'w').close()


def scrapper(url):
    data = []
    start = 0
    iteration = 1

    while start < 1000:
        request = requests.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'html.parser')

        title_block = soup.find('div', class_='lister-list').find_all('div', class_='lister-item mode-advanced')
        for item in title_block:
            title = item.find('h3', class_='lister-item-header').find('a')
            title_id = title.get('href').replace('/title/tt', '').replace('/?ref_=adv_li_tt', '')
            data.append(title_id)
            print(
                f'{url.replace("https://www.imdb.com/search/title/?groups=", "").replace("&start=", "/").replace("&ref_=adv_nxt", "")}: '
                f'{item.find("span", class_="lister-item-index unbold text-primary").get_text()} {title_id}')

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
    data, release_date = [], []
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
        release_date.append(0)
        release_date.append(0)
        return release_date
    year = date[2]

    temp_season = datetime.datetime(2020, int(month), int(day))
    if datetime.datetime(2019, 11, 20) < temp_season < datetime.datetime(2020, 2, 20):
        release_date.append(4)
    elif datetime.datetime(2020, 2, 20) < temp_season < datetime.datetime(2020, 5, 20):
        release_date.append(1)
    elif datetime.datetime(2020, 5, 20) < temp_season < datetime.datetime(2020, 8, 20):
        release_date.append(2)
    elif datetime.datetime(2020, 8, 20) < temp_season < datetime.datetime(2020, 11, 20):
        release_date.append(3)
    elif datetime.datetime(2020, 11, 20) < temp_season < datetime.datetime(2021, 2, 20):
        release_date.append(4)
    else:
        release_date.append(0)

    release_date.append(datetime.datetime.strptime(f'{year}-{month}-{day}', '%Y-%m-%d').isoweekday())
    return release_date


def take_person_score(child_id):
    score, temp = 0, 0
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
            # score += ((100 - int(temp)) / 2) + 30 / 2

    num = len(child_id) if len(child_id) != 0 else 1
    return round(score / num)


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

    budget = []
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
            budget.append(calculate(temp_budget))
        else:
            budget.append(0)

        temp_box = soup.find('span', attrs={'data-wikidata-property-id': 'P2142'})
        if temp_box is not None:
            temp_box = temp_box.get_text()
            budget.append(calculate(temp_box))
        else:
            budget.append(0)

        franchise1 = soup.find('span', attrs={'data-wikidata-property-id': 'P155'})
        franchise2 = soup.find('span', attrs={'data-wikidata-property-id': 'P156'})
        if franchise1 is not None or franchise2 is not None:
            budget.append(1)
        else:
            budget.append(0)
    except (NoSuchElementException, StaleElementReferenceException):
        budget.append(0)
        budget.append(0)
        budget.append(0)
        return budget
        pass

    return budget


def parser(title_id):
    global checked_counter
    directors, writers, stars = [], [], []
    year, mpaa, duration, genre, season, popularity, budget, boxoffice, franchise = '', '', '', '', '', '', '', '', ''
    hour, minute, day, age_limit, country = '', '', '', '', ''

    agelimit_format = {'PG-13': 3, 'NC-17': 5, 'PG': 2, 'R': 4, 'G': 1, 'X': 6, '0': 1, '6': 2, '12': 3, '14': 4, '16': 5, '18': 6}
    genre_format = {'Action': 1, 'Adventure': 2, 'Drama': 3, 'Comedy': 4, 'Crime': 5,
                    'Romance': 6, 'Mystery': 7, 'Horror': 8, 'History': 9, 'Western': 10,
                    'Music': 11, 'Biography': 12, 'Musical': 13, 'Film-Noir': 14, 'Animation': 15,
                    'Fantasy': 16, 'Sci-Fi': 17, 'Thriller': 18, 'Family': 19, 'Short': 20,
                    'Sport': 21, 'War': 22, 'Reality-TV': 23, 'Game-Show': 24, 'Documentary': 25,
                    'Talk-Show': 26, 'News': 27, 'Adult': 28}
    country_check = ['United States', 'United Kingdom', 'Russia', 'Canada', 'France',
                     'South Korea', 'China', 'India', 'Nigeria', 'Germany', 'Australia', 'Brazil']
    country_format = {'United States': 1, 'United Kingdom': 2, 'Russia': 3, 'Canada': 4,
                      'France': 5, 'South Korea': 6, 'China': 7, 'India': 8,
                      'Nigeria': 9, 'Germany': 10, 'Australia': 11, 'Brazil': 12, }

    url = f'https://www.imdb.com/title/tt{str(title_id)}/'
    session = requests.Session()
    request = session.get(url, headers=headers)
    soup = BeautifulSoup(request.content, 'lxml')

    boxoffice_wiki = take_box_wiki(title_id)


    success = soup.find('li', class_='ipc-inline-list__item')
    title = soup.find('h1', class_='TitleHeader__TitleText-sc-1wu6n3d-0')
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

        genre = soup.find('a', class_='GenresAndPlot__GenreChip-sc-cum89p-3')
        if genre is not None:
            genre = genre_format[genre.get_text()]

        country = soup.find('div', attrs={'data-testid': 'title-details-section'})
        if country is not None:
            country = country.find('li', attrs={'data-testid': 'title-details-origin'})
            if country is not None:
                country = country.find('a', class_='ipc-metadata-list-item__list-content-item')
                if country is not None:
                    country = country_format[country.get_text()] if country.get_text() in country_check else 0
        else:
            country = 0

        release_date = convert_date(title_id)
        season = release_date[0]
        day = release_date[1]

        temp = 1
        credits_list = soup.find('div', attrs={'data-testid': 'title-pc-expanded-section'}).find_all('li')
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
    country = int(country) if country else 0
    season = int(season) if season else 0
    day = int(day) if int(day) else 0
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

    if boxoffice < 1000:
        title_id = 0

    budget = round((budget/1000000), 1)
    boxoffice = round((boxoffice/1000000), 1)

    if boxoffice >= (budget * 2):
        profitable = 3
    elif boxoffice >= (budget + (budget / 2)):
        profitable = 2
    elif boxoffice >= budget:
        profitable = 1
    else:
        profitable = 0

    print(title_id, title, url)
    data = {'id': title_id,
            'name': title,
            'url': url,
            'year': year,
            'age-limit': age_limit,
            'duration': duration,
            'release-country': country,
            'release-season': season,
            'release-day': day,
            'directors': directors,
            'writers': writers,
            'stars': stars,
            'genre': genre,
            'franchise': franchise,
            'imdb-popularity': popularity,
            'budget': budget,
            'box-office': boxoffice,
            'profitable': profitable}
    print(data)
    return data


if __name__ == '__main__':
    pool = Pool()
    chrome_options = Options()
    chrome_options.add_argument("--disable-blink-features=AutomationControlled")
    # chrome_options.add_argument(r"user-data-dir=C:\\Users\\" + WINUSER + "\\AppData\\Local\\Google\\Chrome\\User Data")
    chrome_options.add_argument("--headless")
    driver = webdriver.Chrome(options=chrome_options)
    raw_dataset = []
    ids = []
    dataset = []
    counter = 0

    # raw_dataset.append(scrapper(top_1000))
    # raw_dataset.append(scrapper(bottom_1000))

    # for k in raw_dataset[0]:
    #     ids.append(k)
    #
    # for k in raw_dataset[1]:
    #     ids.append(k)
    #
    # print(ids)
    # with open(f'ids.txt', 'w+', encoding='utf-8') as id_file:
    #     for item in ids:
    #         id_file.write(item + '\n')
    #
    # with open(f'raw_data.txt', 'w', encoding='utf-8') as set_file:
    #     set_file.write(f'url;id;name;year;duration;imdb-popularity;budget;box-office\n')
    # with open(f'dataset.txt', 'w', encoding='utf-8') as file:
    #     file.write(f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;X12;D1;D2\n')

    while counter <= 2000:
        # for item_id in ('10366460', '0468569'): #ids:
        #     dataset.append(parser(item_id))
        temp_ids = []

        with open(f'ids.txt', 'r', encoding='utf-8') as temp_file:
            for index, row in enumerate(temp_file, 1):
                if counter <= index < counter + 100:
                    temp_ids.append(row.replace('\n', ''))

        print(1, counter)

        try:
            dataset = pool.map(parser, temp_ids)
        except (TimeoutError, requests.exceptions.ConnectionError) as e:
            pass

        with open(f'raw_data.txt', 'a', encoding='utf-8') as set_file:
            for row in dataset:
                set_file.write(f'{row["url"]};{row["id"]};'
                               f'{row["name"]};{row["year"]};{row["duration"]};'
                               f'{row["imdb-popularity"]};{row["budget"]};{row["box-office"]}\n')

        with open(f'dataset.txt', 'a', encoding='utf-8') as file:
            for row in dataset:
                if row["id"] == 0:
                    pass
                else:
                    file.write(f'{row["age-limit"]};{row["duration"]};'
                               f'{row["release-country"]};{row["release-season"]};{row["release-day"]};'
                               f'{row["directors"]};{row["writers"]};{row["stars"]};'
                               f'{row["genre"]};{row["franchise"]};{row["imdb-popularity"]};'
                               f'{row["budget"]};{row["box-office"]};{row["profitable"]}\n')

        counter += 100
        
    temp_file.close()
    set_file.close()
    file.close()

    df0 = pandas.read_csv('raw_data.txt', sep=';')
    df0.to_excel('raw_data.xlsx', 'DATA', index=False)

    df1 = pandas.read_csv('dataset.txt', sep=';')
    df1.to_excel('dataset.xlsx', 'DATA', index=False)
