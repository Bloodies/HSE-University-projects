import numpy
import os
import pandas
from openpyxl import Workbook
import matplotlib.pyplot as plt
import seaborn as sns
#plt.style.use('ggplot')

open('./dataset.xlsx', 'w').close()
open('./sample.xlsx', 'w').close()

indicator = 1
# files = ['all',
#          '0-10',
#          '10-40',
#          '40-inf']


def del_copy(obj):  # удаляем копии из списка
    n = []
    for i in obj:
        if i not in n:
            n.append(i)
    return n


def convert_dataset(data):
    converted = []
    for row in data:
        converted_row = []


    return converted


# Удаляем выбросы
def del_ejects(filename):
    global indicator
    df = pandas.read_csv(f'./{filename}.txt', sep=';')

    plt.rcParams["figure.figsize"] = [7.50, 3.50]
    plt.rcParams["figure.autolayout"] = True

    if indicator in (1, 10):
        plt.clf()
        print(filename, indicator)
        data = pandas.DataFrame({'Бюджет': df['budget'], 'Кассовые сборы': df['boxoffice']})
        ax = data[['Бюджет', 'Кассовые сборы']].plot(kind='box', title='boxplot')
        plt.show()

    for x in ['budget', 'boxoffice']:
        q75, q25 = numpy.percentile(df.loc[:, x], [75, 25])     # Находим перцентели
        intr_qr = q75 - q25

        maxq = q75 + (1.5 * intr_qr)                            # Определяем квантили
        minq = q25 - (1.5 * intr_qr)

        df.loc[df[x] < minq, x] = numpy.nan                     # Заменяем на NaN
        df.loc[df[x] > maxq, x] = numpy.nan

    #print(df.isnull().sum())
    df.isnull().sum()
    df = df.dropna(axis=0)                                      # Удаляем NaN

    df.budget = df.budget.astype(numpy.int32)
    df.boxoffice = df.boxoffice.astype(numpy.int32)
    df.to_csv(f'./{filename}.txt', index=False, sep=';')
    indicator += 1


def show_heatmap(filename):
    subrow_sum = 0
    with open(f'./{filename}.txt', 'r', encoding='utf-8') as lfile:
        for lrow in lfile:
            subrow_sum += 1

    plt.clf()
    df = pandas.read_csv(f'./{filename}.txt', sep=';')
    cols = df.columns[:round(subrow_sum/5)]
    sns.heatmap(df[cols].isnull())
    plt.show()

    plt.clf()
    sns.heatmap(df[cols])
    plt.show()

    # plt.clf()
    # df_m = df.copy()
    # df_m['budget'] = [i.budget for i in df.index]
    # df_m['boxoffice'] = [i.boxoffice for i in df.index]
    # df_m = df_m.groupby(['budget', 'boxoffice']).mean()
    # fig, ax = plt.subplots(figsize=(11, 9))
    # sns.heatmap(df_m)
    # plt.show()

    plt.clf()
    gr = df.groupby(['budget', 'boxoffice'])
    gr = gr.nunique()
    gr.head()
    # dt_tweet_cnt = gr.loc[:, :, 'realDonaldTrump'].reset_index().pivot(index='hour_utc', columns='minute_utc',
    #                                                                           values='id')
    # dt_tweet_cnt.fillna(0, inplace=True)
    # dt_tweet_cnt = dt_tweet_cnt.reindex(range(0, 24), axis=0, fill_value=0)
    # dt_tweet_cnt = dt_tweet_cnt.reindex(range(0, 60), axis=1, fill_value=0).astype(int)
    # sns.heatmap(df[cols])
    plt.show()


