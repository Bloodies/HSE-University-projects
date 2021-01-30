# -*- coding: utf-8 -*-
"""
Created on Thu Jan 28 22:01:55 2021

@author: Bloodies
"""
import csv
import influxdb
from influxdb import InfluxDBClient

INFLUX_HOST         = 'localhost'       #localhost as standart
INFLUX_PORT         = 8086              #8086 as standart
INFLUX_USENAME      = ''
INFLUX_PASSWORD     = ''

DB_NAME             = 'cluster'         #name of database
QUERY               = '*'               #Select ___ from
MEASUREMENT_NAME    = 'cluster'
FILE_NAME           = "export_cluster"  #name of csv file

QUERY_PATH          = 'select ' + QUERY + ' from ' + MEASUREMENT_NAME
#QUERY_PATH          = "select " + QUERY + " from " + MEASUREMENT_NAME + \
#    "where time >= '2021-01-01'" + "and time <= '2021-01-31'"


client = InfluxDBClient(host=INFLUX_HOST, port=INFLUX_PORT)
#client = InfluxDBClient(host=INFLUX_HOST, port=INFLUX_PORT, username=INFLUX_USENAME, password=INFLUX_PASSWORD)

#commentarii na russkom pishu translitom tak kak inogda ne rabotaet unicode

try:
    client.ping()
except:
    raise Exception("Can not connect to InfluxDB. Check your insert or internet connection")
    
#db_list = client.get_list_database()
client.switch_database(DB_NAME)
#dbname_list = client.query('select * from dbname', chunked=True, chunk_size=10000).get_points()
#print(dbname_list)

result = client.query(QUERY_PATH)
column_names = result.raw["series"][0]["columns"]
csv_header = ""

for column in column_names:
    csv_header += column + ';"'

    csv_header = csv_header[:-1]

    points = result.get_points()
    
    with open(FILE_NAME + ".csv", "w") as export_file:
        export_file.write(csv_header + "\n")
        for point in points:
            line = ""
            for column in column_names:
                #line += str(point[column]) + ";"
#pri formate vishe csv pitaetsya preobrazovat' chislovie yacheiki v date 
#esli csv nuzhen tolko dlya prosmotra to rasskommentirovat' stroku nizhe i kommentirovat' vishe
                line += str(point[column]) + "';'"
            line = line[:-1]
            export_file.write(line + "\n")
    
print("Writing complete")