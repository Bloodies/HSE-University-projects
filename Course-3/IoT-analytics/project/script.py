# -*- coding: utf-8 -*-

"""
Created on 0 0:00:00 0000

@author: Bloodies
"""

# !pip install influxdb

from influxdb import InfluxDBClient
from pytz import timezone
from openpyxl import Workbook
# from sklearn.linear_model import LinearRegression
# import statsmodels.api as sm
# import seaborn as sns
import matplotlib.ticker as ticker
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import csv
import datetime
import os
import shutil

first_hole = 1
last_hole = 100

date = 'Дата замера'
hole = 'Скважина'
x1 = 'Способ эксплуатации'
x2 = 'Режим'
y1 = 'Рпр(ТМ)'
y2 = 'Рзаб(Рпр)'
y3 = 'Рзаб(Нд)'
y4 = 'Рзаб(иссл)'

_host = 'localhost'
_port = 8086


def read_all_sheets(file_name_excel):
    df = pd.DataFrame()
    xls = pd.ExcelFile(file_name_excel)
    for list_excel in xls.sheet_names:
        df = df.append(pd.read_excel(xls, list_excel, parse_dates=[date], index_col=date))
    return df


def replace_text_values_in_x(df, nameX):
    dict_changes = {}
    _list = pd.unique(df[nameX]).tolist()
    i = 1
    for value in _list:
        if str(value) != str(np.NaN):
            df.loc[df[nameX] == value, nameX] = i
            dict_changes[i] = value
            i += 1
    df[nameX] = df[nameX].fillna(len(_list))
    dict_changes[len(_list)] = np.NaN
    return dict_changes


def convert_csv_to_xlsx(current_file):
    wb = Workbook()
    sheet = wb.active

    CSV_SEPARATOR = ";"

    with open(current_file + '.csv') as f:
        reader = csv.reader(f)
        for r, row in enumerate(reader):
            for c, col in enumerate(row):
                for idx, val in enumerate(col.split(CSV_SEPARATOR)):
                    cell = sheet.cell(row=r + 1, column=idx + 1)
                    cell.value = val

    wb.save(current_file + '.xlsx')


def separate_export(current_file, current_path):
    file_name = current_file

    df = read_all_sheets(file_name)
    df.sort_index(inplace=True)

    ''' Если появляется ошибка в данных способа эксплуатации и режиме заменить'''
    # what_replaced_x1 = replace_text_values_in_x(df, x1)
    # what_replaced_x2 = replace_text_values_in_x(df, x2)
    replace_text_values_in_x(df, x1)
    replace_text_values_in_x(df, x2)

    all_data = df.copy()

    if os.path.exists('holes/' + current_path):
        shutil.rmtree('holes/' + current_path)
        os.mkdir('holes/' + current_path)
    else:
        os.mkdir('holes/' + current_path)

    cleaning_map = lambda x: str(x).strip()
    all_data[hole] = all_data[hole].map(cleaning_map)
    all_df_to_influx = all_data.copy()[[hole, x1, x2, y1, y2, y3, y4]]

    influx_file_name = '_data_2018_01-07'
    list_of_holes = pd.unique(all_data[hole]).tolist()
    count_empty_data = 0
    list_empty_data = []
    cleaned_data = pd.DataFrame()
    for _hole in list_of_holes:
        df_to_influx = all_df_to_influx[all_df_to_influx[hole] == _hole][[x1, x2, y1, y2, y3, y4]]
        df_to_influx.insert(loc=0, column='Время', value=df_to_influx.index.time[0])
        temp_df = df_to_influx[[y1, y2, y3, y4]].dropna(axis=0, how='all')
        if not temp_df.empty:
            cleaned_data = cleaned_data.append(df_to_influx)
            df_to_influx.to_csv('holes/' + current_path + '/' + str(_hole) + influx_file_name + '.csv',
                                encoding='cp1251', sep=';')
        else:
            list_empty_data.append(_hole)
            count_empty_data += 1
    print('Количество скважен без данных по давлению: ' + str(count_empty_data))
    print('Список скважен без данных по давлению:')
    i = 1
    for _hole in list_empty_data:
        print(str(i) + '. ' + _hole)
        i += 1
    print('Сохранение в отдельные файлы ".csv" выполнено!')


