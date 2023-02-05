import requests
import matplotlib.pyplot as plt
import numpy as np
from collections import defaultdict

# get data
url = "https://cs526-fc451-default-rtdb.firebaseio.com/raw4/play_info.json"

sum_boss_damage = defaultdict(int)
sum_light_damage = defaultdict(int)
sum_trap_damage = defaultdict(int)


raw_data = requests.get(url).json().values()
# print(raw_data)
levels = [1, 2, 3, 4, 5]

for data in raw_data:
    # print(data)
    # pass level
    if data["level"] != 'N/A':
        if 'state' in data.keys() and data['state'] != 'N/A':
            sum_boss_damage[int(data['level'])] += data['boss_damage']
            sum_light_damage[int(data['level'])] += data['light_damage']
            sum_trap_damage[int(data['level'])] += data['trap_damage']


for level in levels:
    if level not in sum_boss_damage.keys():
        sum_boss_damage[level] = 0
    if level not in sum_light_damage.keys():
        sum_light_damage[level] = 0
    if level not in sum_trap_damage.keys():
        sum_trap_damage[level] = 0


x = []
y_boss_damage = []
y_light_damage = []
y_trap_damage = []
for indexed_key in sorted(sum_boss_damage.keys()):
    print(indexed_key)
    x.append(str(indexed_key))
    y_boss_damage.append(sum_boss_damage[indexed_key])
    y_light_damage.append(sum_light_damage[indexed_key])
    y_trap_damage.append(sum_trap_damage[indexed_key])


x_axis = np.arange(len(x))

plt.bar(x_axis - 0.2, y_boss_damage, color = 'sandybrown', width=0.2, label = 'Boss Damage')
plt.bar(x_axis, y_light_damage, color = 'peachpuff', width=0.2, label = 'Light Damage')
plt.bar(x_axis + 0.2, y_trap_damage, color = 'darkorange', width=0.2, label = 'Trap Damage')

plt.xticks(x_axis, x)
plt.xlabel("Levels")
plt.ylabel("Damage")
plt.title("Damage of Different types of Attack in each level")
plt.legend()
plt.show()