def save(data):
    header = f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;X12;D1\n'
    sample_frow = (f'Бюджет;'
                   f'Длительность;'
                   f'Жанр;'
                   f'Возрастное ограничение;'
                   f'Наличие франшизы;'
                   f'Сезон выхода;'
                   f'Период высокой посещаемость;'
                   f'Рейтинг режиссера;'
                   f'Режиссер престижные награды;'
                   f'Сценаристы престижные награды;'
                   f'Актеры престижные награды;'
                   f'Оскары;'
                   f'Кассовые сборы\n')
    with open(f'./input_dataset.txt', 'w', encoding='utf-8') as file_dataset:
        file_dataset.write(f'budget;duration;genre;mpaa;franchise;season;holiday;'
                           f'dir_rating;dir_rewards;wr_rewards;act_rewards;oscars;boxoffice\n')
    with open(f'./dataset.txt', 'w', encoding='utf-8') as file_sdataset:
        file_sdataset.write(header)
    with open(f'./dataset_sample.txt', 'w', encoding='utf-8') as file_sdataset:
        file_sdataset.write(header)
        file_sdataset.write(sample_frow)
    with open(f'./temp_dataset.txt', 'w', encoding='utf-8') as temp_dataset:
        temp_dataset.write(header)
    with open(f'./temp_dataset_vt.txt', 'w', encoding='utf-8') as temp_tvdataset:
        temp_tvdataset.write(header)
    with open(f'./temp_dataset_valid.txt', 'w', encoding='utf-8') as temp_vdataset:
        temp_vdataset.write(header)
    with open(f'./temp_dataset_test.txt', 'w', encoding='utf-8') as temp_tdataset:
        temp_tdataset.write(header)

    with open(f'./input_dataset.txt', 'a', encoding='utf-8') as file:
        for row in data:
            file.write(f'{row["budget"]};{row["duration"]};{row["genre"]};{row["age-limit"]};{row["franchise"]};'
                       f'{row["release-season"]};{row["holiday"]};{row["director-rating"]};'
                       f'{row["directors-awards"]};{row["writers-awards"]};{row["stars-awards"]};'
                       f'{row["oscars"]};{row["box-office"]}\n')

    for repeat in range(10):  # Удаляем выбросы
        del_ejects(f'input_dataset')

    row_sum = 0
    input_dataset = []
    with open(f'./input_dataset.txt', 'r', encoding='utf-8') as lfile:
        for lrow in lfile:
            input_dataset.append(lrow)
            row_sum += 1

    with open(f'./dataset.txt', 'a', encoding='utf-8') as file:
        for row in input_dataset:
            file.write(row)

    with open(f'./dataset_sample.txt', 'a', encoding='utf-8') as file:
        for row in input_dataset:
            file.write(row)

    row_counter = 0
    dt_val, dt_test, dt_temp, dt_atemp = [], [], [], []
    with open(f'./input_dataset.txt', 'r', encoding='utf-8') as mfile:
        for mrow in mfile:
            if row_counter != 0:
                if row_counter % (round(row_sum / ((row_sum * 15) / 100))) == 0:
                    dt_val.append(mrow)
                else:
                    dt_temp.append(mrow)
            row_counter += 1
    mfile.close()

    row_counter = 0
    row_sum = round(((row_sum * 75) / 100))
    for nrow in dt_temp:
        if row_counter != 0:
            if row_counter % (round(row_sum / ((row_sum * 15) / 100)) + 2) == 0:
                dt_test.append(nrow)
            else:
                dt_atemp.append(nrow)
        row_counter += 1

    with open(f'./temp_dataset.txt', 'a', encoding='utf-8') as ofile:
        for orow in dt_atemp:
            ofile.write(orow)
    with open(f'./temp_dataset_vt.txt', 'a', encoding='utf-8') as pfile:
        for prow in dt_val:
            pfile.write(prow)
        for prow in dt_test:
            pfile.write(prow)
    with open(f'./temp_dataset_valid.txt', 'a', encoding='utf-8') as qfile:
        for qrow in dt_val:
            qfile.write(qrow)
    with open(f'./temp_dataset_test.txt', 'a', encoding='utf-8') as rfile:
        for rrow in dt_test:
            rfile.write(rrow)
    ofile.close()
    pfile.close()

    show_heatmap(f'input_dataset')

    # region Делим созданные фалы на группы и записываем в excel
    group_dataset = pandas.read_csv(f'./dataset.txt', sep=';')
    group_sample = pandas.read_csv(f'./dataset_sample.txt', sep=';')
    group_data = pandas.read_csv(f'./temp_dataset.txt', sep=';')
    group_vt = pandas.read_csv(f'./temp_dataset_vt.txt', sep=';')
    group_valid = pandas.read_csv(f'./temp_dataset_valid.txt', sep=';')
    group_test = pandas.read_csv(f'./temp_dataset_test.txt', sep=';')

    # Подготавливаем листы к записи
    sheets_sample = {'SAMPLE': group_sample, 'DATA': group_data,
                     'VT': group_vt, 'VALID': group_valid, 'TEST': group_test}

    writer = pandas.ExcelWriter(f'./dataset.xlsx', engine='openpyxl')

    for sheet_name in sheets_sample.keys():  # Записываем каждый документ в отдельный лист
        sheets_sample[sheet_name].to_excel(writer, sheet_name=sheet_name, engine='openpyxl', index=False)

    writer.save()

    sheets = {'DATA': group_dataset}

    writer = pandas.ExcelWriter(f'./sample.xlsx', engine='openpyxl')

    for sheet_name in sheets.keys():
        sheets[sheet_name].to_excel(writer, sheet_name=sheet_name, engine='openpyxl', index=False)

    writer.save()
    # endregion

    # region Удаляем временные txt файлы
    if os.path.exists(f'./dataset.txt'):
        os.remove('./dataset.txt')
    if os.path.exists(f'./dataset_sample.txt'):
        os.remove('./dataset_sample.txt')
    if os.path.exists(f'./temp_dataset.txt'):
        os.remove('./temp_dataset.txt')
    if os.path.exists(f'./temp_dataset_vt.txt'):
        os.remove('./temp_dataset_vt.txt')
    if os.path.exists(f'./temp_dataset_valid.txt'):
        os.remove('./temp_dataset_valid.txt')
    if os.path.exists(f'./temp_dataset_test.txt'):
        os.remove('./temp_dataset_test.txt')
    # endregion


