import requests
import matplotlib.pyplot as plt
import numpy as np
from collections import defaultdict

# get data
url = "https://cs526-fc451-default-rtdb.firebaseio.com/raw4/play_info.json"

num_attempts = defaultdict(int)
num_pass = defaultdict(int)
num_lose = defaultdict(int)


raw_data = requests.get(url).json().values()
# print(raw_data)
levels = [1, 2, 3, 4, 5, 6]

for data in raw_data:
    # print(data)
    # pass level
    if data["level"] != 'N/A':
        if 'state' in data.keys():
            if data["state"] == 'win':
                num_pass[int(data['level'])] += 1
            elif data["state"] == 'lose':
                num_lose[int(data['level'])] += 1
            num_attempts[int(data['level'])] += 1

print(num_pass.keys())

for level in levels:
    if level not in num_pass.keys():
        num_pass[level] = 0
    if level not in num_lose.keys():
        num_lose[level] = 0
    if level not in num_attempts.keys():
        num_attempts[level] = 0


print(num_pass)
print(num_lose)
print(num_attempts)

x = []
y_pass = []
y_attempts = []
for indexed_key in sorted(num_attempts.keys()):
    print(indexed_key)
    x.append(str(indexed_key))
    y_pass.append(num_pass[indexed_key])
    y_attempts.append(num_attempts[indexed_key])
print(x)
print(y_pass)
print(y_attempts)

x_axis = np.arange(len(x))

plt.bar(x_axis + 0.2, y_pass, color = 'gold', width=0.4, label = 'Pass Number')
plt.bar(x_axis - 0.2, y_attempts, color = 'aquamarine', width=0.4, label = 'Total Attempts Number')

plt.xticks(x_axis, x)
plt.xlabel("Levels")
plt.ylabel("Number of Attempts")
plt.title("Number of Passes and Total Attempts in each level")
plt.legend()
plt.show()

