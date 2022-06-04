import requests
import time
from bs4 import BeautifulSoup

headers = {'accept': '*/*',
           'user-agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36'}

top_1000 = 'https://www.imdb.com/search/title/?groups=top_1000'
bottom_1000 = 'https://www.imdb.com/search/title/?groups=bottom_1000'


def scrapper(url, rat):
    data = []       # Переменная для хранения id фильмов
    start = 0       # Счетчик обработанных фильмов
    iteration = 0   # Счетчик первой итерации

    while start < 1000:
        request = requests.get(url, headers=headers)            # Передаем ссылку в обработчик
        soup = BeautifulSoup(request.content, 'html.parser')    # Подключаемся к странице

        # Находим список фильмов на странице
        title_block = soup.find('div', class_='lister-list').find_all('div', class_='lister-item mode-advanced')
        for item in title_block:                                # Для каждого элемента в списке
            title = item.find('h3', class_='lister-item-header').find('a')                          # Находим элемент
            title_id = title.get('href').replace('/title/tt', '').replace('/?ref_=adv_li_tt', '')   # Находим id
            data.append(title_id)                               # Записываем id фильма в список
            print(
                f'{rat} / '
                f'{item.find("span", class_="lister-item-index unbold text-primary").get_text().replace(".", ":")} '
                f'id \033[32m{title_id}\033[0m')

        if start + 50 <= len(data):     # Переходим на след страницу
            if iteration == 0:          # Если первая итерация то добавляем необходимый текст к ссылке
                url = url + f'&start={str(start + 50 + 1)}&ref_=adv_nxt'
                iteration += 1
            else:                       # Иначе заменяем параметр страницы в запросе ссылки
                url = url.replace(f'&start={str(start + 1)}', f'&start={str(start + 50 + 1)}')
            start += 50
        else:
            # Иначе вывод ошибки
            # print('сломалось')
            break

    return data  # Возвращаем список id фильмов


if __name__ == '__main__':
    start_time = time.time()
    ids = []
    raw_dataset = [scrapper(top_1000, 'top_1000'),
                   scrapper(bottom_1000, 'bottom_1000')]

    for k in raw_dataset[0]:
        ids.append(k)  # записываем id топ 1000 лучших фильмов

    for k in raw_dataset[1]:
        ids.append(k)  # записываем id топ 1000 худших фильмов

    with open(f'./ids.txt', 'w+', encoding='utf-8') as id_file:
        for row in ids:
            id_file.write(f'{row}\n')  # записываем в файл

    print(f'\n\033[7m--- END IN {round((time.time() - start_time), 3)} sec ---\033[0m')