def save_v2():
    header = (f'X1;X2;X3;X4;X5;X6;X7;X8;X9;X10;X11;X12;X13;X14;X15;X16;'
              f'X17;X18;X19;X20;X21;X22;X23;X24;X25;X26;X27;X28;X29;X30;D1\n')
    sample_frow = (f'Бюджет;'
                   f'Длительность;'
                   f'Боевик;'
                   f'Приключение;'
                   f'Драма;'
                   f'Комедия;'
                   f'Криминальная;'
                   f'Мистика;'
                   f'Ужасы;'
                   f'Исторический;'
                   f'Анимация;'
                   f'Файнтастика;'
                   f'Триллер;'
                   f'Мюзикл;'
                   f'0+;'
                   f'6+;'
                   f'12+;'
                   f'16+;'
                   f'18+;'
                   f'Наличие франшизы;'
                   f'Зима;'
                   f'Весна;'
                   f'Лето;'
                   f'Осень;'
                   f'Период высокой посещаемость;'
                   f'Рейтинг режиссера;'
                   f'Режиссер престижные награды;'
                   f'Сценаристы престижные награды;'
                   f'Актеры престижные награды;'
                   f'Оскары;'
                   f'Кассовые сборы\n')
    with open(f'./dataset_sample.txt', 'w', encoding='utf-8') as f:
        f.write(header)
        f.write(sample_frow)


if __name__ == '__main__':
    versions = ['D1', 'D2', '']
    dataset, converted_dataset = [], []

    with open(f'./raw_dataset.txt', 'r', encoding='utf-8') as temp_fdata:
        for irow in temp_fdata:
            dataset.append(eval(irow.replace('\n', '')))

    dataset = del_copy(dataset)  # удаляем копии
    converted_dataset = convert_dataset(dataset)

    # записываем заголовок
    with open(f'./raw_data.txt', 'w', encoding='utf-8') as set_file:
        set_file.write(f'url;id;name;year;duration;imdb-popularity;budget;box-office\n')

    with open(f'./raw_data.txt', 'a', encoding='utf-8') as j_file:
        for jrow in dataset:
            j_file.write(f'{jrow["url"]};{jrow["id"]};{jrow["name"]};{jrow["duration"]};'
                         f'{jrow["genre"]};{jrow["age-limit"]};{jrow["franchise"]};'
                         f'{jrow["release-season"]};{jrow["holiday"]};{jrow["director-rating"]};'
                         f'{jrow["directors-awards"]};{jrow["writers-awards"]};{jrow["stars-awards"]};'
                         f'{jrow["oscars"]};{jrow["budget"]};{jrow["box-office"]}\n')

    save(dataset)

    # temp_fdata.close()
    # j_file.close()

    # os.remove('./raw_data.txt')
