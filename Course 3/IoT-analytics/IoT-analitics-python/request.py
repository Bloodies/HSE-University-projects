# -*- coding: utf-8 -*-
"""
Created on Wed Jan 27 21:03:34 2021

@author: Bloodies
"""
#!pip install influxdb
from influxdb import InfluxDBClient
from pytz import timezone
import csv
import datetime

input_file = 'DataSource.csv'

time_format = '%D.%M.%Y %H.%M.%S' 
time_zone = 'UTC'
batchsize = 5000
metric = 'cluster3'
tagcolumns = ['well']
fieldcolumns = []

_databasename = 'production'
_delimiter = ';'
Time_column = 'time'
Date_column = 'data'

client = InfluxDBClient(host='Localhost', port=8086)
client.create_database('production')
db_list = client.get_list_database()