def parcing_and_filling():
    file_name = 'data.xlsx'

    def read_all_sheets_f(file_name_excel):
        df = pd.DataFrame()
        xls = pd.ExcelFile(file_name_excel)
        for list_excel in xls.sheet_names:
            df = df.append(pd.read_excel(xls, list_excel, parse_dates=[date], index_col=date))
        return df

    df = read_all_sheets_f(file_name)
    df.sort_index(inplace=True)

    def replace_text_values_in_x_f(df, nameX):
        dict_changes = {}
        _list = pd.unique(df[nameX]).tolist()
        i = 1
        for value in _list:
            if str(value) != str(np.NaN):
                df.loc[df[nameX] == value, nameX] = i
                dict_changes[i] = value
                i += 1
        df[nameX] = df[nameX].fillna(len(_list))
        dict_changes[len(_list)] = np.NaN
        return dict_changes

    ''' Если появляется ошибка в данных способа эксплуатации и режиме заменить'''
    # what_replaced_x1 = replace_text_values_in_x_f(df, x1)
    # what_replaced_x2 = replace_text_values_in_x_f(df, x2)
    replace_text_values_in_x_f(df, x1)
    replace_text_values_in_x_f(df, x2)

    all_data = df.copy()

    cleaning_map = lambda x: str(x).strip()
    all_data[hole] = all_data[hole].map(cleaning_map)
    all_df_to_influx = all_data.copy()[[hole, x1, x2, y1, y2, y3, y4]]

    list_of_holes = pd.unique(all_data[hole]).tolist()
    count_empty_data = 0
    fill_method = 'bfill'
    list_empty_data = []
    cleaned_data = pd.DataFrame()
    for _hole in list_of_holes[first_hole:last_hole]:
        if count_empty_data > 0:
            fill_method = 'ffill'
        df_to_influx = all_df_to_influx[all_df_to_influx[hole] == _hole][[x1, x2, y1, y2, y3, y4]]
        df_to_influx.insert(loc=0, column='Время', value=df_to_influx.index.time[0])
        df_to_influx.insert(loc=0, column='Скважина', value=_hole)
        temp_df = df_to_influx[[y1, y2, y3, y4]].dropna(axis=1, how='all')
        if not temp_df.empty:
            cleaned_data = cleaned_data.append(df_to_influx)
            cleaned_data[y1].fillna(method=fill_method, inplace=True)
            cleaned_data[y2].fillna(method=fill_method, inplace=True)
            cleaned_data[y2].fillna(method=fill_method, inplace=True)
            cleaned_data[y3].fillna(method=fill_method, inplace=True)
            cleaned_data[y4].fillna(method=fill_method, inplace=True)

            cleaned_data.to_csv('raw_holes.csv', encoding='cp1251', sep=';')
        else:
            list_empty_data.append(_hole)
            count_empty_data += 1


