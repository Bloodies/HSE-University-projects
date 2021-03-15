# -*- coding: utf-8 -*-
"""
Created on 0 0:00:00 0000

@author: Bloodies
"""

import numpy as np
import pandas as pd

data = pd.read_csv('clean_holes.csv', sep=';', engine='python')
df = pd.DataFrame(data)

print(f"DataFrame:\n{df}\n")
print(f"column types:\n{df.dtypes}")
        
holes_list = []

col_List= df['Скважина'].tolist()
num = 1
for i in range(len(col_List)):
    if (i == 0) or (col_List[i] != col_List[i-1]):
        holes_list.append(col_List[i])
        num += 1
        
columns = ['Рпр(ТМ)', 'Рзаб(Рпр)', 'Рзаб(Нд)', 'Рзаб(иссл)']

for hole in holes_list:
    df1 =  df[lambda df: df['Скважина'] == hole]

    for col in columns:         
        col_List= df1[col].tolist()
        
        for i in range(len(col_List)):
            delta = len(col_List) - i
            left = col_List[i-1]+col_List[i-2]+col_List[i-3]+col_List[i-4]+col_List[i-5]
            if delta == 1:
                right = col_List[0]+col_List[1]+col_List[2]+col_List[3]+col_List[4]
            if delta == 2:
                right = col_List[i+1]+col_List[0]+col_List[1]+col_List[2]+col_List[3]
            if delta == 3:
                right = col_List[i+1]+col_List[i+2]+col_List[0]+col_List[1]+col_List[2]
            if delta == 4:
                right = col_List[i+1]+col_List[i+2]+col_List[i+3]+col_List[0]+col_List[1]
            if delta == 5:
                right = col_List[i+1]+col_List[i+2]+col_List[i+3]+col_List[i+4]+col_List[0]
            if delta >= 6:
                right = col_List[i+1]+col_List[i+2]+col_List[i+3]+col_List[i+4]+col_List[i+5]
            col_List[i] = (left + right) / 10        
                       
        df1[col] = col_List
        df[lambda df: df['Скважина'] == hole] = df1

max = df.max()
df['Рпр(ТМ)'] = df['Рпр(ТМ)'].apply(lambda x: round(x / max['Рпр(ТМ)'], 3))
df['Рзаб(Рпр)'] = df['Рзаб(Рпр)'].apply(lambda x: round(x / max['Рзаб(Рпр)'], 3))
df['Рзаб(Нд)'] = df['Рзаб(Нд)'].apply(lambda x: round(x / max['Рзаб(Нд)'], 3))
df['Рзаб(иссл)'] = df['Рзаб(иссл)'].apply(lambda x: round(x / max['Рзаб(иссл)'], 3))

df.to_csv('normal_holes.csv', index=False, sep=';', encoding='cp1251')