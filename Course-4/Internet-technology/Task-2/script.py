#pip install -r requirements.txt

import requests
import re
from pprint import pprint
from bs4 import BeautifulSoup

api_key = r""

url_1 = r"https://ru.wikipedia.org/w/index.php?title=JSON&action=history"
url_2 = r"https://api.hh.ru/vacancies?industry=7&per_page=10&page=0"
url_3 = r"https://www.booking.com/searchresults.ru.html?label=gen173nr-1FCAEoggI46AdIM1gEaMIBiAEBmAEhuAEXyAEU2AEB6AEB-AELiAIBqAIDuAKgxYXuBcACAQ&sid=bc5edb21df74621477bf9c71bcf97f49&sb=1&src=index&src_elem=sb&error_url=https%3A%2F%2Fwww.booking.com%2Findex.ru.html%3Flabel%3Dgen173nr-1FCAEoggI46AdIM1gEaMIBiAEBmAEhuAEXyAEU2AEB6AEB-AELiAIBqAIDuAKgxYXuBcACAQ%3Bsid%3Dbc5edb21df74621477bf9c71bcf97f49%3Bsb_price_type%3Dtotal%26%3B&ss=%D0%9F%D0%B5%D1%80%D0%BC%D1%8C%2C+%D0%9F%D0%B5%D1%80%D0%BC%D1%81%D0%BA%D0%B8%D0%B9+%D0%BA%D1%80%D0%B0%D0%B9%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F&is_ski_area=&ssne=%D0%9B%D0%BE%D0%BD%D0%B4%D0%BE%D0%BD&ssne_untouched=%D0%9B%D0%BE%D0%BD%D0%B4%D0%BE%D0%BD&checkin_year=2019&checkin_month=11&checkin_monthday=9&checkout_year=2019&checkout_month=11&checkout_monthday=10&group_adults=2&group_children=0&no_rooms=1&b_h4u_keep_filters=&from_sf=1&ss_raw=gthvm&ac_position=0&ac_langcode=ru&ac_click_type=b&dest_id=-2980155&dest_type=city&iata=PEE&place_id_lat=58.01496&place_id_lon=56.24672&search_pageview_id=089a5390e2b60096&search_selected=true&search_pageview_id=089a5390e2b60096&ac_suggestion_list_length=5&ac_suggestion_theme_list_length=0"

headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36'}


def Task_1():
    regex = r'(?<!:)([0-9a-fA-F]{4}(:[0-9a-fA-F]{4}){7})(?!:)|(\b(?:(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(?:\.|$)){4}\b)'
    reg = re.compile(regex)

    request = requests.get(url_1, headers=headers)
    soup = BeautifulSoup(request.text, "html.parser")

    ip_list = []
    country = []
    tree_elements = soup.find_all('span', class_='history-user')

    print("Парсинг сайта")
    for element in tree_elements:
        match = re.search(regex, element.find('a', class_='mw-userlink').get_text().strip())
        if match is not None:
            # print(match.group())
            ip_list.append(match.group())

    # print(ip_list)
    # print(set(ip_list))
    print("Поиск стран по ip")
    for ip in set(ip_list):
        request = requests.get(r"http://api.ipstack.com/" + ip + "?access_key=" + api_key)
        obj = request.json()
        # print(obj["country_name"])
        country.append(obj["country_name"])

    print("---------------")
    dictionary = {i: country.count(i) for i in country}
    for w in sorted(dictionary, key=dictionary.get, reverse=True):
        print(w, dictionary[w])


def Task_2():
    request = requests.get(url_2)
    pprint(request.json())


def Task_3():
    while True:

        request = requests.get(url_3, headers=headers)
        soup = BeautifulSoup(request.content, "html.parser")

        hotelBlock = soup.find_all("div", {"class": "sr_item"})

        i = 0
        for hotel in hotelBlock:
            hotelName = hotel.find("span", {"class": "sr-hotel__name"}).get_text().strip()
            print(hotelName)

            print("\n Элементов на странице:" + " " + str(len(hotelBlock)))

            nextPage = soup.find("a", title="Next page")
            if nextPage is None:
                nextPage = soup.find("a", title="Следующая страница")

            if nextPage is not None:
                url = "https://www.booking.com/" + nextPage.attrs["href"]
                print("Обработка следующей страницы... \n")
            else:
                break

if __name__ == '__main__':
    Task_1()
    # Task_2()
    # Task_3()