def INFLUX_INPUT(current_file, current_metric, cycle):
    index = 0
    _databasename = 'holes'
    timecolumn = 'Время'
    datecolumn = 'Дата замера'
    timeformat = '%Y-%m-%d %H:%M:%S'
    datatimezone = 'UTC'
    _delimiter = ';'
    batchsize = 5000
    inputfilename = current_file  # 'holes.csv' #'clear_holes.csv' #'normal_holes.csv'    
    metric = [current_metric]  # ['raw_data'] #['clean_data'] #['normal_data']
    tagcolumns = ['Скважина']
    fieldcolumns = []

    client = InfluxDBClient(host=_host, port=_port)
    client.create_database(_databasename)
    db_list = client.get_list_database()
    print('существующие базы данных' + db_list)

    if cycle == 0:
        client.drop_database(_databasename)  # client.drop_database(_databasename)
        client.create_database(_databasename)  # client.create_database(_databasename)

    client.switch_database(_databasename)

    epoch_naive = datetime.datetime.utcfromtimestamp(0)
    epoch = timezone('UTC').localize(epoch_naive)

    def unix_time_millis(dt):
        return int((dt - epoch).total_seconds() * 1000)

    """ Check if data type of field is float """

    def isfloat(value):
        try:
            float(value)
            return True
        except:
            return False

    """ Check if data type of field is int """

    def isinteger(value):
        try:
            if int(value):
                return True
            else:
                return False
        except:
            return False

    datapoints1 = []
    count = 0
    with open(inputfilename, 'r') as csvfile:
        reader = csv.DictReader(csvfile, delimiter=_delimiter)
        fieldcolumns = reader.fieldnames[2:]
        for row in reader:
            datetime_naive = datetime.datetime.strptime(row[datecolumn] + ' ' + row[timecolumn], timeformat)
            datetime_local = timezone(datatimezone).localize(datetime_naive)

            timestamp = unix_time_millis(datetime_local) * 1000000  # in nanoseconds
            tags = {}

            for t in tagcolumns:
                if t in row:
                    v = row[t]
                    pass
                tags[t] = v

            # fieldNames = {'Рзаб(Нд)', 'Рзаб(Рпр)'}
            fields = {}
            for f in fieldcolumns:
                v = 0
                if f in row:
                    if isinteger(row[f]):
                        v = int(row[f])
                    else:
                        v = float(row[f]) if isfloat(row[f]) else row[f]
                fields[f] = v

            datapoints1.append({"measurement": metric[0], "time": timestamp, "fields": fields, "tags": tags})
            count += 1

            if len(datapoints1) % batchsize == 0:
                index += 1
                print('Read %d lines' % count)
                print('Inserting %d datapoints...' % (len(datapoints1)))
                response = client.write_points(datapoints1)

                if not response:
                    print('Problem inserting points, exiting...')
                    exit(1)
                print("Wrote %d, response: %s" % (len(datapoints1), response))
                datapoints1 = []
        print(index)

    if len(datapoints1) > 0:
        print('Read %d lines' % count)
        print('Inserting %d datapoints...' % (len(datapoints1)))
        response = client.write_points(datapoints1)

        if not response:
            print('Problem inserting points, exiting...')
            exit(1)

        print("Wrote %d, response: %s" % (len(datapoints1), response))

    print('Done')


def cleaning():
    df = pd.read_csv('raw_holes.csv', sep=';', engine='python')
    q_low = df.quantile(.01)
    q_high = df.quantile(.99)
    print(q_low)
    print(q_high)

    df = df[(df['Режим'] >= q_low['Режим']) & (df['Режим'] <= q_high['Режим'])]
    df = df[(df['Рпр(ТМ)'] >= q_low['Рпр(ТМ)']) & (df['Рпр(ТМ)'] <= q_high['Рпр(ТМ)'])]
    df = df[(df['Рзаб(Рпр)'] >= q_low['Рзаб(Рпр)']) & (df['Рзаб(Рпр)'] <= q_high['Рзаб(Рпр)'])]
    df = df[(df['Рзаб(иссл)'] >= q_low['Рзаб(иссл)']) & (df['Рзаб(иссл)'] <= q_high['Рзаб(иссл)'])]

    df.to_csv('clean_holes.csv', index=False, sep=';', encoding='cp1251')


def normalization():
    data = pd.read_csv('clean_holes.csv', sep=';', engine='python')
    df = pd.DataFrame(data)

    print(f"DataFrame:\n{df}\n")
    print(f"column types:\n{df.dtypes}")

    holes_list = []

    col_List = df['Скважина'].tolist()
    num = 1
    for i in range(len(col_List)):
        if (i == 0) or (col_List[i] != col_List[i - 1]):
            holes_list.append(col_List[i])
            num += 1

        columns = ['Рпр(ТМ)', 'Рзаб(Рпр)', 'Рзаб(Нд)', 'Рзаб(иссл)']

    for hole in holes_list:
        df1 = df[lambda df: df['Скважина'] == hole]

        for col in columns:
            col_List = df1[col].tolist()

            for i in range(len(col_List)):
                delta = len(col_List) - i
                left = col_List[i - 1] + col_List[i - 2] + col_List[i - 3] + col_List[i - 4] + col_List[i - 5]
                if delta == 1:
                    right = col_List[0] + col_List[1] + col_List[2] + col_List[3] + col_List[4]
                if delta == 2:
                    right = col_List[i + 1] + col_List[0] + col_List[1] + col_List[2] + col_List[3]
                if delta == 3:
                    right = col_List[i + 1] + col_List[i + 2] + col_List[0] + col_List[1] + col_List[2]
                if delta == 4:
                    right = col_List[i + 1] + col_List[i + 2] + col_List[i + 3] + col_List[0] + col_List[1]
                if delta == 5:
                    right = col_List[i + 1] + col_List[i + 2] + col_List[i + 3] + col_List[i + 4] + col_List[0]
                if delta >= 6:
                    right = col_List[i + 1] + col_List[i + 2] + col_List[i + 3] + col_List[i + 4] + col_List[i + 5]
                col_List[i] = (left + right) / 10

            df1[col] = col_List
            df[lambda df: df['Скважина'] == hole] = df1

    max = df.max()
    df['Рпр(ТМ)'] = df['Рпр(ТМ)'].apply(lambda x: round(x / max['Рпр(ТМ)'], 3))
    df['Рзаб(Рпр)'] = df['Рзаб(Рпр)'].apply(lambda x: round(x / max['Рзаб(Рпр)'], 3))
    df['Рзаб(Нд)'] = df['Рзаб(Нд)'].apply(lambda x: round(x / max['Рзаб(Нд)'], 3))
    df['Рзаб(иссл)'] = df['Рзаб(иссл)'].apply(lambda x: round(x / max['Рзаб(иссл)'], 3))

    df.to_csv('normal_holes.csv', index=False, sep=';', encoding='cp1251')


