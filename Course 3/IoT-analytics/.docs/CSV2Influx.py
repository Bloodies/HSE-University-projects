# -*- coding: utf-8 -*-
"""
Created on Tue May  7 22:29:48 2019

@author: пк
"""

# в консоли IPython      ->      !pip install influxdb
from influxdb import InfluxDBClient
import csv
import datetime
from pytz import timezone

_host = 'localhost'
_port = 8086

_databasename = 'cluster3'
inputfilename = 'DataSource.csv'
timecolumn = 'time'
datecolumn = 'п»їdata'
timeformat = '%d.%m.%Y %H:%M:%S'
datatimezone = 'UTC'
_delimiter=';'
batchsize = 5000
metric = 'cluster3'
tagcolumns = ['well']
fieldcolumns = []

client = InfluxDBClient(host=_host, port=_port)
client.create_database(_databasename)
"""Проверяем, есть ли база данных"""
db_list = client.get_list_database()


""" Здесь нужно проверить кодом, есть ли нужная БД в списке
for db in db_list:
    if db == 
"""



"""Устанавливаем переключение на необходимую базу данных"""
client.drop_database(_databasename)
client.create_database(_databasename)

client.switch_database(_databasename)


epoch_naive = datetime.datetime.utcfromtimestamp(0)
epoch = timezone('UTC').localize(epoch_naive)

def unix_time_millis(dt):
    return int((dt - epoch).total_seconds() * 1000)

"""
    Check if data type of field is float
"""
def isfloat(value):
        try:
            float(value)
            return True
        except:
            return False

"""
    Check if data type of field is int
"""
def isinteger(value):
        try:
            if(float(value).is_integer()):
                return True
            else:
                return False
        except:
            return False

datapoints = []
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
            v = 'ID0100'
            if t in row:
                #v = row[t]
                pass
            tags[t] = v
        
        fields = {}
        for f in fieldcolumns:
            v = 0
            if f in row:
                v = float(row[f]) if isfloat(row[f]) else row[f]
            fields[f] = v

        point = {"measurement": metric, "time": timestamp, "fields": fields, "tags": tags}
        
        datapoints.append(point)
        count += 1
            
        if len(datapoints) % batchsize == 0:
            print('Read %d lines'%count)
            print('Inserting %d datapoints...'%(len(datapoints)))
            response = client.write_points(datapoints)
            
            if response == False:
                print('Problem inserting points, exiting...')
                exit(1)
            print("Wrote %d, response: %s" % (len(datapoints), response))
            
            datapoints = []
            
# write rest
if len(datapoints) > 0:
    print('Read %d lines'%count)
    print('Inserting %d datapoints...'%(len(datapoints)))
    response = client.write_points(datapoints)
    
    if response == False:
        print('Problem inserting points, exiting...')
        exit(1)
    
    print("Wrote %d, response: %s" % (len(datapoints), response))

print('Done')