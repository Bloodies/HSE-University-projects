# -*- coding: utf-8 -*-
"""
Created on Sat Jan 30 15:16:18 2021

@author: Bloodies
"""

import requests

url = "http://localhost:8086/query"

payload = "q=CREATE%20DATABASE%20tick_udemy"
headers = {
    'content-type': "application/x-www-form-urlencoded"
    }

response = requests.request("POST", url, data=payload, headers=headers)

print(response.text)