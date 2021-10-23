# pip install -r requirements.txt

import re
import requests
from bs4 import BeautifulSoup
from datetime import datetime, timedelta

api_key = r''

url_1 = r'https://ru.wikipedia.org/w/index.php?title=JSON&action=history'
# url_2 = r'https://api.hh.ru/vacancies?industry=7&per_page=10&page=0'
url_2 = r'https://api.hh.ru/vacancies'
url_3 = r'https://www.booking.com/searchresults.ru.html?label=gen173nr-1DCAEoggI46AdIM1gEaMIBiAEBmAEhuAEXyAEM2AED6AEBiAIBqAIDuAKz486LBsACAdICJDE3ZDhlZmZhLWI1ZTAtNDE3Yi04ODQxLTU0ODMxYjA2MzQzNNgCBOACAQ&sid=15910ac4ef1e864c27fdd2e87e03835b&sb=1&sb_lp=1&src=index&src_elem=sb&error_url=https%3A%2F%2Fwww.booking.com%2Findex.ru.html%3Flabel%3Dgen173nr-1DCAEoggI46AdIM1gEaMIBiAEBmAEhuAEXyAEM2AED6AEBiAIBqAIDuAKz486LBsACAdICJDE3ZDhlZmZhLWI1ZTAtNDE3Yi04ODQxLTU0ODMxYjA2MzQzNNgCBOACAQ%3Bsid%3D15910ac4ef1e864c27fdd2e87e03835b%3Bsb_price_type%3Dtotal%3Bsig%3Dv10it45QJ4%26%3B&ss=%D0%9F%D0%B5%D1%80%D0%BC%D1%8C&is_ski_area=0&ssne=%D0%9F%D0%B5%D1%80%D0%BC%D1%8C&ssne_untouched=%D0%9F%D0%B5%D1%80%D0%BC%D1%8C&dest_id=-2980155&dest_type=city&checkin_year=2021&checkin_month=10&checkin_monthday=30&checkout_year=2021&checkout_month=10&checkout_monthday=31&group_adults=1&group_children=0&no_rooms=1&b_h4u_keep_filters=&from_sf=1&order=price&offset=0'
headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36'}


def Task_1():
    regex = r'(?<!:)([0-9a-fA-F]{4}(:[0-9a-fA-F]{4}){7})(?!:)|(\b(?:(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(?:\.|$)){4}\b)'

    request = requests.get(url_1, headers=headers)
    soup = BeautifulSoup(request.text, 'html.parser')

    ip_list = []
    country = []
    tree_elements = soup.find_all('span', class_='history-user')

    print('Парсинг сайта')
    for element in tree_elements:
        match = re.search(regex, element.find('a', class_='mw-userlink').get_text().strip())
        if match is not None:
            # print(match.group())
            ip_list.append(match.group())

    # print(ip_list)
    # print(set(ip_list))
    print('Поиск стран по ip')
    for ip in set(ip_list):
        request = requests.get(r'http://api.ipstack.com/' + ip + '?access_key=' + api_key)
        obj = request.json()
        # print(obj["country_name"])
        country.append(obj['country_name'])

    print('---------------')
    dictionary = {i: country.count(i) for i in country}
    for w in sorted(dictionary, key=dictionary.get, reverse=True):
        print(w, dictionary[w])


def Task_2():
    page = 0
    pages = 0

    low_count = 0
    low_price = 0

    high_count = 0
    high_price = 0

    while True:
        '''
        https://github.com/hhru/api/tree/master/docs
        https://api.hh.ru/areas
        Россия - 113
        Пермь - 72
        Пермский край - 1317
        Москва - 1
        '''
        params = {
            'text': 'NAME:Python',
            # 'area': 1,
            'page': page,
            'per_page': 100
        }
        request = requests.get(url_2, params).json()

        for item in request.items():
            if item[0] == 'items':
                for sub_item in item[1]:
                    # print(sub_item['name'])
                    # if sub_item['address'] is not None:
                    #     print(sub_item['address']['city'])
                    if sub_item['salary'] is not None:
                        # print(sub_item['salary']['from'], sub_item['salary']['to'])
                        if sub_item['salary']['from'] is not None:
                            low_price += sub_item['salary']['from']
                            low_count += 1
                        if sub_item['salary']['to'] is not None:
                            high_price += sub_item['salary']['to']
                            high_count += 1
            elif item[0] == 'pages':
                pages = item[1]

        print(f'анализ {page + 1} страницы из {pages}')

        if (request['pages'] - page) <= 1:
            break
        else:
            page += 1

    print(
        f'Средняя зарплата Python разработчиков от {round(low_price / low_count)} до {round(high_price / high_count)}')
    # request = requests.get(url_2)
    # pprint(request.json())


def Task_3():
    end = False
    price = 0
    offset = 0
    today = datetime.strptime(datetime.today().strftime('%Y-%m-%d'), '%Y-%m-%d')
    saturday_delta = timedelta((7 + 5 - today.weekday()) % 7)
    sunday_delta = timedelta((7 - 1 - today.weekday()) % 7)
    url = url_3.replace('checkin_year=2021', 'checkin_year=' + str((today + saturday_delta).strftime('%Y'))) \
        .replace('checkin_month=10', 'checkin_month=' + str((today + saturday_delta).strftime('%m'))) \
        .replace('checkin_monthday=30', 'checkin_monthday=' + str((today + saturday_delta).strftime('%d'))) \
        .replace('checkout_year=2021', 'checkout_year=' + str((today + sunday_delta).strftime('%Y'))) \
        .replace('checkout_month=10', 'checkout_month=' + str((today + sunday_delta).strftime('%m'))) \
        .replace('checkout_monthday=31', 'checkout_monthday=' + str((today + sunday_delta).strftime('%d')))

    while True:

        request = requests.get(url, headers=headers)
        soup = BeautifulSoup(request.content, 'html.parser')

        hotelBlock = soup.find_all('div',
                                   class_='_fe1927d9e _0811a1b54 _a8a1be610 _022ee35ec b9c27d6646 fb3c4512b4 fc21746a73')

        print("\n----------------------------------------")
        for hotel in hotelBlock:
            if hotel is not None:
                print(hotel.find('div', class_='fde444d7ef _c445487e2').get_text(),
                      hotel.find("span", class_="fde444d7ef _e885fdc12").get_text())
                price += int(
                    hotel.find('span', class_='fde444d7ef _e885fdc12').get_text().replace(' ', '').replace('руб.', ''))
        print("----------------------------------------")

        elements = re.search(r'\d+', soup.find('h1', class_='_30227359d _0db903e42').get_text()).group()

        if offset + 25 <= int(elements) and end is False:
            print(f'Элементы {offset}-{offset + 25} из {elements}')
            offset += 25
            url = url.replace(('&offset=' + str(offset - 25)), ('&offset=' + str(offset)))
        elif offset + 25 == int(elements) and end is False:
            print(f'Элементы {offset}-{elements} из {elements}')
            url = url.replace(('&offset=' + str(offset - 25)), ('&offset=' + str(int(elements) - 1)))
            end = True
        elif offset > int(elements) and end is False:
            print(f'Элементы {offset}-{elements} из {elements}')
            break
        else:
            print(f'Элементы {offset}-{elements} из {elements}')
            break

    print(f"Средняя стоимость проживания с {(today + saturday_delta).strftime('%d.%m.%Y')} по {(today + sunday_delta).strftime('%d.%m.%Y')} в Перми = {round(price / int(elements))} руб.")


if __name__ == '__main__':
    # Task_1()
    # Task_2()
    Task_3()