def panels_export(current_file, current_path):
    file_name = current_file
    # xls = pd.ExcelFile('Данные для исследований.xlsx')
    # print(xls.sheet_names) # имена листов в считанном файле
    # print(len(xls.sheet_names)) # количество листов в считанном файле

    # df = pd.read_excel('Данные для исследований.xlsx')
    # df = df.set_index('Дата замера')

    df = read_all_sheets(file_name)
    # df_copy = df.copy()
    df.copy()
    df.sort_index(inplace=True)

    ''' Если появляется ошибка в данных способа эксплуатации и режиме заменить'''
    # what_replaced_x1 = replace_text_values_in_x(df, x1)
    # what_replaced_x2 = replace_text_values_in_x(df, x2)
    replace_text_values_in_x(df, x1)
    replace_text_values_in_x(df, x2)

    """ Здесь порядок другой!!!
    см. текущий порядок в возвращаемом словаре

    df.loc[df['Способ эксплуатации'] == 'Газлифт', 'Способ эксплуатации'] = 1
    df.loc[df['Способ эксплуатации'] == 'Фонтанный', 'Способ эксплуатации'] = 2
    df.loc[df['Способ эксплуатации'] == 'Электропогружным насосом', 'Способ эксплуатации'] = 3
    df.loc[df['Способ эксплуатации'] == 'По НКТ с пакером', 'Способ эксплуатации'] = 4
    df.loc[df['Способ эксплуатации'] == 'По НКТ с воронкой', 'Способ эксплуатации'] = 5
    df.loc[df['Способ эксплуатации'] == 'Установка электроцентробежная для подачи воды', 'Способ эксплуатации'] = 6
    df.loc[df['Способ эксплуатации'] == 'Прочие способы эксплуатации', 'Способ эксплуатации'] = 7
    df['Способ эксплуатации'] = df['Способ эксплуатации'].fillna(8)

    df.loc[df['Режим'] == 'АПВ', 'Режим'] = 1
    df.loc[df['Режим'] == 'ПДФ', 'Режим'] = 2
    df.loc[df['Режим'] == 'ПКВ', 'Режим'] = 3
    df['Режим'] = df['Режим'].fillna(4)
    """

    all_data = df.copy()

    if os.path.exists('panels/' + current_path):
        shutil.rmtree('panels/' + current_path)
        os.mkdir('panels/' + current_path)
    else:
        os.mkdir(current_path)

    # Чистим лишние пробулы в столбце "Скважина"
    # Это классная команда для простого преобразования данных. Определяете словарь,
    # в котором «ключами» являются старые значения, а «значениями» – новые значения:
    cleaning_map = lambda x: str(x).strip()
    all_data[hole] = all_data[hole].map(cleaning_map)

    all_data = all_data[[hole, x1, x2, y1, y2, y3, y4]]

    def draw_plot(name_hole, data_to_draw):
        formatter = ticker.FormatStrFormatter("%.0f")
        fig, axes = plt.subplots(1, 2)
        axes[0].plot(data_to_draw[y1], label=y1)
        axes[0].plot(data_to_draw[y2], label=y2)
        axes[0].plot(data_to_draw[y3], label=y3)
        axes[0].plot(data_to_draw[y4], label=y4)
        axes[0].legend()
        axes[0].set_title('Измерения давлений')
        axes[0].set_xlabel(date, fontsize=12)
        axes[0].set_ylabel('P', fontsize=12)
        axes[1].plot(data_to_draw[x1], label=x1)
        axes[1].plot(data_to_draw[x2], label=x2)
        axes[1].legend()
        axes[1].set_title(x1 + ' и ' + x2)
        axes[1].set_xlabel(date, fontsize=12)
        axes[1].set_ylabel('№', fontsize=12)
        axes[1].yaxis.set_major_formatter(formatter)
        fig.set_figwidth(15)
        fig.set_figheight(8)
        fig.suptitle(hole + ' ' + str(name_hole))
        fig.autofmt_xdate()
        plt.legend()
        # plt.show()
        plt.savefig('panels/' + current_path + '/gridJ_' + name_hole + '.png', dpi=100, format='png')

    list_of_holes = pd.unique(all_data[hole]).tolist()
    count_empty_data = 0
    list_empty_data = []
    cleaned_data = pd.DataFrame()
    for _hole in list_of_holes:
        df_to_draw = all_data[all_data[hole] == _hole][[x1, x2, y1, y2, y3, y4]]
        temp_df = df_to_draw[[y1, y2, y3, y4]].dropna(axis=0, how='all')
        if not temp_df.empty:
            cleaned_data = cleaned_data.append(df_to_draw)
            draw_plot(_hole, df_to_draw)
            """ Эта часть кода вынесена в функцию
            fig, axes = plt.subplots(1,2)
            axes[0].plot(df_to_draw[y1], label=y1)
            axes[0].plot(df_to_draw[y2], label=y2)
            axes[0].plot(df_to_draw[y3], label=y3)
            axes[0].plot(df_to_draw[y4], label=y4)
            axes[0].legend()
            axes[0].set_title('Измерения давлений')
            axes[1].plot(df_to_draw[x1], label=x1)
            axes[1].plot(df_to_draw[x2], label=x2)
            axes[1].set_title('Настройка')
            axes[1].legend()
            fig.set_figwidth(15)
            fig.set_figheight(8)
            plt.title(_hole)
            plt.show()
            """
        else:
            # print('Для скважины '+ _hole +' нет данных по давлению!')
            list_empty_data.append(_hole)
            count_empty_data += 1
    print('Количество скважен без данных по давлению: ' + str(count_empty_data))
    """
    print('Список скважен без данных по давлению:')
    i=1
    for _hole in list_empty_data:
        print(str(i)+'. '+_hole)
        i+=1
    """


