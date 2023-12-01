import csv
import os

fileName = 'henry_constants'
script_dir = os.path.dirname(os.path.abspath(__file__))
src_file_path = os.path.join(script_dir, '%s_src.csv' %fileName)
file_path = os.path.join(script_dir, '%s.csv' %fileName)
data = []
headers = None
index = 0

with open(src_file_path,newline='') as srccsvfile:
    reader = csv.reader(srccsvfile, delimiter=',', quotechar='"')
    for row in reader:
        if index== 1:
            headers = row
        elif headers != None:
            v1 = row[1]
            v2 = row[3]
            v3= row[2]
            if "" not in [v1, v3] and "L" in v2:
                data.append(row)
        index+=1


with open(file_path, 'w', newline='') as csvfile:
    writer = csv.writer(csvfile, delimiter=',',quotechar='"')
    writer.writerow(headers)
    writer.writerows(data)