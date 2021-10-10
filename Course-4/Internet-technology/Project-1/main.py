import re

num_map = [(1000, 'M'), (900, 'CM'), (500, 'D'), (400, 'CD'), (100, 'C'), (90, 'XC'),
           (50, 'L'), (40, 'XL'), (10, 'X'), (9, 'IX'), (5, 'V'), (4, 'IV'), (1, 'I')]

with open ('input.txt', 'r') as file:
  old_data = file.read().split("\n")
  for key in old_data:
      print(key)
      print(old_data)
      old_data = re.findall(r"-?\d+(\.\d+)?", key)
      print(key)
      print(old_data)
      roman = ''

      for num in old_data:
          while num > 0:
              for i, r in num_map:
                  while num >= i:
                      roman += r
                      num -= i

      print(key)
      print(old_data)
      print(roman)
      new_data = old_data.replace(old_data, roman)

      with open('output.txt', 'w') as f:
          f.write(new_data)