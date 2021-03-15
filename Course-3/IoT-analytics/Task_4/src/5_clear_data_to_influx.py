# -*- coding: utf-8 -*-
"""
Created on Tue May  7 22:29:48 2019

@author: пк
"""

# !pip install influxdb
from influxdb import InfluxDBClient
import csv
import datetime
from pytz import timezone
import pandas as pd
import numpy as np

_host = 'localhost'
_port = 8086

index = 0
_databasename = 'holes'
inputfilename = 'normal_holes.csv' #'clean_holes.csv' #'normal_holes.csv'
timecolumn = 'Время'
datecolumn = 'Дата замера'
timeformat = '%Y-%m-%d %H:%M:%S'
datatimezone = 'UTC'
_delimiter=';'
batchsize = 5000
metric = ['normal_data'] #['clean_data'] #['normal_data']
tagcolumns = ['Скважина']
fieldcolumns = []

client = InfluxDBClient(host=_host, port=_port)
client.create_database(_databasename)
db_list = client.get_list_database()

#client.drop_database(_databasename)
#client.create_database(_databasename)
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

        timestamp = unix_time_millis(datetime_local) * 1000000 # in nanoseconds
        tags = {}

        for t in tagcolumns:
            if t in row:
                v = row[t]
                pass
            tags[t] = v
        
        fieldNames = {'Рзаб(Нд)', 'Рзаб(Рпр)'}
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
            print('Read %d lines'%count)
            print('Inserting %d datapoints...'%(len(datapoints1)))
            response = client.write_points(datapoints1)
            
            if response == False:
                print('Problem inserting points, exiting...')
                exit(1)
            print("Wrote %d, response: %s" % (len(datapoints1), response))            
            datapoints1 = []
    print(index)

if len(datapoints1) > 0:
    print('Read %d lines'%count)
    print('Inserting %d datapoints...'%(len(datapoints1)))
    response = client.write_points(datapoints1)
    
    if response == False:
        print('Problem inserting points, exiting...')
        exit(1)
    
    print("Wrote %d, response: %s" % (len(datapoints1), response))

print('Done')