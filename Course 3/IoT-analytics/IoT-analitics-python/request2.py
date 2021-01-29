# -*- coding: utf-8 -*-
"""
Created on Thu Jan 28 22:01:55 2021

@author: Bloodies
"""
import csv
import influxdb
import itertools
import argparse
import pandas as pd

from influxdb import InfluxDBClient

client = InfluxDBClient(host='localhost', port=8086)
SEPARATOR = ','

try:
    client.ping()
except:
    raise Exception("Can not connect to InfluxDB. Is your network connection ok?")
    
#db_list = client.get_list_database()
client.switch_database('dbname')
#dbname_list = client.query('select * from dbname', chunked=True, chunk_size=10000).get_points()
#print(dbname_list)

#myData = dbname_list.to_list()
#myFile = open('example2.csv', 'w')
#with myFile:
#    writer = csv.writer(myFile)
#    writer.writerows(dbname_list)

#request = "SELECT * FROM " + measurement + " WHERE time >= " + \start_date + " AND time <= " + end_date

result = client.query('select * from dbname')
column_names = result.raw["series"][0]["columns"]
csv_header = ""

for column in column_names:
    csv_header += column + SEPARATOR

    csv_header = csv_header[:-1]

    points = pt = result.get_points()

    with open("export.csv", "w") as export_file:
        export_file.write(csv_header + "\n")
        for point in points:
            line = ""
            for column in column_names:
                line += str(point[column]) + SEPARATOR
            line = line[:-1]
            export_file.write(line + "\n")

#--------------------------------
#points = client.query('select * from dbname', chunked=True, chunk_size=10000).get_points()
#chunks = pd.DataFrame(points)
#for chunk in chunks:
#    pd.DataFrame(chunk).to_csv('example2.csv', sep=",", encoding="utf-8")
#--------------------------------
#measurement = next(iter(dbname_list))
#chunks = dbname_list[measurement]
#for chunk in chunks:
#    pd.DataFrame(chunk).to_csv('example2.csv', sep=",", encoding="utf-8")
#--------------------------------
#dfs = pd.DataFrame(dbname_list)
#for d in dfs:
#    d.to_excel('output.xlsx', "Sheet")
#--------------------------------
#myData = dbname_list.to_list()
#myFile = open('example2.csv', 'w')
#with myFile:
#    writer = csv.writer(myFile)
#    writer.writerows(myData)
    
print("Writing complete")