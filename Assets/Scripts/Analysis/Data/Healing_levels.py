import requests
import matplotlib.pyplot as plt
import numpy as np
from collections import defaultdict

# create data
url = "https://cs526-fc451-default-rtdb.firebaseio.com/raw4/play_info.json"

levels = [1, 2, 3, 4, 5]

raw_data = requests.get(url).json().values()

sum_healing = defaultdict(int)
sum_attempts = defaultdict(int)
ret = {}
for data in raw_data:
    if data["level"] != 'N/A':
        if 'healing_energy' in data:
            sum_healing[int(data['level'])] += int(data["healing_energy"])
            sum_attempts[int(data['level'])] += 1

for level in levels:
    if level not in sum_healing.keys():
        sum_healing[level] = 0
    if level not in sum_attempts.keys():
        sum_attempts[level] = 0

for level in sum_healing.keys():
    if sum_attempts[level] != 0:
        ret[level] = ("%.2f" % (sum_healing[level] / sum_attempts[level]))
    else:
        ret[level] = 0


x = []
y = []
for indexed_level in sorted(ret.keys()):
    x.append(str(indexed_level))
    y.append(float(ret[indexed_level]))
print(x)
print(y)

plt.bar(x, y, color = 'plum', width=0.3)
plt.xlabel("Levels")
plt.ylabel("Healing Amount")
plt.title("Average Healing Amount in each level")
plt.show()