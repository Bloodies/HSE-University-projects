# -*- coding: utf-8 -*-
"""
Created on Tue May  7 12:43:07 2019

@author: пк
"""

import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np

file_name = 'Данные для исследований.xlsx'
list_excel = '022018'
xls = pd.ExcelFile(file_name)
#df = pd.read_excel(xls, list_excel, parse_dates=['Дата замера'], index_col='Дата замера')
df = pd.read_excel(xls, list_excel)

cleaning_map = lambda x: str(x).strip()
df['Скважина'] = df['Скважина'].map(cleaning_map)

list_of_holes = pd.unique(df['Скважина']).tolist()

df.loc[df['Способ эксплуатации'] == 'Газлифт', 'Способ эксплуатации'] = 1
df.loc[df['Способ эксплуатации'] == 'Фонтанный', 'Способ эксплуатации'] = 2
df.loc[df['Способ эксплуатации'] == 'Электропогружным насосом', 'Способ эксплуатации'] = 3
df.loc[df['Способ эксплуатации'] == 'Прочие способы эксплуатации', 'Способ эксплуатации'] = 4
df['Способ эксплуатации'] = df['Способ эксплуатации'].fillna(5)

df.loc[df['Режим'] == 'АПВ', 'Режим'] = 1
df.loc[df['Режим'] == 'ПДФ', 'Режим'] = 2
df.loc[df['Режим'] == 'ПКВ', 'Режим'] = 3
df['Режим'] = df['Режим'].fillna(4)

df_copy = df.copy()[['Скважина', 'Режим', 'Способ эксплуатации', 'Рзаб(иссл)']]

#print(type(df_copy['Режим'][0]))
#print(df_copy.head(100))

new_df = pd.DataFrame()
for hole in list_of_holes:
    df_hole = df_copy[df_copy['Скважина']==hole]
    #df_hole.info()
    total_num = df_hole.shape[0]
    null_num = df_hole['Рзаб(иссл)'].isnull().sum()
    if total_num == null_num:
        # удалить все эти строки из всего датасета
        df_hole = df_hole.dropna()
    else:
        # проверить равенство режима и способа эксплуатации
        pass
        #for row_index in range(total_num):
        #    if ((row_index==0) & (df_hole['Режим'][row_index] == df_hole['Способ эксплуатации'][row_index]) & (df_hole['Рзаб(иссл)'][row_index]!=np.NaN)):
        #        df_hole['Рзаб(иссл)'] = df_hole['Рзаб(иссл)'].asfreq('D', method='ffill')
        #    else:
        #        break
    new_df = new_df.append(df_hole)
df_copy = new_df
print(df_copy.info)

df_for_regr_s = df_copy[0:140]
chg_71 = df_copy[140:168]
chg_71 = chg_71.fillna(71)
df_for_regr_m = df_copy[168:224]
chg_48_6 = df_copy[224:252]
chg_48_6 = chg_48_6.fillna(48.6)
df_for_regr_e = df_copy[252:364]

df_for_regr = pd.DataFrame()
df_for_regr = df_for_regr.append(df_for_regr_s).append(chg_71).append(df_for_regr_m).append(chg_48_6).append(df_for_regr_e)

from sklearn.linear_model import LinearRegression
import statsmodels.api as sm
Y = df_for_regr['Рзаб(иссл)']
X = df_for_regr[['Режим', 'Способ эксплуатации']]

regr = LinearRegression()
regr.fit(X, Y)
print('Коэффициент при РЕЖИМЕ', regr.coef_[0])
print('Коэффициент при СПОСОБЕ', regr.coef_[1])
print('Свободный член', regr.intercept_)
print('R-квадрат', regr.score(X, Y))

X = sm.add_constant(X)
model = sm.OLS(Y, X).fit()
print_model = model.summary()
print(print_model)

# панельная регрессия (лучше со случайными эффектами)
# посмотреть по рядам (TimeSeries, графики по всем данным)
# подготовить данные для записи в инфлюкс
# скользящее среднее, экспоненциальное сглаживание, тренд