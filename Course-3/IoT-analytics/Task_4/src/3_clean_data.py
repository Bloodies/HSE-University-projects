# -*- coding: utf-8 -*-
"""
Created on 0 0:00:00 0000

@author: Bloodies
"""

# !pip install influxdb
import pandas as pd
import numpy as np

df = pd.read_csv('holes.csv', sep=';', engine='python')
q_low = df.quantile(.01)
q_high = df.quantile(.99)
print(q_low)
print(q_high)

df = df[(df['Режим'] >= q_low['Режим']) & (df['Режим'] <= q_high['Режим'])]
df = df[(df['Рпр(ТМ)'] >= q_low['Рпр(ТМ)']) & (df['Рпр(ТМ)'] <= q_high['Рпр(ТМ)'])]
df = df[(df['Рзаб(Рпр)'] >= q_low['Рзаб(Рпр)']) & (df['Рзаб(Рпр)'] <= q_high['Рзаб(Рпр)'])]
df = df[(df['Рзаб(иссл)'] >= q_low['Рзаб(иссл)']) & (df['Рзаб(иссл)'] <= q_high['Рзаб(иссл)'])]

df.to_csv('clean_holes.csv', index=False, sep=';', encoding='cp1251')