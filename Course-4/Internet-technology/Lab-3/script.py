# Импортируем нужные модули
from collections import Counter
from dateutil.relativedelta import relativedelta
from datetime import datetime
from urllib.request import urlretrieve
import matplotlib.pyplot as plt
import math
import os
import time
import vk

token = ''
session = vk.Session(access_token=token)
vkapi = vk.API(session, v='5.81')

group_1 = 'podslushanoperm'
group_2 = 'vikiperm'

photo_url = 'https://vk.com/album-132_47581240'

group_1_ages = []
group_2_ages = []
group_1_city = Counter
group_2_city = Counter
group_1_ids = []
group_2_ids = []

fig, ax = plt.subplots(3)


def GroupAnalysis(group_id, op):
    global group_1_ages, group_1_city, group_1_ids, group_2_ages, group_2_city, group_2_ids

    # print(vkapi.groups.getMembers(group_id=group_id, fields='sex,bdate,city', count=1000, sort='id_desc'))
    users = vkapi.groups.getMembers(group_id=group_id, fields='sex,bdate,city', count=1000, sort='id_desc')['items']
    sex = [i['sex'] for i in users]
    bdate = list(filter(lambda x: x is not None, [i['bdate'] if 'bdate' in i and len(i['bdate'].split('.')) == 3 else None for i in users]))
    ages = [relativedelta(datetime.now(), datetime.strptime(i, '%d.%m.%Y')).years for i in bdate]
    city_rate = Counter(list(filter(lambda x: x is not None, [i['city']['title'] if 'city' in i else None for i in users])))
    female_count = sex.count(1)
    male_count = len(sex) - female_count

    if op == 0:
        group_1_ages = ages
        group_1_city = city_rate
        group_1_ids = [i['id'] for i in users]
    elif op == 1:
        group_2_ages = ages
        group_2_city = city_rate
        group_2_ids = [i['id'] for i in users]

    print(f'{group_id} (https://vk.com/{group_id}):')
    print(f'{(female_count / 1000 * 100):.1f}% участников женского пола')
    print(f'{(male_count / 1000 * 100):.1f}% участников мужского пола')
    print(f'Топ 3 города: {city_rate.most_common(3)}')
    print(f'Средний возраст: {(sum(ages) / len(ages)):.1f}')


def DoPlot(op, text, city):
    global ax

    ax[op].set_title(text)
    top5 = city.most_common(3)
    labels = [i[0] for i in top5]
    values = [i[1] for i in top5]

    others = city
    for i in top5:
        others.pop(i[0])

    sum_others = sum(list(dict(others).values()))

    labels.append('Другие')
    values.append(sum_others)

    ax[op].pie(values, labels=labels)


def LoadPhotos():
    # Разбираем ссылку
    owner_id = photo_url.split('/')[-1].split('_')[0].replace('album', '')

    albums = vkapi.photos.getAlbums(owner_id=owner_id)

    if not os.path.exists('saved'):
        os.mkdir('saved')

    time_now = time.time()  # время старта

    counter_total = 0
    broken_total = 0

    for album in albums['items']:
        photo_folder = f'saved/album{owner_id}_{album["id"]}'
        if not os.path.exists(photo_folder):
            os.mkdir(photo_folder)
        photos_count = album['size']

        counter = 0  # текущий счетчик
        breaked = 0  # не загружено из-за ошибки
        prog = 0  # процент загруженных

        # Подсчитаем сколько раз нужно получать список фото, так как число получится не целое - округляем в большую сторону
        for j in range(math.ceil(photos_count / 1000)):
            # Получаем список фото
            photos = vkapi.photos.get(owner_id=owner_id, album_id=album['id'], count=1000, offset=j * 1000)
            for photo in photos['items']:
                counter += 1
                url = photo['sizes'][-1]['url']  # Получаем адрес изображения
                print(f"Загружаю фото № {counter} из {photos_count} альбома {album['id']}. Прогресс: {prog} %")
                prog = round(100 / photos_count * counter, 2)
                try:
                    # Загружаем и сохраняем файл
                    urlretrieve(url, photo_folder + "/" + os.path.split(url)[1].split('?')[0])
                except Exception:
                    print(url)
                    print('Произошла ошибка, файл пропущен.')
                    breaked += 1

        counter_total += counter
        broken_total += breaked

    time_for_dw = time.time() - time_now
    print(f'\nВ очереди было {counter_total} файлов. Из них удачно загружено {counter_total - broken_total} файлов, {broken_total} не удалось загрузить. Затрачено времени: {round(time_for_dw, 1)} сек.')


if __name__ == '__main__':
    print('\n-------------------------------------------------------------------------------------------------------\n')
    GroupAnalysis(group_1, 0)
    print('\n-------------------------------------------------------------------------------------------------------\n')
    GroupAnalysis(group_2, 1)
    print('\n-------------------------------------------------------------------------------------------------------\n')
    intersection = set(group_1_ids).intersection(set(group_2_ids))
    print(f'{len(intersection)} пользователей из 1000 ({(len(intersection) / len(group_1_ids) * 100):.1f}%) состоят в обеих группах')

    # plot 1
    ax[0].set_title('Сравнение среднего возраста')
    ax[0].bar([group_1, group_2], [sum(group_1_ages) / len(group_1_ages), sum(group_2_ages) / len(group_2_ages)])

    DoPlot(1, f'Топ 3 города {group_1}', group_1_city)
    DoPlot(2, f'Топ 3 города {group_2}', group_2_city)

    plt.show()
    print('\n-------------------------------------------------------------------------------------------------------\n')
    LoadPhotos()
