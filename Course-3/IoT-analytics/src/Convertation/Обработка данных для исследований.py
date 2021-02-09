# -*- coding: utf-8 -*-
"""
Created on Sun May  5 23:07:10 2019

@author: пк
"""
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np

#df = pd.read_excel('Данные для исследований.xlsx')
#df = df.set_index('Дата замера')

file_name = 'Данные для исследований.xlsx'
#xls = pd.ExcelFile('Данные для исследований.xlsx')
#print(xls.sheet_names) # имена листов в считанном файле
#print(len(xls.sheet_names)) # количество листов в считанном файле

# далее можно создать функции для получения данных 

def read_all_sheets(file_name_excel):
    df = pd.DataFrame()
    xls = pd.ExcelFile(file_name_excel)
    for list_excel in xls.sheet_names:
        df = df.append(pd.read_excel(xls, list_excel))
    return df

all_data = read_all_sheets(file_name)

# Чистим лишние пробулы в столбце "Скважина"
# Это классная команда для простого преобразования данных. Определяете словарь,
# в котором «ключами» являются старые значения, а «значениями» – новые значения:
cleaning_map = lambda x: str(x).strip()
all_data['Скважина'] = all_data['Скважина'].map(cleaning_map)

# Выбираем скважину
chosen_hole = all_data[all_data['Скважина']=='1р']
# Удаляем пустые столбцы
# print(DataFrame.dropna.__doc__)
chosen_hole.dropna(axis=1, how='all', inplace=True)


print(chosen_hole.info())


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
