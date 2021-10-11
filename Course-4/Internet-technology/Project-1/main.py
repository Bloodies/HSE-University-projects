import re

patternStr = r'\bM{0,4}(?:CM|CD|D?C{0,3})(?:XC|XL|L?X{0,3})(?:IX|IV|V?I{0,3})\b'
open('output.txt', 'w').close()


def valueOfRoman(c):
    if c == 'M':
        return 1000
    elif c == 'D':
        return 500
    elif c == 'C':
        return 100
    elif c == 'L':
        return 50
    elif c == 'X':
        return 10
    elif c == 'V':
        return 5
    elif c == 'I':
        return 1
    return -1


def romanToInt(s):
    if s is None or len(s) == 0:
        return 0

    total = 0
    current = s[0]
    run = valueOfRoman(current)

    for i in range(1, len(s)):
        next = s[i]
        value = valueOfRoman(s[i])
        if next == current:
            run += value
        else:
            if value < valueOfRoman(current):
                total += run
            else:
                total -= run
            run = valueOfRoman(next)

        current = next

    return total + run


with open("input.txt", "r", encoding='utf-8') as file:
    for line in file:
        for match in re.findall(patternStr, line):
            if len(match) != 0:
                # line = line.replace(match, str(romanToInt(match)))
                line = re.sub(match, str(romanToInt(match)), line)
        print(line)
        with open("output.txt", "a", encoding='utf-8') as output:
            output.write(line)