if os.path.exists('holes'):
    separate_export('Данные для исследований.xlsx', '0_null_data_holes')
else:
    os.mkdir('holes')
    separate_export('Данные для исследований.xlsx', '0_null_data_holes')

if os.path.exists('panels'):
    panels_export('Данные для исследований.xlsx', '0_null_data_panels')
else:
    os.mkdir('panels')
    panels_export('Данные для исследований.xlsx', '0_null_data_panels')
print("вывод null data завершен")

parcing_and_filling()
print("данные raw_data заполены")
INFLUX_INPUT('raw_holes.csv', 'raw_data', 0)
print("вывод raw_data в Influx завершен")
convert_csv_to_xlsx('raw_holes')
print("raw_data еонвертирован")

separate_export('raw_holes.xlsx', '1_raw_data_holes')
panels_export('raw_holes.xlsx', '1_raw_data_panels')
print("вывод raw_data завершен")

cleaning()
print("данные clean_data заполены")
INFLUX_INPUT('clean_holes.csv', 'clean_data', 1)
print("вывод clean_data в Influx завершен")
convert_csv_to_xlsx('clean_holes')
print("clean_data еонвертирован")

separate_export('clean_holes.xlsx', '2_clean_data_holes')
panels_export('clean_holes.xlsx', '2_clean_data_panels')
print("вывод clean_data завершен")

normalization()
print("данные normal_data заполены")
INFLUX_INPUT('normal_holes.csv', 'normal_data', 1)
print("вывод normal_data в Influx завершен")
