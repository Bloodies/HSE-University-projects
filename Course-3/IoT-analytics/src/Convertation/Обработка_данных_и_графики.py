# -*- coding: utf-8 -*-
"""
Created on Sun May  5 23:07:10 2019

@author: пк
"""
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import matplotlib.ticker as ticker
import numpy as np

file_name = 'Данные для исследований.xlsx'
#xls = pd.ExcelFile('Данные для исследований.xlsx')
#print(xls.sheet_names) # имена листов в считанном файле
#print(len(xls.sheet_names)) # количество листов в считанном файле

#df = pd.read_excel('Данные для исследований.xlsx')
#df = df.set_index('Дата замера')


"""
    Переменные для обозначения имён колонок в выборке данных
"""

data = 'Дата замера'
hole = 'Скважина'
x1 = 'Способ эксплуатации'
x2 = 'Режим'
y1 = 'Рпр(ТМ)'
y2 = 'Рзаб(Рпр)'
y3 = 'Рзаб(Нд)'
y4 = 'Рзаб(иссл)'


def read_all_sheets(file_name_excel):
    df = pd.DataFrame()
    xls = pd.ExcelFile(file_name_excel)
    for list_excel in xls.sheet_names:
        df = df.append(pd.read_excel(xls, list_excel, parse_dates=[data], index_col=data))
    return df

df = read_all_sheets(file_name)

df_copy = df.copy()

df.sort_index(inplace=True)

def replace_text_values_in_x(df, nameX):
    dict_changes = {}
    _list = pd.unique(df[nameX]).tolist()
    i = 1
    for value in _list:
        if (str(value) != str(np.NaN)):
            df.loc[df[nameX] == value, nameX] = i
            dict_changes[i] = value
            i += 1
    df[nameX] = df[nameX].fillna(len(_list))
    dict_changes[len(_list)] = np.NaN
    return dict_changes

what_replaced_x1 = replace_text_values_in_x(df, x1)
what_replaced_x2 = replace_text_values_in_x(df, x2)

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

# Чистим лишние пробулы в столбце "Скважина"
# Это классная команда для простого преобразования данных. Определяете словарь,
# в котором «ключами» являются старые значения, а «значениями» – новые значения:
cleaning_map = lambda x: str(x).strip()
all_data[hole] = all_data[hole].map(cleaning_map)

all_data = all_data[[hole, x1, x2, y1, y2, y3, y4]]

def draw_plot(name_hole, data_to_draw):
    formatter = ticker.FormatStrFormatter("%.0f")
    fig, axes = plt.subplots(1,2)
    axes[0].plot(data_to_draw[y1], label=y1)
    axes[0].plot(data_to_draw[y2], label=y2)
    axes[0].plot(data_to_draw[y3], label=y3)
    axes[0].plot(data_to_draw[y4], label=y4)
    axes[0].legend()
    axes[0].set_title('Измерения давлений')
    axes[0].set_xlabel(data, fontsize=12)
    axes[0].set_ylabel('P', fontsize=12)
    axes[1].plot(data_to_draw[x1], label=x1)
    axes[1].plot(data_to_draw[x2], label=x2)
    axes[1].legend()
    axes[1].set_title(x1+' и '+x2)
    axes[1].set_xlabel(data, fontsize=12)
    axes[1].set_ylabel('№', fontsize=12)
    axes[1].yaxis.set_major_formatter(formatter)
    fig.set_figwidth(15)
    fig.set_figheight(8)
    fig.suptitle(hole + ' ' + str(name_hole))
    fig.autofmt_xdate()
    plt.legend()
    #plt.show()
    plt.savefig('diagrams/gridJ_'+ name_hole +'.png', dpi=100, format='png')

list_of_holes = pd.unique(all_data[hole]).tolist()
count_empty_data = 0
list_empty_data = []
cleaned_data = pd.DataFrame()
for _hole in list_of_holes:
    df_to_draw = all_data[all_data[hole] == _hole][[x1, x2, y1, y2, y3, y4]]
    temp_df = df_to_draw[[y1, y2, y3, y4]].dropna(axis=0, how='all')
    if (not temp_df.empty):
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
        #print('Для скважины '+ _hole +' нет данных по давлению!')
        list_empty_data.append(_hole)
        count_empty_data += 1
print('Количество скважен без данных по давлению: '+ str(count_empty_data))
"""
print('Список скважен без данных по давлению:')
i=1
for _hole in list_empty_data:
    print(str(i)+'. '+_hole)
    i+=1
"""

# Выбираем скважину
#chosen_hole = all_data[all_data['Скважина']=='1р']
# Удаляем пустые столбцы
# print(DataFrame.dropna.__doc__)
#chosen_hole.dropna(axis=1, how='all', inplace=True)


#print(chosen_hole.info())

#ex = all_data.copy().dropna()
#print(ex.info())

#print(all_data.info())







#df_jan_2018 = pd.read_excel(xls, '012018')
#df_feb_2018 = pd.read_excel(xls, '022018')
#df_mar_2018 = pd.read_excel(xls, '032018')
#df_apr_2018 = pd.read_excel(xls, '042018')
#df_may_2018 = pd.read_excel(xls, '052018')
#df_jun_2018 = pd.read_excel(xls, '062018')
#df_jul_2018 = pd.read_excel(xls, '072018')

#print(df_jan_2018.head())
#print(df_feb_2018.head())
#print(df_mar_2018.head())
#print(df_apr_2018.head())
#print(df_may_2018.head())
#print(df_jun_2018.head())
#print(df_jul_2018.head())

#df_new=df[['Скважина', 'Тип ЗУ', 'Рзаб(Рпр)' , 'Рзаб(Нд)', 'Рзаб(иссл)', 'Рпр(ТМ)']]

#df.plot(subplots = True)
#plt.show()
